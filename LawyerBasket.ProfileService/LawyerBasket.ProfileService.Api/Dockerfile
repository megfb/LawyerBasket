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
COPY ["LawyerBasket.ProfileService/LawyerBasket.ProfileService.Api/LawyerBasket.ProfileService.Api.csproj", "LawyerBasket.ProfileService/LawyerBasket.ProfileService.Api/"]
COPY ["LawyerBasket.ProfileService/LawyerBasket.ProfileService.Data/LawyerBasket.ProfileService.Data.csproj", "LawyerBasket.ProfileService/LawyerBasket.ProfileService.Data/"]
COPY ["LawyerBasket.ProfileService/LawyerBasket.ProfileService.Application/LawyerBasket.ProfileService.Application.csproj", "LawyerBasket.ProfileService/LawyerBasket.ProfileService.Application/"]
COPY ["LawyerBasket.ProfileService/LawyerBasket.ProfileService.Domain/LawyerBasket.ProfileService.Domain.csproj", "LawyerBasket.ProfileService/LawyerBasket.ProfileService.Domain/"]
COPY ["LawyerBasket.Shared/LawyerBasket.Shared.Common/LawyerBasket.Shared.Common.csproj", "LawyerBasket.Shared/LawyerBasket.Shared.Common/"]
COPY ["LawyerBasket.ProfileService/LawyerBasket.ProfileService.Infrastructure/LawyerBasket.ProfileService.Infrastructure.csproj", "LawyerBasket.ProfileService/LawyerBasket.ProfileService.Infrastructure/"]
RUN dotnet restore "./LawyerBasket.ProfileService/LawyerBasket.ProfileService.Api/LawyerBasket.ProfileService.Api.csproj"
COPY . .
WORKDIR "/src/LawyerBasket.ProfileService/LawyerBasket.ProfileService.Api"
RUN dotnet build "./LawyerBasket.ProfileService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LawyerBasket.ProfileService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LawyerBasket.ProfileService.Api.dll"]