# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar ficheiros .sln e projeto
COPY *.sln .
COPY MotoPartsShop/*.csproj ./MotoPartsShop/
RUN dotnet restore

# Copiar todo o código
COPY MotoPartsShop/. ./MotoPartsShop/
WORKDIR /src/MotoPartsShop

# Build em Release
RUN dotnet publish -c Release -o /app

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Expor porta 80
EXPOSE 80

# Iniciar app
ENTRYPOINT ["dotnet", "MotoPartsShop.dll"]