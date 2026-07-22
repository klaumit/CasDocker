
#ifndef LIBWEBBY_H
#define LIBWEBBY_H

word MmLinkGetOpenStat(void);

word MmLinkTxBufClr(void);

word MmLinkRxBufClr(void);

word MmLinkPortOpen(SRL_STAT *po);

word MmLinkPortClose(void);

word MmLinkPortFClose(void);

word MmLinkRecvByte(byte *data);

word MmLinkRecvBlock(byte *data, word size, word *num);

word MmLinkSendBlock(byte *data, word size);

#endif
