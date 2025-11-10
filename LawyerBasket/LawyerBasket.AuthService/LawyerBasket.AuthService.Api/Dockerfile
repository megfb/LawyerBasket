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
COPY ["LawyerBasket.AuthService/LawyerBasket.AuthService.Api/LawyerBasket.AuthService.Api.csproj", "LawyerBasket.AuthService/LawyerBasket.AuthService.Api/"]
COPY ["LawyerBasket.AuthService/LawyerBasket.AuthService.Data/LawyerBasket.AuthService.Data.csproj", "LawyerBasket.AuthService/LawyerBasket.AuthService.Data/"]
COPY ["LawyerBasket.AuthService/LawyerBasket.AuthService.Application/LawyerBasket.AuthService.Application.csproj", "LawyerBasket.AuthService/LawyerBasket.AuthService.Application/"]
COPY ["LawyerBasket.Shared/LawyerBasket.Shared.Common/LawyerBasket.Shared.Common.csproj", "LawyerBasket.Shared/LawyerBasket.Shared.Common/"]
COPY ["LawyerBasket.AuthService/LawyerBasket.AuthService.Domain/LawyerBasket.AuthService.Domain.csproj", "LawyerBasket.AuthService/LawyerBasket.AuthService.Domain/"]
COPY ["LawyerBasket.AuthService/LawyerBasket.AuthService.Infrastructure/LawyerBasket.AuthService.Infrastructure.csproj", "LawyerBasket.AuthService/LawyerBasket.AuthService.Infrastructure/"]
RUN dotnet restore "./LawyerBasket.AuthService/LawyerBasket.AuthService.Api/LawyerBasket.AuthService.Api.csproj"
COPY . .
WORKDIR "/src/LawyerBasket.AuthService/LawyerBasket.AuthService.Api"
RUN dotnet build "./LawyerBasket.AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LawyerBasket.AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LawyerBasket.AuthService.Api.dll"]