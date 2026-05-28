#include <define.h>
#include <libc.h>
#include <l_libc.h>
#include <stdio.h>
#include <string.h>

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
	char tmp[128];
	int i = 0;
	int z = 0;

	LibInitDisp();
	LibClrDisp();

	while (1)
	{
		i = i + 1;
		sprintf(tmp, "%d - %d - %d", i, i, i);
		writeln(tmp, 0, z);
		wait();

		z = z + 8;
		if (z > (160-8))
			z = 0;
	}

	LibJumpMenu();
}
