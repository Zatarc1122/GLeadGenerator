FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BitMouse.LeadGenerator.Api/BitMouse.LeadGenerator.Api.csproj", "BitMouse.LeadGenerator.Api/"]
RUN dotnet restore "BitMouse.LeadGenerator.Api/BitMouse.LeadGenerator.Api.csproj"
COPY . .
WORKDIR "/src/BitMouse.LeadGenerator.Api"
RUN dotnet build "BitMouse.LeadGenerator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitMouse.LeadGenerator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitMouse.LeadGenerator.Api.dll"]