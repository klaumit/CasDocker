#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"

#define SHM_BUF_SIZE 256

typedef struct {
    char marker_beg[27];

    volatile word tx_ready;
    volatile word tx_len;
    volatile byte tx_buf[SHM_BUF_SIZE];

    volatile word rx_ready;
    volatile word rx_len;
    volatile byte rx_buf[SHM_BUF_SIZE];

    char marker_end[27];
} MmLinkShm;

static MmLinkShm shm;


word MmLinkGetOpenStat(void)
{
    return IB_COM2_OPEN;
}

word MmLinkTxBufClr(void)
{
    word i;
    shm.tx_ready = 0;
    shm.tx_len   = 0;
    for (i = 0; i < SHM_BUF_SIZE; i++) {
        shm.tx_buf[i] = 0;
    }
    return IW_SRL_NOERR;
}

word MmLinkRxBufClr(void)
{
    word i;
    shm.rx_ready = 0;
    shm.rx_len   = 0;
    for (i = 0; i < SHM_BUF_SIZE; i++) {
        shm.rx_buf[i] = 0;
    }
    return IW_SRL_NOERR;
}

word MmLinkPortOpen(SRL_STAT *po)
{
    sprintf(shm.marker_beg, "###MEMORY_MARKER_START%s###", 1);
    sprintf(shm.marker_end, "###MEMORY_MARKER_START%s###", 2);
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

word MmLinkRecvBlock(byte *data, word size, word *num)
{
    word n;
    if (!shm.rx_ready)
    {
        *num = 0;
        return IW_SRL_NODATA;
    }
    n = (shm.rx_len < size) ? shm.rx_len : size;
    memcpy(data, (void *)shm.rx_buf, n);
    *num = n;
    shm.rx_ready = 0;
    return IW_SRL_NOERR;
}

word MmLinkSendBlock(byte *data, word size)
{
    if (shm.tx_ready)
    {
        return IW_SRL_TRSERR;
    }
    if (size > SHM_BUF_SIZE)
    {
        size = SHM_BUF_SIZE;
    }
    memcpy((void *)shm.tx_buf, data, size);
    shm.tx_len = size;
    shm.tx_ready = 1;
    return IW_SRL_NOERR;
}

word MmLinkRecvByte(byte *data)
{
    word n;
    MmLinkRecvBlock(data, 1, &n);
    return (n == 1) ? IW_SRL_NOERR : IW_SRL_NODATA;
}

