FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EShop.Payment/EShop.Payment.csproj", "EShop.Payment/"]
COPY ["EShop.Common/EShop.Common.csproj", "EShop.Common/"]
RUN dotnet restore "EShop.Payment/EShop.Payment.csproj"
COPY . .
WORKDIR "/src/EShop.Payment"
RUN dotnet build "EShop.Payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Payment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Payment.dll"]