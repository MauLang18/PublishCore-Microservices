#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PublishCore.Hosted.Api/PublishCore.Hosted.Api.csproj", "PublishCore.Hosted.Api/"]
COPY ["PublishCore.Hosted.Utilities/PublishCore.Hosted.Utilities.csproj", "PublishCore.Hosted.Utilities/"]
RUN dotnet restore "./PublishCore.Hosted.Api/./PublishCore.Hosted.Api.csproj"
COPY . .
WORKDIR "/src/PublishCore.Hosted.Api"
RUN dotnet build "./PublishCore.Hosted.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PublishCore.Hosted.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublishCore.Hosted.Api.dll"]