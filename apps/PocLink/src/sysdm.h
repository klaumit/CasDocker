
byte GetCommKind(void);
const char *GetCommStr(byte num);

byte GetCpuKind(void);
const char *GetCpuStr(byte num);

word OpenPort(byte port);
const char *GetOpenPortStr(word num);

void TestPort();

void ClosePort();

void Wait(void);

void UpdateCrc(word *crc, const byte *data, word length);

