#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sayranet.ODataEFCore.WebApi/Sayranet.ODataEFCore.WebApi.csproj", "Sayranet.ODataEFCore.WebApi/"]
RUN dotnet restore "Sayranet.ODataEFCore.WebApi/Sayranet.ODataEFCore.WebApi.csproj"
COPY . .
WORKDIR "/src/Sayranet.ODataEFCore.WebApi"
RUN dotnet build "Sayranet.ODataEFCore.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sayranet.ODataEFCore.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sayranet.ODataEFCore.WebApi.dll"]