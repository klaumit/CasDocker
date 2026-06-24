#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>
#include "sysdm.h"

void main()
{
	char debug[128];
	int i;

	LibInitDisp();
	LibClrDisp();
	
	LibStringDsp( B@ "### Pocket Link ###", 5, 10, 160, B@@ IB_PFONT2);

	sprintf(debug, "CPU and port: %s over %s", 
		GetCpuStr(GetCpuKind()), GetCommStr(GetCommKind())
	);
	LibStringDsp( B@ debug, 5, 30, 160, B@@ IB_PFONT1);

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
