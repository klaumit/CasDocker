@echo off

call build.bat

set EXE=PvMake\bin\Debug\fx40\PvMake
set PRJ=Y:\apps\Hello

echo.
echo  ::: Prepare :::
echo.
REM echo --simulate 
%EXE% --clean --prepare --build -i %PRJ%

echo 

cd ..

