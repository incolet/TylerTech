FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TylerTechnologies.Api/TylerTechnologies.Api.csproj", "TylerTechnologies.Api/"]
COPY ["TylerTechnologies.Core/TylerTechnologies.Core.csproj", "TylerTechnologies.Core/"]
COPY ["TylerTechnologies.Infrastructure/TylerTechnologies.Infrastructure.csproj", "TylerTechnologies.Infrastructure/"]

RUN dotnet restore "TylerTechnologies.Api/TylerTechnologies.Api.csproj"

COPY . .
WORKDIR "/src/TylerTechnologies.Api"
RUN dotnet build "TylerTechnologies.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TylerTechnologies.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TylerTechnologies.Api.dll"]
