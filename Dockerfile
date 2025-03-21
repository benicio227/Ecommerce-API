# Etapa 1: Imagem base com o runtime do .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa 2: Imagem do SDK do .NET 8 para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos do projeto e restaurar dependências
COPY ["EcommerceAPI/EcommerceAPI.csproj", "EcommerceAPI/"]
WORKDIR "/src/EcommerceAPI"
RUN dotnet restore

# Copiar todo o código restante e compilar o projeto
COPY . .
RUN dotnet build -c Release -o /app/build

# Etapa 3: Publicação do projeto
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Etapa 4: Imagem final com a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar a porta corretamente para o Railway
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]

