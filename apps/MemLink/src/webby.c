#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"

word MmLinkGetOpenStat(void)
{
    return IB_COM2_OPEN;
}

word MmLinkTxBufClr(void)
{
    return IW_SRL_NOERR;
}

word MmLinkRxBufClr(void)
{
    return IW_SRL_NOERR;
}

word MmLinkPortOpen(SRL_STAT *po)
{
    return IW_SRL_NOERR;
}

word MmLinkPortClose(void)
{
    return IW_SRL_NOERR;
}

word MmLinkPortFClose(void)
{
    return IW_SRL_NOERR;
}

word MmLinkRecvByte(byte *data)
{
    return IW_SRL_NOERR;
}

word MmLinkRecvBlock(byte *data, word size, word *num)
{
    return IW_SRL_NOERR;
}

word MmLinkSendBlock(byte *data, word size)
{
    return IW_SRL_NOERR;
}

