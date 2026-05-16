@echo off

call build.bat

set EXE=PvMake\bin\Debug\PvMake
set PRJ=Z:\kak6

echo.
echo  ::: Build ::: 
echo.
%EXE% --build -i %PRJ%

echo.
echo  ::: Simulate ::: 
echo.
%EXE% --simulate -i %PRJ%

echo.
echo  ::: Upload ::: 
echo.
%EXE% --upload -i %PRJ%

echo.
