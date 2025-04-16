# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy and restore dependencies
COPY ["taskmanager.api/taskmanager.api.csproj", "taskmanager.api/"]
RUN dotnet restore "taskmanager.api/taskmanager.api.csproj"

# Copy the entire source code and build the application
COPY ["taskmanager.api/", "taskmanager.api/"]
WORKDIR /src/taskmanager.api
RUN dotnet build "taskmanager.api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "taskmanager.api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "taskmanager.api.dll"]