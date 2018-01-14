FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 8001

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY *.sln ./
COPY ItemsBasket.AuthenticationService/ItemsBasket.AuthenticationService.csproj ItemsBasket.AuthenticationService/
COPY ItemsBasket.Common/ItemsBasket.Common.csproj ItemsBasket.Common/
RUN dotnet restore
COPY . .
WORKDIR /src/ItemsBasket.AuthenticationService
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ItemsBasket.AuthenticationService.dll"]
