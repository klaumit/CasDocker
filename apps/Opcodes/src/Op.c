#include "libc.h"
#include "Op.h"

void main(void)
{
	volatile signed int a = 0x4A;
    volatile unsigned int b = 0x12;
	volatile int xa[4] = {0x2B,35,75,100};
    print_char(a - b); /* 8 */

    a = 0x1A;
    b = 0x1D;
    print_char(a + b); /* 7 */

    a = 0x1B;
    b = 0x02;
    print_char(a * b); /* 6 */

    a = 0x34;
    print_char(++a); /* 5 */
    print_char(--a); /* 4 */

    a = 0x38;
    b = 0x39;
    print_char(a & b); /* 8 */

    a = 0xAD;
    print_char(~a); /* R */

    a = 0x3C;
    b = 0x1B;
    print_char((a % b) + 0x30); /* 6 */

    a = 0x75;
    b = 0x03;
    print_char((a / b) + 0x10); /* 7 */

    a = 0x07;
    b = 0x03;
    print_char(a << b); /* 8 */

    a = 0xD4;
    b = 0x02;
    print_char(a >> b); /* 5 */

    a = 0xAB;
    b = 0x3F;
    print_char(a < b ? 'Y' : 'N');
    print_char(a > b ? 'Y' : 'N');
    print_char(a <= b ? 'Y' : 'N');
    print_char(a >= b ? 'Y' : 'N');
    print_char(a == b ? 'Y' : 'N');
    print_char(a != b ? 'Y' : 'N');

    a = 0xAB;
    b = 0x3F;
    print_char((a ^ b) - 590); /* F */

    a = 0xAB;
    b = 0x3F;
    print_char((a | b) - 593); /* n */

    print_char((a >= 10 && b >= 0) ? 'Y' : 'N');
    print_char((a > 5776 || b < 14) ? 'Y' : 'N');

    print_char((!(a > b)) ? 'Y' : 'N');

	if (a >= b){
		print_char('1'); /* 1 */
	} else {
		print_char('2');
	}
	
	a = 0x91;
	switch (a) {
		case 0x91:
			print_char('S'); /* S */
			break;
		default:
			print_char('F');
			break;
	}
	
	a = 3;
	while (a > 0) {
		print_char('0' + (a--)); /* 3,2,1 */
	}
	
	a = 4;
	do {
		print_char('A' + (a+=2)); /* G,I,K,M */
	} while (a < 11);
	
	a = 0x1234;
	b = 0;
	
	while (a) {
		b = b * 10 + a % 10;
		a /= 10;
	}
	print_char((b >> 8) + 0x30); /* 2 */
	
	a = 4;
	for (a = 0; a < 8; a+=3)
		print_char('0' + a); /* 0,3,6 */
	
	b = 0;
	#ifdef __HITACHI__
		b = 8;
	#endif

	print_char('0'+sizeof(xa)-b); /* 8 */

	b = 0;
	#ifdef __HITACHI__
		b = 2;
	#endif
	
	print_char('A'+sizeof(xa[1])-b); /* C */
	xa[1] = 0x33;
	print_char('B'+sizeof(xa[0])-b); /* D */
	
    exit_dos();
}

void exit_dos(void)
{
    nf_exit();
}

void print_nl(void)
{
    nf_nl();
}

void print_char(short c)
{
    nf_char(c);
}
