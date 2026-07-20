#!/bin/sh

sed -e "s/PocLink/MemLink/g" -e "s/Link2/Link1/g" ../PocLink/project.ini > ./project.ini

cp ../PocLink/src/sysdm.*     ./src/
cp ../PocLink/src/xhacks.*    ./src/
cp ../PocLink/src/msglayer.*  ./src/

sed -e "s/Pocket Link/Memory Link/g" -e "s/15/999/g" ../PocLink/src/main.c > ./src/main.c

