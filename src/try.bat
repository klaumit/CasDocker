@echo off

call build.bat

set EXE=PvMake\bin\Debug\fx40\PvMake
set PRJ=Y:\apps\PocLink

echo.
echo  ::: Prepare :::
echo.
REM echo --simulate 
%EXE% --clean --prepare --build --upload -i %PRJ%

echo 


