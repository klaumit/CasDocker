
#ifdef __HITACHI__

#define SwitchBank(addr,bank) 

typedef void (*FarFun)(void);

#else

/*
 * SwitchBank (from GetOS2 v1.20 by J. Steingraeber)
 *
 * makes RAM or FLASH memory accessible at certain bank
 *
 * addr A31-A16 of source address (must be even)
 *      use 0000 - 00FE for RAM (only 0000 will work)
 *      use 0100 - 010E for program flash (1st MB, PVOS)
 *      use 0110 - 011E for program flash (2nd MB, Add-In)
 *      use 0120 - 013E for data flash (4MB data)
 *
 * bank destination bank
 *      use 0 for address space 0000:0000 (forbidden)
 *      use 1 for address space 2000:0000
 *      use 2 for address space 4000:0000
 *      use 3 for address space 6000:0000
 *      use 4 for address space 8000:0000 (forbidden)
 *      use 5 for address space A000:0000
 *      use 6 for address space C000:0000 (forbidden)
 *
 * remarks:
 *   you can not switch bank 0, 4 and 6, because that will crash your
 *   PV; bank 0 contains the interrupt vectors, and every program
 *   expects to find RAM here; bank 4 contains your Add-In, so
 *   switching this would make your Add-In vanishing; bank 6 contains
 *   the BIOS, your PV can not operate without it
 *
 * examples:
 *
 *   SwitchBank(0x0000,3);
 *     will make 128kB RAM accessible at address 6000:0000 so RAM can
 *     be accessed at 0000:0000 and 6000:0000 in the same way (which
 *     is possible but stupid, you still have only 128kB of RAM)
 *
 *   SwitchBank(0x0104,5);
 *     will make Fonts accessible at address A000:0000 and Graphics
 *     accessible at B000:0000
 *
 *   SwitchBank(0x0120,1);
 *     will make first 128kB of data flash accessible at 2000:0000
 *
 */
void _asm_switchbank(char *,int,int);
#define SwitchBank(addr,bank) _asm_switchbank("\n MOV BH,AL\n MOV AL,AH\n MOV AH,BL\n OR AH,80h\n INT 0C8h\n MOV AL,BH\n MOV AH,BL\n INT 0C8h\n",addr,bank);

unsigned int _asm_getipseg(char *);
#define GetIPSeg() _asm_getipseg("\n PUSH CS\n POP AX\n")

unsigned int _asm_getipoff(char *);
#define GetIPOff() _asm_getipoff("\n CALL $+3\n POP AX\n")

typedef void (far *FarFun)(void);

#endif

unsigned long GetIP(void);

