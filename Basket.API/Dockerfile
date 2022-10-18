#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Basket.API/Basket.API.csproj", "Basket.API/"]
COPY ["Basket.Logger/Basket.Logger.csproj", "Basket.Logger/"]
COPY ["Basket.Middleware/Basket.Middleware.csproj", "Basket.Middleware/"]
COPY ["Basket.Service/Basket.Service.csproj", "Basket.Service/"]
COPY ["Basket.Data/Basket.Data.csproj", "Basket.Data/"]
COPY ["Basket.Model/Basket.Entity.csproj", "Basket.Model/"]
COPY ["Store/Basket.Store.csproj", "Store/"]
RUN dotnet restore "Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
