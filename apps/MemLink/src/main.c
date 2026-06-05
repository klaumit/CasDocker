#include <stdrom.h>
#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>

#define EVENT_TCH    1 
#define EVENT_CRADLE 4 
#define EVENT_BLD1   8
static void PollEvent(TCHSTS far* tsts, byte event_mask);

/* PV-touch screen event handling */
PollEvent(&tsts, EVENT_TCH | EVENT_CRADLE);

void wait()
{
	LibWait(IB_1SWAIT);
}

void writeln(char *txt, int x, int y)
{
	LibStringDsp( B@ txt, x, y, 160, B@@ IB_CG57FONT);
	LibPutDisp();
}

void hey1()
{
   /* PV init */
   LibTchStackClr();
   LibTchStackPush( NULL );
   LibTchStackPush( TchHardIcon );
   LibTchStackPush( TchStop );
   LibTchInit();
}

void main()
{
	char arr[20][128];
	int z = 0;

	LibInitDisp();
	LibClrDisp();

	while (1)
	{
		for (z = 0; z < 20; z++)
		{
			sprintf(arr[z], "%d - %d + %d", z, z, z);
			writeln(arr[z], 0, z * 8);
		}
		wait();
	}

	LibJumpMenu();
}

