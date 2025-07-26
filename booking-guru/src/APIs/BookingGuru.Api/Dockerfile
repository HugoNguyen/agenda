# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIs/BookingGuru.Api/BookingGuru.Api.csproj", "APIs/BookingGuru.Api/"]
COPY ["Modules/Mocks/BookingGuru.Modules.Mocks.Infrastructure/BookingGuru.Modules.Mocks.Infrastructure.csproj", "Modules/Mocks/BookingGuru.Modules.Mocks.Infrastructure/"]
COPY ["Common/BookingGuru.Common.Infrastructure/BookingGuru.Common.Infrastructure.csproj", "Common/BookingGuru.Common.Infrastructure/"]
COPY ["Common/BookingGuru.Common.Application/BookingGuru.Common.Application.csproj", "Common/BookingGuru.Common.Application/"]
COPY ["Common/BookingGuru.Common.Domain/BookingGuru.Common.Domain.csproj", "Common/BookingGuru.Common.Domain/"]
COPY ["Modules/Mocks/BookingGuru.Modules.Mocks.Application/BookingGuru.Modules.Mocks.Application.csproj", "Modules/Mocks/BookingGuru.Modules.Mocks.Application/"]
COPY ["Modules/Mocks/BookingGuru.Modules.Mocks.Domain/BookingGuru.Modules.Mocks.Domain.csproj", "Modules/Mocks/BookingGuru.Modules.Mocks.Domain/"]
COPY ["Modules/Mocks/BookingGuru.Modules.Mocks.IntegrationEvents/BookingGuru.Modules.Mocks.IntegrationEvents.csproj", "Modules/Mocks/BookingGuru.Modules.Mocks.IntegrationEvents/"]
COPY ["Modules/Mocks/BookingGuru.Modules.Mocks.Presentation/BookingGuru.Modules.Mocks.Presentation.csproj", "Modules/Mocks/BookingGuru.Modules.Mocks.Presentation/"]
COPY ["Common/BookingGuru.Common.Presentation/BookingGuru.Common.Presentation.csproj", "Common/BookingGuru.Common.Presentation/"]
RUN dotnet restore "./APIs/BookingGuru.Api/BookingGuru.Api.csproj"
COPY . .
WORKDIR "/src/APIs/BookingGuru.Api"
RUN dotnet build "./BookingGuru.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookingGuru.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingGuru.Api.dll"]