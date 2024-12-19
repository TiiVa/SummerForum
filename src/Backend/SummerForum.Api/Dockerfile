#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Backend/SummerForum.Api/SummerForum.Api.csproj", "src/Backend/SummerForum.Api/"]
COPY ["src/Backend/SummerForum.Api.DataAccess/SummerForum.Api.DataAccess.csproj", "src/Backend/SummerForum.Api.DataAccess/"]
COPY ["src/Frontend/SummerForum.Client/SummerForum.Client.csproj", "src/Frontend/SummerForum.Client/"]
COPY ["src/Shared/SummerForum.CommonInterfaces/SummerForum.CommonInterfaces.csproj", "src/Shared/SummerForum.CommonInterfaces/"]
COPY ["src/Shared/SummerForum.DataTransferContract/SummerForum.DataTransferContract.csproj", "src/Shared/SummerForum.DataTransferContract/"]
RUN dotnet restore "./src/Backend/SummerForum.Api/SummerForum.Api.csproj"
COPY . .
WORKDIR "/src/src/Backend/SummerForum.Api"
RUN dotnet build "./SummerForum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SummerForum.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SummerForum.Api.dll"]