FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj (cache de restore)
COPY Backend.csproj ./
RUN dotnet restore

# Copia o restante do código
COPY . .

# Build / Publish
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Backend.dll"]
