# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Tnosc.OtripleS.Client.Web/Tnosc.OtripleS.Client.Web.csproj", "src/Tnosc.OtripleS.Client.Web/"]
COPY ["lib/Tnosc.Lib.Client.Web/Tnosc.Lib.Client.Web.csproj", "lib/Tnosc.Lib.Client.Web/"]
COPY ["src/Tnosc.OtripleS.Client.Web.Client/Tnosc.OtripleS.Client.Web.Client.csproj", "src/Tnosc.OtripleS.Client.Web.Client/"]
COPY ["src/Tnosc.OtripleS.Client.Application/Tnosc.OtripleS.Client.Application.csproj", "src/Tnosc.OtripleS.Client.Application/"]
COPY ["src/Tnosc.OtripleS.Client.Domain/Tnosc.OtripleS.Client.Domain.csproj", "src/Tnosc.OtripleS.Client.Domain/"]
COPY ["src/Tnosc.OtripleS.Client.Infrastructure/Tnosc.OtripleS.Client.Infrastructure.csproj", "src/Tnosc.OtripleS.Client.Infrastructure/"]
RUN dotnet restore "./src/Tnosc.OtripleS.Client.Web/Tnosc.OtripleS.Client.Web.csproj"
COPY . .
WORKDIR "/src/src/Tnosc.OtripleS.Client.Web"
RUN dotnet build "./Tnosc.OtripleS.Client.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tnosc.OtripleS.Client.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tnosc.OtripleS.Client.Web.dll"]