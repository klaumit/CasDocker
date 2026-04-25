#!/bin/sh

docker build . -f ./Dockerfile -t pocketwin

mkdir mnt

docker run -v $PWD/mnt:/src -it --rm pocketwin


