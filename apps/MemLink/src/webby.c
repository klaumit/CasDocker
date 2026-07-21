#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"


char arr[20][128];

sprintf(arr[0], "###MEMORY_MARKER_START%d###", 1);
sprintf(arr[1], "###UNIQUE_%s_ABC###", "1234567890");
sprintf(arr[2], "###MEMORY_MARKER_END%d#####", 1);

sprintf(arr[10], "###MEMORY_MARKER_START%d###", 2);
sprintf(arr[11], "###UNIQUE_%s_ABC###", "1234567890");
sprintf(arr[12], "###MEMORY_MARKER_END%d#####", 2);



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

