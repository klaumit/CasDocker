#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>
#include "sysdm.h"
#include "msglayer.h"

void main()
{
	char text[MAX_PAYLOAD+1];
	byte kind;
	char debug[128];
	int i;
	int maxTry;

	LibInitDisp();
	LibClrDisp();
	
	LibStringDsp( B@ "### Pocket Link ###", 5, 10, 160, B@@ IB_PFONT2);

	sprintf(debug, "CPU and port: %s over %s", 
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind())
	);
	LibStringDsp( B@ debug, 5, 30, 160, B@@ IB_PFONT1);
	
	LibPutDisp();

	sprintf(debug, "Communication: %s",
		GetOpenPortStr(OpenPort(GetCommKind()))
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

	maxTry = 25;
	for (i = 0; i < maxTry; i++)
	{
		sprintf(debug, "Waiting %d of %d sec...", i, maxTry);
		LibStringDsp( B@ debug, 5, 100, 160, B@@ IB_PFONT2);
		LibPutDisp();

		if (ReadTxtMessage(&kind, text))
		{
			sprintf(debug, " [%d] %s", kind, text);
			LibStringDsp( B@ debug, 5, 120, 160, B@@ IB_PFONT2);
			LibPutDisp();

			if (kind == MSG_QUIT)
			{
				sprintf(debug, "  --> I will quit soon!");
				LibStringDsp( B@ debug, 5, 140, 160, B@@ IB_PFONT2);
				LibPutDisp();
				Wait(5);
				break;
			}
		}
		Wait(1);
	}

	ClosePort();
	LibJumpMenu();
}

