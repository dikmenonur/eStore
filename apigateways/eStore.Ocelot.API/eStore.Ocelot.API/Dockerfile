#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apigateways/eStore.Ocelot.API/eStore.Ocelot.API/eStore.Ocelot.API.csproj", "apigateways/eStore.Ocelot.API/eStore.Ocelot.API/"]
RUN dotnet restore "apigateways/eStore.Ocelot.API/eStore.Ocelot.API/eStore.Ocelot.API.csproj"
COPY . .
WORKDIR "/src/apigateways/eStore.Ocelot.API/eStore.Ocelot.API"
RUN dotnet build "eStore.Ocelot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eStore.Ocelot.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eStore.Ocelot.API.dll"]