#!/bin/sh

sed -e "s/PocLink/MemLink/g" -e "s/Link2/Link1/g" ../PocLink/project.ini > ./project.ini

