FROM mcr.microsoft.com/dotnet/core/runtime:2.2

ARG gitBranch=unspecified

EXPOSE 5050

COPY bin/release/netcoreapp2.2/publish/ /app/bin/
COPY client/build/ /app/client/build/
# COPY client/assets/ /app/client/assets/
COPY views/ /app/views/
     
ENV APP_PATH /app/
ENV GIT_BRANCH $gitBranch
ENV TZ=Europe/London

CMD dotnet /app/bin/Linn.Production.Service.Host.dll