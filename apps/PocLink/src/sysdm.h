
byte GetCommKind(void);
const char *GetCommStr(byte num);

word GetCommState(void);
const char *GetStateStr(word num);

byte GetCpuKind(void);
const char *GetCpuStr(byte num);

word OpenPort(byte port);
const char *GetOpenPortStr(word num);

void ClosePort();

void Wait(void);

