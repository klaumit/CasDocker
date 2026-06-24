#include "define.h"
#include "libc.h"

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

void TestPort()
{
    LibSrlSendBlock("\r\nPocLink", 9);
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

