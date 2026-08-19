#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>
#include "sysdm.h"
#include "msglayer.h"
#include "xhacks.h"

#ifdef __HITACHI__
#else
    #include <stdrom.h>
#endif

void main()
{
	char text[MAX_PAYLOAD+1];
	byte kind;
	char debug[128];
	int i, maxTry, newTry, srcAdr, bank, seg, off, len;
	int curOff, remaining, chunkLen;
	FarFun jump;
	word *m_code, *m_sts;
	byte array[64];

	#ifdef __HITACHI__
	    unsigned int code[3];
	    volatile byte *src;
	    byte c;
		void **mode_info;
	#else
	    unsigned char far *src;
	    unsigned char c;
		word *m_seg, *m_ofs;
	#endif
	char tmp[MAX_PAYLOAD];
	word j, ptr;

	LibInitDisp();
	LibClrDisp();
	
	LibStringDsp( B@ "### Pocket Link ###", 5, 10, 160, B@@ IB_PFONT2);

	sprintf(debug, "CPU and port: %s over %s", 
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind())
	);
	LibStringDsp( B@ debug, 5, 30, 160, B@@ IB_PFONT1);
	
	LibPutDisp();

	sprintf(debug, "Comm: %s  Ptr: %08lX",
		GetOpenPortStr(OpenPort(GetCommKind())), GetIP()
	);
	LibStringDsp( B@ debug, 5, 40, 160, B@@ IB_PFONT1);

	sprintf(debug, "Lang: %s %s %s",
		GetLayoutStr(GetKeyLayout()), GetLangStr(GetLanguage()), GetLangSuppStr(GetLangSupport())
	);
	LibStringDsp( B@ debug, 5, 50, 160, B@@ IB_PFONT1);

	sprintf(debug, "APO: %d s  Batt: %s",
		GetAPOTimeS(), GetBattStr()
	);
	LibStringDsp( B@ debug, 5, 60, 160, B@@ IB_PFONT1);

	sprintf(debug, "Free: %3.0d% % of %d blocks",
		GetFreeMemory(), GetTotalMemory()
	);
	LibStringDsp( B@ debug, 5, 70, 160, B@@ IB_PFONT1);

	sprintf(debug, "M: %s (%s)",
		GetModelStr(), GetVersionStr()
	);
	LibStringDsp( B@ debug, 5, 80, 160, B@@ IB_PFONT1);

	LibPutDisp();

	sprintf(debug, "app=PocLink;cpu=%s;comm=%s;area=%s;ver=%s;chip=%s;mem=%d",
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind()),
		GetLangSuppStr(GetLangSupport()), GetVersionStr(),
		GetModelStr(), GetTotalMemory()
	);
	SendTxtMessage(MSG_HELLO, debug);

	maxTry = 15 * TICKS_PER_SEC;
	for (i = 0; i < maxTry; i++)
	{
		if (i % TICKS_PER_SEC == 0)
		{
		sprintf(debug, "Waiting %d of %d sec...", i / TICKS_PER_SEC, maxTry / TICKS_PER_SEC);
		LibStringDsp( B@ debug, 5, 100, 160, B@@ IB_PFONT2);
		LibPutDisp();
		}

		if (ReadTxtMessage(&kind, text))
		{
			sprintf(debug, " [%d] %s", kind, text);
			LibStringDsp( B@ debug, 5, 120, 160, B@@ IB_PFONT2);
			LibPutDisp();

			if (kind == MSG_QUIT)
			{
				sprintf(debug, " -> I will quit soon!");
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				WaitTicks(5 * TICKS_PER_SEC);
				break;
			}
			if (kind == MSG_ALIVE)
			{
				if (sscanf(text, "%x", &newTry) != 1)
				{
					newTry = 21;
				}
				sprintf(debug, " -> I will wait %d s!", newTry);
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				maxTry = newTry * TICKS_PER_SEC;
				i = 0;
			}
			if (kind == MSG_GET_MODE)
			{
				if (sscanf(text, "%x", &bank) != 1)
				{
					bank = 0;
				}
				sprintf(debug, " -> Get the mode #%d!", bank);
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				if (bank == 1)
				{
					#ifdef __HITACHI__
					LibGetLastMode(&m_code, &m_sts, &mode_info);
					#else
					LibGetLastMode(&m_code, &m_sts, &m_seg, &m_ofs);
					#endif
				}
				else if (bank == 2)
				{
					#ifdef __HITACHI__
					LibGetMode(&m_code, &m_sts, &mode_info);
					#else
					LibGetMode(&m_code, &m_sts, &m_seg, &m_ofs);
					#endif
				}
				#ifdef __HITACHI__
				    sprintf(tmp, "%02X|%04X|%04X|%08X", bank, m_code, m_sts, mode_info);
				#else
				    sprintf(tmp, "%02X|%04X|%04X|%08X", bank, m_code, m_sts, MK_FP(m_seg, m_ofs));
				#endif
				SendTxtMessage(MSG_GET_MODE, (char *)tmp);
				maxTry = 25 * TICKS_PER_SEC;
				i = 0;
			}
			if (kind == MSG_JUMP_OS)
			{
				if (sscanf(text, "%x|%x|%x", &bank, &m_code, &m_sts) != 3)
				{
					bank = 0; m_code = 0; m_sts = 0;
				}
				sprintf(debug, " -> K %02X %04X %04X", bank, m_code, m_sts);
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				if (bank == 1)
				{
					ptr = LibModeJump(m_code, m_sts);
				}
				else if (bank == 2)
				{
					LibMenuJump(m_code);
					ptr = 1;
				}
				else if (bank == 3)
				{
					LibScrtJmp(m_sts, m_code);
					ptr = 1;
				}
				else if (bank == 4)
				{
					#ifdef __HITACHI__
						LibSecretCall((void *)(((dword)m_code << 16) | (word)m_sts));
					#else
						LibSecretCall(m_code, m_sts);
					#endif
					ptr = 1;
				}
				else if (bank == 5)
				{
					LibCallListMenu();
					ptr = 1;
				}
				else if (bank == 6)
				{
					ptr = LibDualWin(m_code, m_sts, &array);
				}
				sprintf(tmp, "%02X|%04X|%04X|%02X", bank, m_code, m_sts, ptr);
				SendTxtMessage(MSG_JUMP_OS, (char *)tmp);
				maxTry = 25 * TICKS_PER_SEC;
				i = 0;
			}
			if (kind == MSG_JUMP_FAR)
			{
				if (sscanf(text, "%x|%x|%x|%x", &srcAdr, &bank, &seg, &off) != 4)
				{
					srcAdr = 0; bank = 0; seg = 0; off = 0;
				}
				sprintf(debug, " -> J %04X %d %04X %04X", srcAdr, bank, seg, off);
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				WaitTicks(7 * TICKS_PER_SEC);
				if (bank != 0) { SwitchBank((word)srcAdr, (byte)bank); }

				#ifdef __HITACHI__
					code[0] = 0xD201E400; code[1] = 0x422B0009;
					code[2] = ((unsigned long)(word)(seg) << 16) | ((unsigned long)(word)(off));
					jump = (FarFun)code;
				#else
					jump = (FarFun)MK_FP((word)seg, (word)off);
				#endif

				ClosePort();
				jump();

				if (bank != 0) { SwitchBank(0x0104, (byte)bank); /* Fonts */ }
				break;
			}
			if (kind == MSG_MEM_READ)
			{
				if (sscanf(text, "%x|%x|%x|%x|%x", &srcAdr, &bank, &seg, &off, &len) != 5)
				{
					srcAdr = 0; bank = 0; seg = 0; off = 0; len = 0;
				}
				sprintf(debug, " -> R %04X %d %04X %04X %d", srcAdr, bank, seg, off, len);
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				SwitchBank((word)srcAdr, (byte)bank);
				remaining = len;
				curOff = off;

				do
				{
				chunkLen = PKT_SIZE;
				if (chunkLen > remaining) chunkLen = remaining;
				if (curOff + chunkLen > SEG_SIZE) chunkLen = (SEG_SIZE - curOff);

				#ifdef __HITACHI__
				    src = (volatile byte *)(((unsigned long)(word)(seg) << 16) | (unsigned long)(word)(curOff));
				#else
				    src = (unsigned char far *)MK_FP((word)seg, (word)curOff);
				#endif

				ptr = sprintf(tmp, "%04X|%02X|%04X|%04X|%04X|", (word)srcAdr, (byte)bank, (word)seg, (word)curOff, (word)chunkLen);
				for (j = 0; j < chunkLen; j++)
				{
				    c = src[j];
				    sprintf(&tmp[ptr], "%02X", c);
				    ptr += 2;
				}
				SendTxtMessage(MSG_MEM_READ, (char *)tmp);

				curOff += chunkLen;
				remaining -= chunkLen;
				if (remaining > 0) WaitTicks(1);
				} 
				while (remaining > 0 && curOff < SEG_SIZE);

				SwitchBank(0x0104, (byte)bank); /* Fonts */
				maxTry = 25 * TICKS_PER_SEC;
				i = 0;
			}
		}
		WaitTicks(1);
	}

	ClosePort();
	LibJumpMenu();
}

