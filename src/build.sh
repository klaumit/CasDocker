#!/bin/sh

dotnet build PvMake.slnx

dotnet publish -f net10.0-windows -r win-x86 --self-contained true -o win_build PvMake/PvMake.n.csproj

