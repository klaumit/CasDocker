FROM ubuntu:noble

RUN dpkg --add-architecture i386 \
 && apt-get update \
 && apt-get install -y --no-install-recommends \
    wine32:i386 wine tree \
 && rm -rf /var/lib/apt/lists/*

ENV WINEDEBUG=-all
ENV WINEPREFIX=/win

ADD ./win/PV450X.tar.gz  /win/drive_c/
ADD ./win/PV750.tar.gz   /win/drive_c/
ADD ./win/PV750p.tar.gz  /win/drive_c/
ADD ./win/PVS450.tar.gz  /win/drive_c/
ADD ./win/PVS660.tar.gz  /win/drive_c/
ADD ./win/PVS600.tar.gz  /win/drive_c/
ADD ./win/PVS1600.tar.gz /win/drive_c/

RUN ln -s /src /win/drive_c/CASIO/PV3S1600/Ext \
 && ln -s /src /win/drive_c/CASIO/PV2EN03/C/Ext


