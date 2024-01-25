#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PublishCore.Gateway.Api/PublishCore.Gateway.Api.csproj", "PublishCore.Gateway.Api/"]
RUN dotnet restore "./PublishCore.Gateway.Api/./PublishCore.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/PublishCore.Gateway.Api"
RUN dotnet build "./PublishCore.Gateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PublishCore.Gateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublishCore.Gateway.Api.dll"]