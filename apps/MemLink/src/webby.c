#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"

#define SHM_BUF_SIZE 256

char marker_beg[27];

volatile word tx_ready;
volatile word tx_len;
volatile byte tx_buf[SHM_BUF_SIZE];

volatile word rx_ready;
volatile word rx_len;
volatile byte rx_buf[SHM_BUF_SIZE];

char marker_end[27];


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
    sprintf(marker_beg, "###MEMORY_MARKER_START%s###", 1);
    sprintf(marker_end, "###MEMORY_MARKER_START%s###", 2);
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

