FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

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
# TODO: Make it self-contained
################
# Build a small image for .NET 8
# Largely inspired by https://www.thorsten-hans.com/how-to-build-smaller-and-secure-docker-images-for-net5
#
#ARG RUNTIME=linux-x64
#ARG BUILD_CONFIGURATION=Release
#ARG PROJECT_FILE=ActionsMinUtils.Sample.csproj
#
## Builder
#FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder
#ARG RUNTIME
#ARG BUILD_CONFIGURATION
#ARG PROJECT_FILE
#WORKDIR /src
#COPY . .
#RUN dotnet restore --runtime $RUNTIME
#RUN dotnet publish $PROJECT_FILE \
#    --configuration $BUILD_CONFIGURATION \
#    --output /app/publish \
#    --no-restore \
#    --runtime $RUNTIME \
#    --self-contained true \
#    /p:PublishSingleFile=true

# Runtime
#FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine
#ARG PROJECT_FILE
#ENV \
#    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#    LC_ALL=en_US.UTF-8 \
#    LANG=en_US.UTF-8
#RUN apk add --no-cache \
#    icu-data-full \
#    icu-libs \
#    gcompat
#RUN adduser --disabled-password \
#    --home /app \
#    --gecos '' dotnetuser \
#    && chown -R dotnetuser /app
#ENV PROJECT_FILE=$PROJECT_FILE
#USER dotnetuser
#WORKDIR /app
#COPY --from=builder /app/publish .
#RUN ln -s /app/$(basename $PROJECT_FILE .csproj) /app/__entrypoint
#ENTRYPOINT ["/app/__entrypoint"]





#ARG RUNTIME=linux-x64
#ARG BUILD_CONFIGURATION=Release
#ARG PROJECT_FILE=Api.csproj
#
#
#FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS base
#ENV \
#    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#    LC_ALL=en_US.UTF-8 \
#    LANG=en_US.UTF-8
#RUN apk add --no-cache \
#    icu-data-full \
#    icu-libs \
#    gcompat
#RUN adduser --disabled-password \
#    --home /app \
#    --gecos '' dotnetuser \
#    && chown -R dotnetuser /app
#ENV PROJECT_FILE=$PROJECT_FILE
#USER dotnetuser
#WORKDIR /app
#EXPOSE 5000
#
##FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
#ARG PROJECT_FILE
#ARG RUNTIME
#ARG BUILD_CONFIGURATION
#WORKDIR /src
#COPY ["Api/Api.csproj", "Api/"]
#COPY ["Business/Business.csproj", "Business/"]
#COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
#COPY ["Schemes/Schemes.csproj", "Schemes/"]
#RUN dotnet restore "Api/Api.csproj" --runtime $RUNTIME
#
##
#COPY . .
##WORKDIR "/src/Api"
##RUN dotnet restore "Api/Api.csproj" --runtime $RUNTIME
#WORKDIR "/src/Api"
#RUN dotnet build $PROJECT_FILE -c $BUILD_CONFIGURATION -o /app/build --runtime $RUNTIME
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION
#RUN dotnet publish $PROJECT_FILE \
#    --configuration $BUILD_CONFIGURATION \
#    --output /app/publish \
#    --no-restore \
#    --self-contained true \
#    /p:PublishSingleFile=true \
#    --runtime $RUNTIME
#
#
#FROM base AS final
#ARG PROJECT_FILE
#WORKDIR /app
#COPY --from=publish /app/publish .
#RUN ln -s /app/$(basename $PROJECT_FILE .csproj) /app/__entrypoint
#ENTRYPOINT ["/app/__entrypoint"]

