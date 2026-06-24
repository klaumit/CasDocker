#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>

void main()
{
	char debug[128];
	int i;

	LibInitDisp();
	LibClrDisp();

	LibStringDsp( B@ "Hello World!!!", 5, 10, 160, B@@ IB_PFONT2);
	LibStringDsp( B@ "Your device is fucked", 5, 30, 160, B@@ IB_PFONT1);
	LibStringDsp( B@ "by the mafia", 5, 40, 160, B@@ IB_PFONT2);
	LibStringDsp( B@ "http://www.mafia.com.uk", 5, 60, 160, B@@ IB_PFONT2);
	LibStringDsp( B@ "admin@nonsense.com.uk", 5, 70, 160, B@@ IB_PFONT2);
	LibPutDisp();

	for (i = 0; i < 100; i++)
	{
		sprintf(debug, "Waiting %d sec...", i);
		LibStringDsp( B@ debug, 5, 100, 160, B@@ IB_PFONT2);
		LibPutDisp();
		LibWait(IB_1SWAIT);
	}
	LibJumpMenu();
}

