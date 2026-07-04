#!/bin/sh

dotnet build DevForge.slnx

dotnet publish -f net10.0-windows -r win-x86 --self-contained true -o wdf_build DevForge/DevForge.n.csproj

