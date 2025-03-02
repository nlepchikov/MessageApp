FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MessageApp.csproj", "./"]
RUN dotnet restore "MessageApp.csproj"

COPY . .
RUN dotnet build "MessageApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageApp.csproj" -c Release -o /app/publish

# Используем официальный образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageApp.dll"]