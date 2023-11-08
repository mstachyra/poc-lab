#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RebusLab.ModuleA/RebusLab.ModuleA.csproj", "RebusLab.ModuleA/"]
RUN dotnet restore "RebusLab.ModuleA/RebusLab.ModuleA.csproj"
COPY . .
WORKDIR "/src/RebusLab.ModuleA"
RUN dotnet build "RebusLab.ModuleA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RebusLab.ModuleA.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RebusLab.ModuleA.dll"]