#include <stdio.h>
#include "define.h"
#include "libc.h"
#include "l_define.h"
#include "l_libc.h"

#define SHM_BUF_SIZE 256

typedef struct {
    char marker_beg[24];

    volatile dword tx_ready;
    volatile dword tx_len;
    volatile byte tx_buf[SHM_BUF_SIZE];

    char marker_mid[24];

    volatile dword rx_ready;
    volatile dword rx_len;
    volatile byte rx_buf[SHM_BUF_SIZE];

    char marker_end[24];
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
    sprintf(shm.marker_beg, "###%s_%08lX_BEG%i###", "ML", &shm.tx_ready,   15);
    sprintf(shm.marker_mid, "###%s_%08lX_MID%i###", "ML", &shm.rx_ready,   16);
    sprintf(shm.marker_end, "###%s_%08lX_END%i###", "ML", &shm.marker_end, 17);
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
    word n, i, o;
    if (shm.rx_ready < 1)
    {
        *num = 0;
        return IW_SRL_NODATA;
    }
    n = (shm.rx_len < size) ? shm.rx_len : size;
    o = (shm.rx_len - shm.rx_ready);
    for (i = 0; i < n; i++)
    {
        data[i] = shm.rx_buf[o + i];
    }
    *num = n;
    shm.rx_ready = shm.rx_ready - n;
    return IW_SRL_NOERR;
}

word MmLinkSendBlock(byte *data, word size)
{
    word i;
    if (shm.tx_ready >= 1)
    {
        return IW_SRL_TRSERR;
    }
    if (size > SHM_BUF_SIZE)
    {
        size = SHM_BUF_SIZE;
    }
    for (i = 0; i < size; i++)
    {
        shm.tx_buf[i] = data[i];
    }
    shm.tx_len = size;
    shm.tx_ready = size;
    return IW_SRL_NOERR;
}

word MmLinkRecvByte(byte *data)
{
    word n;
    MmLinkRecvBlock(data, 1, &n);
    return (n == 1) ? IW_SRL_NOERR : IW_SRL_NODATA;
}

