FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EfCoreDeepDive.API/EfCoreDeepDive.API.csproj", "EfCoreDeepDive.API/"]
RUN dotnet restore "EfCoreDeepDive.API/EfCoreDeepDive.API.csproj"
COPY . .
WORKDIR "/src/EfCoreDeepDive.API"
RUN dotnet build "EfCoreDeepDive.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EfCoreDeepDive.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EfCoreDeepDive.API.dll"]
