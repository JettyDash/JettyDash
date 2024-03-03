FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/Api.csproj", "Api/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Schemes/Schemes.csproj", "Schemes/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]





#
# First stage: Building the application using .NET Core SDK
#FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
#WORKDIR /app
#
## Copy solution file and restore solution-level dependencies
#COPY *.csproj ./
#RUN dotnet restore 
#COPY . .
#RUN dotnet publish -c Release -o out
#
## Second stage: Making the published application executable
#FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
#WORKDIR /app
#COPY --from=build /app/out ./
#
## Optionally, perform AOT compilation
#RUN dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishTrimmed=true
#
## Run the application
#ENTRYPOINT ["dotnet", "jetty-dash-backend.dll"]
