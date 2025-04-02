FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY backend.csproj Streaming-App/



RUN dotnet restore "Streaming-App/backend.csproj"

COPY . .

RUN dotnet publish "Streaming-App/backend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Streaming-App.dll"]
