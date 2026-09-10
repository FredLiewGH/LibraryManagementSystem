# ============================================================
# Stage 1: Build
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY LMS.sln .
COPY LMS/LMS.csproj                   LMS/
COPY LMS.Services/LMS.Services.csproj LMS.Services/
COPY LMS.EFCore/LMS.EFCore.csproj     LMS.EFCore/
RUN dotnet restore LMS/LMS.csproj

COPY . .
RUN dotnet publish LMS/LMS.csproj -c Release -o /app/publish --no-restore

# ============================================================
# Stage 2: Runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# Required for EF Core globalization (utf8mb4 collation, citext)
RUN apk add --no-cache icu-libs \
    && rm -rf /var/cache/apk/*
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN addgroup -S lmsgroup && adduser -S lmsuser -G lmsgroup

WORKDIR /app
COPY --from=build /app/publish .
RUN chown -R lmsuser:lmsgroup /app

EXPOSE 8083
USER lmsuser

HEALTHCHECK --interval=60s --timeout=5s --start-period=10s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8083/default/health || exit 1

ENTRYPOINT ["dotnet", "LMS.dll"]