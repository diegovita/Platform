

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src



COPY ["BloggingPlatform/BloggingPlatform.csproj", "BloggingPlatform/"]
RUN dotnet restore "./BloggingPlatform/./BloggingPlatform.csproj"
COPY . .
WORKDIR "/src/BloggingPlatform"
RUN dotnet build "./BloggingPlatform.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test "./BloggingPlatform.csproj" -c $BUILD_CONFIGURATION --no-build --verbosity normal

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BloggingPlatform.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet","BloggingPlatform.dll"]