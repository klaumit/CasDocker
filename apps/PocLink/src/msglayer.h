
void UpdateCrc(word *crc, const byte *data, word length);

bool ReadPort(byte *data);
bool SendBlock(byte *data, word size);
bool ReadBlock(byte *data, word size, word *num);

#define SYNC0       0xAA
#define SYNC1       0x55

#define MSG_UNKNOWN  0
#define MSG_HELLO    1
#define MSG_QUIT     2
#define MSG_ALIVE    3
#define MSG_MEM_READ 4

#define MAX_PAYLOAD 240

typedef struct
{
    unsigned char  kind;
    unsigned short length;
    unsigned char  payload[MAX_PAYLOAD];
    unsigned short checksum;
} Message;

bool SendTxtMessage(unsigned char kind, char *text);
bool ReadTxtMessage(unsigned char *kind, char *text);

