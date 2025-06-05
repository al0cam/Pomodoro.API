# Build stage (SDK includes EF tools)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

# Restore and build
RUN dotnet restore
RUN dotnet build -c Release

# Publish app
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy built app
COPY --from=build /app/publish .

# Ensure SQLite directory
RUN mkdir -p /data
VOLUME ["/data"]

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "Pomodoro.API.dll"]
