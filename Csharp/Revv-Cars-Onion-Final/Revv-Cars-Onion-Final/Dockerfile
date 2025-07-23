# ---------------- Build Stage ---------------- stage 1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the sln file
COPY *.sln .

# Copy all csproj files for restore
COPY Revv.cars.Web/Revv.cars.Web.csproj Revv.cars.Web/
COPY Revv.cars.Repo/Revv.cars.Repo.csproj Revv.cars.Repo/
COPY Revv.cars.DomainService/Revv.cars.DomainService.csproj Revv.cars.DomainService/
COPY Revv.cars.Domain/Revv.cars.Domain.csproj Revv.cars.Domain/
COPY Revv.cars.Shared/Revv.cars.Shared.csproj Revv.cars.Shared/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/Revv.cars.Web
RUN dotnet publish -c Release -o /app/publish

# ---------------- Runtime Stage ---------------- stage 2
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port (adjust if needed)
EXPOSE 80

# Set the entry point
ENTRYPOINT ["dotnet", "Revv.cars.Web.dll"]
