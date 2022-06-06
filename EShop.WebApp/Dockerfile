FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EShop.WebApp/EShop.WebApp.csproj", "EShop.WebApp/"]
COPY ["EShop.Common/EShop.Common.csproj", "EShop.Common/"]
RUN dotnet restore "EShop.WebApp/EShop.WebApp.csproj"
COPY . .
WORKDIR "/src/EShop.WebApp"
RUN dotnet build "EShop.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.WebApp.dll"]