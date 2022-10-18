
# BASE
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV ASPNETCORE_URLS=http://+:25020
EXPOSE 25020
WORKDIR /app

# BUILD
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

USER root

COPY . ./src

RUN pwd		
RUN ls ./src


RUN dotnet restore ./src/UserService/UserService.sln
RUN dotnet build ./src/UserService/UserService.sln -c Release --no-restore
RUN dotnet pack ./src/UserService/UserService.sln -c Release --no-build --output /source/packages

# PUBLISH
RUN echo "Starting publish"
FROM build AS publish
RUN dotnet publish "./src/UserService/UserService/UserService.API.csproj" -c Release --no-build -o /publish
RUN echo "End publish"

# PACKAGES
FROM scratch AS packages
COPY --from=build /source/packages .
CMD [""]

# FINAL
FROM base AS final
COPY --chown=ffuser:ffuser --from=publish /publish .
ENTRYPOINT ["dotnet", "UserService.API.dll"]
