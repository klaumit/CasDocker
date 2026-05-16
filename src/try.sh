#!/bin/sh

./build.sh

export WINEPREFIX=$PWD/win_env
export WINEARCH=win32
export WINEDEBUG=-all

cd win_build
echo winetricks vb6run

export EXE="wine pvmake"
export PRJ="%USERPROFILE%\Desktop\kak6\Sample"

echo 
echo  ::: Build ::: 
echo 
$EXE --build -i $PRJ

echo 
echo  ::: Simulate ::: 
echo 
$EXE --simulate -i $PRJ

echo 
echo  ::: Upload ::: 
echo 
$EXE --upload -i $PRJ

echo 

cd ..

