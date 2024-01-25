#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PublishCore.Auth.Api/PublishCore.Auth.Api.csproj", "PublishCore.Auth.Api/"]
COPY ["PublishCore.Auth.Application/PublishCore.Auth.Application.csproj", "PublishCore.Auth.Application/"]
COPY ["PublishCore.Auth.Domain/PublishCore.Auth.Domain.csproj", "PublishCore.Auth.Domain/"]
COPY ["PublishCore.Auth.Infrastructure/PublishCore.Auth.Infrastructure.csproj", "PublishCore.Auth.Infrastructure/"]
COPY ["PublishCore.Auth.Utilities/PublishCore.Auth.Utilities.csproj", "PublishCore.Auth.Utilities/"]
COPY ["PublishCore.Auth.JWT/PublishCore.Auth.JWT.csproj", "PublishCore.Auth.JWT/"]
RUN dotnet restore "./PublishCore.Auth.Api/./PublishCore.Auth.Api.csproj"
COPY . .
WORKDIR "/src/PublishCore.Auth.Api"
RUN dotnet build "./PublishCore.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PublishCore.Auth.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublishCore.Auth.Api.dll"]