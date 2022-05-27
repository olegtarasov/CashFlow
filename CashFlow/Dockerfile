FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

ARG CERT_PASSWORD

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://*:443;http://*:80

ENV ASPNETCORE_Kestrel__Certificates__Default__Path=localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=$CERT_PASSWORD

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY data_depersonalized.db /app/publish/data.db
COPY certs/*.pfx /app/publish/
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
COPY certs/*.crt /usr/local/share/ca-certificates/
RUN /usr/sbin/update-ca-certificates
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.dll", "serve"]