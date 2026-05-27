@echo off

call build.bat

set EXE=PvMake\bin\Debug\PvMake
set PRJ=Y:\apps\Dumpy

echo.
echo  ::: Prepare :::
echo.
%EXE% --clean --prepare --build --simulate -i %PRJ%

echo.
echo  ::: Simulate :::
echo.
echo %EXE% --simulate -i %PRJ%

echo.
echo  ::: Upload :::
echo.
echo %EXE% --upload -i %PRJ%

echo.

