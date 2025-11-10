# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LawyerBasket.SocialService/LawyerBasket.SocialService.Api/LawyerBasket.SocialService.Api.csproj", "LawyerBasket.SocialService/LawyerBasket.SocialService.Api/"]
COPY ["LawyerBasket.Shared/LawyerBasket.Shared.Common/LawyerBasket.Shared.Common.csproj", "LawyerBasket.Shared/LawyerBasket.Shared.Common/"]
RUN dotnet restore "./LawyerBasket.SocialService/LawyerBasket.SocialService.Api/LawyerBasket.SocialService.Api.csproj"
COPY . .
WORKDIR "/src/LawyerBasket.SocialService/LawyerBasket.SocialService.Api"
RUN dotnet build "./LawyerBasket.SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LawyerBasket.SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LawyerBasket.SocialService.Api.dll"]