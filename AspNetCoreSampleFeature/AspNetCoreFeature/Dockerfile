FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AspNetCoreFeature.csproj", "AspNetCoreFeature/"]
RUN dotnet restore "AspNetCoreFeature/AspNetCoreFeature.csproj"

WORKDIR "/src/AspNetCoreFeature"
COPY . .
RUN dotnet build "AspNetCoreFeature.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AspNetCoreFeature.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreFeature.dll"]
