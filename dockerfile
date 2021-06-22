FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

WORKDIR /src

COPY ["dot_net.csproj", "src/dot_net/"]
COPY *.config .

WORKDIR "/src/src/dot_net"

WORKDIR /src

RUN dotnet restore "src/dot_net/dot_net.csproj" --disable-parallel
COPY . .
WORKDIR "/src/src/dot_net"

RUN dotnet build "dot_net.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dot_net.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV AppSettings__EnableHTTPS="false"

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dot_net.dll"]