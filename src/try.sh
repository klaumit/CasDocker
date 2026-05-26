#!/bin/sh

./build.sh

export WINEPREFIX=$PWD/win_env
export WINEARCH=win32
export WINEDEBUG=-all

cd win_build
echo winetricks vb6run

export EXE="wine pvmake"
export PRJ="%USERPROFILE%\Projects\Sample"

echo 
echo  ::: Prepare ::: 
echo 
$EXE --clean --prepare --build -i $PRJ

echo 
echo  ::: Simulate ::: 
echo 
echo $EXE --simulate -i $PRJ

echo 
echo  ::: Upload ::: 
echo 
echo $EXE --upload -i $PRJ

echo 

cd ..

