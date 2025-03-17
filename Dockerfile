# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# run for the first time to create the image
# docker build -t my-dotnet8-webapp .
# docker run -d -p 8080:8080 -p 8081:8081 --name my-dotnet8-webapp my-dotnet8-webapp
# https://hbayraktar.medium.com/how-to-dockerize-a-net-8-asp-net-core-web-application-b15f63246535


# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Devops.Backend/Devops.Backend.csproj", "Devops.Backend/"]
RUN dotnet restore "./Devops.Backend/Devops.Backend.csproj"
COPY . .
WORKDIR "/src/Devops.Backend"
RUN dotnet build "./Devops.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Devops.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Devops.Backend.dll"]