FROM microsoft/dotnet:2.2-runtime

COPY bin/release/netcoreapp3.0/publish/ /app/bin/

ENV TZ UTC

CMD dotnet /app/bin/Linn.Production.Messaging.Host.dll