FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EShop.Ordering/EShop.Ordering.csproj", "EShop.Ordering/"]
COPY ["EShop.Common/EShop.Common.csproj", "EShop.Common/"]
RUN dotnet restore "EShop.Ordering/EShop.Ordering.csproj"
COPY . .
WORKDIR "/src/EShop.Ordering"
RUN dotnet build "EShop.Ordering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Ordering.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Ordering.dll"]