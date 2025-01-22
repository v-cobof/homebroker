# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo .csproj e restaurar dependências
COPY Homebroker.csproj ./
RUN dotnet restore Homebroker.csproj

# Copiar todo o código-fonte
COPY . ./

# Compilar o projeto
RUN dotnet build Homebroker.csproj -c Release -o /app/build

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/build .

# Expor a porta padrão do ASP.NET Core
EXPOSE 80

# Comando de inicialização
ENTRYPOINT ["dotnet", "Homebroker.dll"]
