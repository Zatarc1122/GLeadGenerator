#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["GLeadGenerator.Integration.Api/GLeadGenerator.Integration.Api.csproj", "GLeadGenerator.Integration.Api/"]
RUN dotnet restore "GLeadGenerator.Integration.Api/GLeadGenerator.Integration.Api.csproj"
COPY . .
WORKDIR "/src/GLeadGenerator.Integration.Api"
RUN dotnet build "GLeadGenerator.Integration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GLeadGenerator.Integration.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GLeadGenerator.Integration.Api.dll"]