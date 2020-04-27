FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Copy sources
WORKDIR /app
COPY . .

# Install dependencies and build to .dll
RUN dotnet restore
RUN dotnet publish -c Release -o out Api/Api.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS run

# Copy built .dll from build phase
WORKDIR /app
COPY --from=build /app/out ./

CMD ASPNETCORE_URLS="http://0.0.0.0:$PORT" dotnet Api.dll
