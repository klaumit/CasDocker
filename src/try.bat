@echo off

call build.bat

set EXE=PvMake\bin\Debug\PvMake
set PRJ=Y:\apps\MemLink

echo.
echo  ::: Prepare :::
echo.
%EXE% --clean --prepare --build --simulate -i %PRJ%

echo 

cd ..

