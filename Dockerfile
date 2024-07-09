

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY *.sln ./
COPY ["BloggingPlatform/BloggingPlatform.csproj", "BloggingPlatform/"]
COPY ["BloggingPlatform.Tests/BloggingPlatform.Tests.csproj", "BloggingPlatform.Tests/"]
RUN dotnet restore


COPY . .

RUN dotnet build --configuration Release

WORKDIR "/src/BloggingPlatform.Tests"
RUN dotnet test --configuration Release

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet","BloggingPlatform.dll"]