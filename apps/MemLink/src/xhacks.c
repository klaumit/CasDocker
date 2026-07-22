#include "xhacks.h"

#ifdef __HITACHI__

void pseudoFunc()
{
    volatile unsigned long x;
    x = 0x12345678;
}

unsigned long GetIP()
{
    unsigned long pc;
    pc = (unsigned long)pseudoFunc;
    return pc;
}

#else

unsigned long GetIP()
{
    return (((unsigned long)GetIPSeg() << 16) | (unsigned long)GetIPOff());
}

#endif

