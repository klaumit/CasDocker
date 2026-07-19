#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>

#ifdef __HITACHI__
#else
    #include <stdrom.h>
#endif

void wait()
{
	LibWait(IB_1SWAIT);
}

void writeln(char *txt, int x, int y)
{
	LibStringDsp( B@ txt, x, y, 160, B@@ IB_CG57FONT);
	LibPutDisp();
}

void main()
{
	char arr[20][128];
	int z = 0;

	LibInitDisp();
	LibClrDisp();
	
	LibStringDsp( B@ "### Memory Link ###", 5, 10, 160, B@@ IB_PFONT2);

	while (1)
	{
		for (z = 3; z < 20; z++)
		{
			sprintf(arr[z], "%d - %d + %d", z, z, z);
			writeln(arr[z], 0, z * 8);
		}
		wait();
	}

	LibJumpMenu();
}

