FROM microsoft/dotnet:2.2-runtime

COPY bin/release/netcoreapp2.2/publish/ /app/bin/

ENV TZ UTC

CMD dotnet /app/bin/Linn.Production.Messaging.Host.dll