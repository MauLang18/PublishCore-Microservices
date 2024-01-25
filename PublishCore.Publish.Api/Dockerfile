#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PublishCore.Publish.Api/PublishCore.Publish.Api.csproj", "PublishCore.Publish.Api/"]
COPY ["PublishCore.Auth.JWT/PublishCore.Auth.JWT.csproj", "PublishCore.Auth.JWT/"]
COPY ["PublishCore.Publish.Application/PublishCore.Publish.Application.csproj", "PublishCore.Publish.Application/"]
COPY ["PublishCore.Publish.Domain/PublishCore.Publish.Domain.csproj", "PublishCore.Publish.Domain/"]
COPY ["PublishCore.Publish.Infrastructure/PublishCore.Publish.Infrastructure.csproj", "PublishCore.Publish.Infrastructure/"]
COPY ["PublishCore.Publish.Utilities/PublishCore.Publish.Utilities.csproj", "PublishCore.Publish.Utilities/"]
RUN dotnet restore "./PublishCore.Publish.Api/./PublishCore.Publish.Api.csproj"
COPY . .
WORKDIR "/src/PublishCore.Publish.Api"
RUN dotnet build "./PublishCore.Publish.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PublishCore.Publish.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublishCore.Publish.Api.dll"]