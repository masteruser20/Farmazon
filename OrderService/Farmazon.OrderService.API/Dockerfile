FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Farmazon.OrderService.API/Farmazon.OrderService.API.csproj", "Farmazon.OrderService.API/"]
RUN dotnet restore "Farmazon.OrderService.API/Farmazon.OrderService.API.csproj"
COPY . .
WORKDIR "/src/Farmazon.OrderService.API"
RUN dotnet build "Farmazon.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Farmazon.OrderService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Farmazon.OrderService.API.dll"]
