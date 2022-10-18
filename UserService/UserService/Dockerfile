#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UserService/UserService.API.csproj", "UserService/"]
COPY ["UserService.Model/UserService.Model.csproj", "UserService.Model/"]
COPY ["UserService.Repository/UserService.Repository.csproj", "UserService.Repository/"]
COPY ["UserService.Service/UserService.Service.csproj", "UserService.Service/"]
RUN dotnet restore "UserService/UserService.API.csproj"
COPY . .
WORKDIR "/src/UserService"
RUN dotnet build "UserService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.API.dll"]