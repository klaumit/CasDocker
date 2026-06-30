
#define SYNC0       0xAA
#define SYNC1       0x55

#define MSG_UNKNOWN 0
#define MSG_HELLO   1
#define MSG_QUIT    2
#define MSG_INFO    3

#define MAX_PAYLOAD 64

typedef struct
{
    unsigned char  kind;
    unsigned short length;
    unsigned char  payload[MAX_PAYLOAD];
    unsigned short checksum;
} Message;

int SendTxtMessage(unsigned char kind, char *text);

void UpdateCrc(word *crc, const byte *data, word length);

