FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/APIGatewayMovies/APIGatewayMovies.csproj", "ApiGateway/APIGatewayMovies/"]
RUN dotnet restore "ApiGateway/APIGatewayMovies/APIGatewayMovies.csproj"
COPY . .
WORKDIR "/src/ApiGateway/APIGatewayMovies"
RUN dotnet build "APIGatewayMovies.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGatewayMovies.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGatewayMovies.dll"]

