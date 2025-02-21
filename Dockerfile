FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["N5_Challenge/N5_Challenge.csproj", "N5_Challenge/"]
RUN dotnet restore "./N5_Challenge/N5_Challenge.csproj"
COPY . .
WORKDIR "/src/N5_Challenge"
RUN dotnet build "./N5_Challenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./N5_Challenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5_Challenge.dll"]