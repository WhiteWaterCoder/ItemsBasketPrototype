FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 8002

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY *.sln ./
COPY ItemsBasket.BasketService/ItemsBasket.BasketService.csproj ItemsBasket.BasketService/
RUN dotnet restore
COPY . .
WORKDIR /src/ItemsBasket.BasketService
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ItemsBasket.BasketService.dll"]
