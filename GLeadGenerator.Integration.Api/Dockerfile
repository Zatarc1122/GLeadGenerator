#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BitMouse.LeadGenerator.Integration.Api/BitMouse.LeadGenerator.Integration.Api.csproj", "BitMouse.LeadGenerator.Integration.Api/"]
RUN dotnet restore "BitMouse.LeadGenerator.Integration.Api/BitMouse.LeadGenerator.Integration.Api.csproj"
COPY . .
WORKDIR "/src/BitMouse.LeadGenerator.Integration.Api"
RUN dotnet build "BitMouse.LeadGenerator.Integration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitMouse.LeadGenerator.Integration.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitMouse.LeadGenerator.Integration.Api.dll"]