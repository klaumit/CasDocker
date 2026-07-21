#!/bin/sh

sed -e "s/PocLink/MemLink/g" -e "s/Link2/Link1/g" ../PocLink/project.ini > ./project.ini

cp ../PocLink/src/sysdm.*     ./src/
cp ../PocLink/src/xhacks.*    ./src/
cp ../PocLink/src/msglayer.*  ./src/

sed -e "s/Pocket Link/Memory Link/g" -e "s/PocLink/MemLink/g" -e "s/15/999/g" -e "s/25/200/g" -e "s/21/180/g" ../PocLink/src/main.c > ./src/main.c
sed -e "s/LibSrl/MmLink/g" ../PocLink/src/msglayer.c > ./src/msglayer.c
sed -e "s/LibSrl/MmLink/g" ../PocLink/src/sysdm.c > ./src/sysdm.c

