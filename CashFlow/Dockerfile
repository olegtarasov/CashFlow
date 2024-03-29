﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:80

WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY data_depersonalized.db /app/publish/data.db
COPY *.sln ./
COPY */*.csproj ./
RUN find *.csproj | sed -e 's/.csproj//g' | xargs mkdir && \
    find *.csproj | sed -r -e 's/((.+).csproj)/.\/\1 .\/\2/g' | xargs -I % sh -c 'mv %'

# Restore NuGet dependencies for target project and all dependencies
RUN dotnet restore "CashFlow/CashFlow.csproj"

# Copy all the other source files and publish the app.
COPY . .
WORKDIR "/src/CashFlow"
RUN dotnet publish "CashFlow.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.dll", "serve"]