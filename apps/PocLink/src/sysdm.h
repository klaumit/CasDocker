
byte GetCommKind(void);
const char *GetCommStr(byte num);

word GetCommState(void);
const char *GetStateStr(word num);

byte GetKeyLayout(void);
const char *GetLayoutStr(byte num);

byte GetLanguage(void);
const char *GetLangStr(byte num);

word GetAPOTimeMs(void);

byte GetLangSupport(void);
const char *GetLangSuppStr(byte num);

byte GetCpuKind(void);
const char *GetCpuStr(byte num);

word OpenPort(byte port);
const char *GetOpenPortStr(word num);

void ClosePort();

void Wait(void);

