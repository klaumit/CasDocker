#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>
#include "sysdm.h"
#include "msglayer.h"

void main()
{
	char debug[128];
	int i;
	int maxTry;

	LibInitDisp();
	LibClrDisp();
	
	LibStringDsp( B@ "### Pocket Link ###", 5, 10, 160, B@@ IB_PFONT2);

	sprintf(debug, "CPU and port: %s over %s", 
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind())
	);
	LibStringDsp( B@ debug, 5, 30, 160, B@@ IB_PFONT1);

	sprintf(debug, "Communication: %s", 
		GetOpenPortStr(OpenPort(GetCommKind()))
	);
	LibStringDsp( B@ debug, 5, 40, 160, B@@ IB_PFONT1);

	LibPutDisp();

	sprintf(debug, "app=PocLink;cpu=%s;comm=%s", 
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind())
	);
	SendTextMessage(MSG_HELLO, debug, 29);

	maxTry = 15;
	for (i = 0; i < maxTry; i++)
	{
		sprintf(debug, "Waiting %d of %d sec...", i, maxTry);
		LibStringDsp( B@ debug, 5, 100, 160, B@@ IB_PFONT2);
		LibPutDisp();
		LibWait(IB_1SWAIT);
	}

	ClosePort();
	LibJumpMenu();
}

