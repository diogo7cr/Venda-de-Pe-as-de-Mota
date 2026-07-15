# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo o projeto
COPY . .

# Restaurar dependências
RUN dotnet restore MotoPartsShop.csproj

# Publicar aplicação
RUN dotnet publish MotoPartsShop.csproj -c Release -o /app/publish

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "MotoPartsShop.dll"]
