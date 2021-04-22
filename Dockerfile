    #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["src/TechChallenge/TechChallenge.csproj", "TechChallenge/TechChallenge.csproj"]
COPY ["src/TechChallenge.Application/TechChallenge.Application.csproj", "TechChallenge.Application/TechChallenge.Application.csproj"]
COPY ["src/TechChallenge.Data/TechChallenge.Data.csproj", "TechChallenge.Data/TechChallenge.Data.csproj"]
COPY ["src/TechChallenge.Infra/TechChallenge.Infra.csproj", "TechChallenge.Infra/TechChallenge.Infra.csproj"]


RUN dotnet restore "TechChallenge/TechChallenge.csproj"

COPY src/. .

WORKDIR /src/TechChallenge
RUN dotnet build "TechChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechChallenge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge.dll"]