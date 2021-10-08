FROM mcr.microsoft.com/dotnet/core/runtime:2.2

COPY bin/release/netcoreapp2.2/publish/ /app/bin/

ENV TZ UTC

CMD dotnet /app/bin/Linn.Production.Messaging.Host.dll