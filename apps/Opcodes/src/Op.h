
#ifndef EXP_DEF
#define EXP_DEF
#include "define.h"

void exit_dos(void);
void print_char(short c);
void print_nl(void);

#ifdef __HITACHI__

#pragma inline_asm(nf_exit)
static int nf_exit()
{
    mov #76, R3
    mov #0, R4
    trapa #33
}

#pragma inline_asm(nf_char)
static int nf_char(int a)
{
    mov #2, R3
    trapa #33
}

#pragma inline_asm(nf_nl)
static int nf_nl()
{
    mov #2, R3
    mov #13, R4
    trapa #33
    mov #10, R4
    trapa #33
}

#else

#define nf_exit() _asm_nf_exit("\n mov ax, 4C00h \n int 21h \n")
void _asm_nf_exit();

#define nf_char(c) _asm_nf_char("\n mov dl, al \n mov ah, 02h \n int 21h \n", c)
void _asm_nf_char(unsigned char *, unsigned char);

#define nf_nl() _asm_nf_nl("\n mov ah, 02h \n mov dl, 0Dh \n int 21h \n mov dl, 0Ah \n int 21h \n")
void _asm_nf_nl();

#endif

#endif

