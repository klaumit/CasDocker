#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"

const char *GetBattStr(void)
{
    if (LibGetBLD())
    {
        return "Normal";
    }
    else
    {
        return "Low";
    }
}

byte GetCommKind(void)
{
	#ifdef __HITACHI__
		return LibGetCommDevice();
	#else
		return IB_SRL_COM2;
	#endif
}

const char *GetCommStr(byte num)
{
    switch (num)
    {
        case IB_SRL_COM2: return "9pin";
        case IB_SRL_COM3: return "USB";
        default: return "?";
    }
}

const char *GetVersionStr(void)
{
    byte version[16 + 1];
    static char ascii[16 + 1];
    LibGetVer2(version);
    sprintf(ascii, "%s", version);
    return ascii;
}

const char *GetModelStr(void)
{
    dword num = LibGetModelType();
    static char ascii[4 + 1];
    ascii[0] = (num >> 24) & 0xFF;
    ascii[1] = (num >> 16) & 0xFF;
    ascii[2] = (num >> 8)  & 0xFF;
    ascii[3] = num & 0xFF;
    ascii[4] = '\0';
    return ascii;
}

word GetAPOTimeS(void)
{
    word sec;
    sec = (word)( (float)LibGetAPOTime() * (float)0.5 );
    return sec;
}

word GetFreeMemory(void)
{
    word mem;
    if (LibGetFlash() == 0)
        mem = 0;
    else
        mem = (word)(100 * ((float)LibGetFreeBlock() / (float)LibGetFlash()));
    return mem;
}

word GetTotalMemory(void)
{
    word total;
    total = LibGetFlash();
    return total;
}

byte GetLangSupport(void)
{
    return LibGetLangInf();
}

const char *GetLangSuppStr(byte num)
{
    switch (num)
    {
        case IB_LANG_ENGLISH: return "America";
        default:              return "Europe";
    }
}

byte GetLanguage(void)
{
    return LibGetLang();
}

const char *GetLangStr(byte num)
{
    switch (num)
    {
        case IB_DEUTSCH: return "German";
        case IB_ENGLISH: return "English";
        case IB_ESPANOL: return "Spanish";
        case IB_FRANCAIS: return "French";
        case IB_ITALIANO: return "Italian";
        default: return "?";
    }
}

byte GetKeyLayout(void)
{
    return LibGetKeyKind();
}

const char *GetLayoutStr(byte num)
{
    switch (num)
    {
        case IB_QWERTY: return "QWERTY";
        case IB_AZERTY: return "AZERTY";
        case IB_QWERTZ: return "QWERTZ";
        default: return "?";
    }
}

word GetCommState(void)
{
    return LibSrlGetOpenStat();
}

const char *GetStateStr(word num)
{
    switch (num)
    {
        case IB_NO_OPEN: return "Closed";
        case IB_COM2_OPEN: return "9pin";
        #ifdef __HITACHI__
        case IB_COM3_OPEN: return "USB";
        #else
        #endif
        default: return "?";
    }
}

byte GetCpuKind(void)
{
    #ifdef __HITACHI__
		return 2;
	#else
		return 1;
	#endif
}

const char *GetCpuStr(byte num)
{
    switch (num)
    {
        case 1: return "X86";
        case 2: return "SH3";
        default: return "?";
    }
}

word OpenPort(byte kind)
{
    SRL_STAT srl;
    LibSrlTxBufClr();
    LibSrlRxBufClr();
    srl.port = kind;
    srl.speed = IB_SRL_38400BPS;
    srl.parit = IX_SRL_NONE;
    srl.datab = IX_SRL_8DATA;
    srl.stopb = IX_SRL_1STOP;
    srl.fctrl = IX_SRL_RSCS;
    return LibSrlPortOpen(&srl);
}

const char *GetOpenPortStr(word num)
{
    switch (num)
    {
        case IW_SRL_NOERR: return "Open";
        case IW_SRL_PRMERR: return "Error";
        default: return "?";
    }
}

void ClosePort()
{
    word wTimeout = 8000;
    while (wTimeout && (LibSrlPortClose() != IW_SRL_NOERR))
    {
        --wTimeout;
    }
    LibSrlPortFClose();
}

void Wait(void)
{
    LibWait(IB_125MWAIT);
}

