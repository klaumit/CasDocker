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

