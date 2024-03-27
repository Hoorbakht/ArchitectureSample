#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ArchitectureSample.Application.Api/ArchitectureSample.Application.Api.csproj", "ArchitectureSample.Application.Api/"]
RUN dotnet restore "./ArchitectureSample.Application.Api/ArchitectureSample.Application.Api.csproj"
COPY . .
WORKDIR "/src/ArchitectureSample.Application.Api"
RUN dotnet build "./ArchitectureSample.Application.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ArchitectureSample.Application.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchitectureSample.Application.Api.dll"]