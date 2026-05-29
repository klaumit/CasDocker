#!/bin/sh

./build.sh

export WINEPREFIX=$PWD/win_env
export WINEARCH=win32
export WINEDEBUG=-all

cd win_build
# winetricks vb6run

export EXE="wine pvmake"
export PRJ="%USERPROFILE%\Projects\MemLink"

echo 
echo  ::: Prepare ::: 
echo 
$EXE --clean --prepare --build --simulate -i $PRJ

echo 

cd ..

