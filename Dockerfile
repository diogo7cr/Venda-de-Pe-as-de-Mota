# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar todo o projeto
COPY . .

# Restaurar dependências
RUN dotnet restore MotoPartsShop.csproj

# Publicar aplicação
RUN dotnet publish MotoPartsShop.csproj -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Copiar a aplicação publicada
COPY --from=build /app/publish .

# Expor a porta
EXPOSE 80

# Iniciar a aplicação
ENTRYPOINT ["dotnet", "MotoPartsShop.dll"]
