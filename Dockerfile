# Multi-stage distroless Native AOT build.
# Produces a ~15MB final image with no shell, no package manager, non-root UID.

ARG DOTNET_SDK=10.0-alpine
ARG RUNTIME_IMAGE=gcr.io/distroless/static-debian12:nonroot
ARG PROJECT={{PROJECT_NAME}}
ARG RID=linux-musl-x64

# --- build ---
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_SDK} AS build
ARG PROJECT
ARG RID
WORKDIR /src

# AOT needs clang + zlib dev headers on alpine
RUN apk add --no-cache clang build-base zlib-dev

COPY global.json Directory.Build.props Directory.Packages.props ./
COPY src/${PROJECT}/${PROJECT}.csproj src/${PROJECT}/
RUN dotnet restore "src/${PROJECT}/${PROJECT}.csproj" -r ${RID} --locked-mode

COPY src/ src/
RUN dotnet publish "src/${PROJECT}/${PROJECT}.csproj" \
    -c Release \
    -r ${RID} \
    --no-restore \
    -o /app/publish

# --- runtime ---
FROM ${RUNTIME_IMAGE} AS runtime
ARG PROJECT
WORKDIR /app
COPY --from=build /app/publish/${PROJECT} /app/app

USER nonroot:nonroot
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_TieredCompilation=1

ENTRYPOINT ["/app/app"]
