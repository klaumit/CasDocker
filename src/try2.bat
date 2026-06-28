@echo off

set EXE=PvMake\bin\Debug\net40\PvMake
set PRJ=Y:\apps\PocLink

echo.
echo  ::: Prepare :::
echo.
%EXE% --clean --prepare --build -i %PRJ%

echo.


