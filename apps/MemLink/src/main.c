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
	
	LibStringDsp( B@ "### Memory Link ###", 5, 6, 160, B@@ IB_PFONT2);

	sprintf(arr[0], "###MEMORY_MARKER_START0###");
	sprintf(arr[1], "###UNIQUE_1234567890ABC###");
	sprintf(arr[2], "###MEMORY_MARKER_END0#####");
	sprintf(arr[3], "| Please do not replace  |");
	sprintf(arr[4], "| the text with anything |");
	sprintf(arr[5], "| that you would like!   |");	
	sprintf(arr[6], "| noon gig nun rotor dad |");
	sprintf(arr[7], "| boob peep kayak level  |");
	sprintf(arr[8], "| racecar eve mom stats  |");	
	sprintf(arr[9], "###MEMORY_MARKER_START1###");
	sprintf(arr[10], "###UNIQUE_1234567890ABC###");
	sprintf(arr[11], "###MEMORY_MARKER_END1#####");
	sprintf(arr[12], "| Ptr0 = %08X |", &arr);
	sprintf(arr[13], "| Ptr1 = %08X |", &arr[13]);
	
	while (1)
	{
		for (z = 0; z < 14; z++)
		{
			writeln(arr[z], 0, (z+3) * 8);
		}
		wait();
	}

	LibJumpMenu();
}
