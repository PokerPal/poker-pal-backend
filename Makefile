default: build

# Build rules

build:
	dotnet build PokerPal.sln

clean:
	dotnet clean PokerPal.sln

# Run rules

inmemory: build
	dotnet run -p Api/Api.csproj --launch-profile "Local API/In Memory Database"

postgresql: build
	dotnet run -p Api/Api.csproj --launch-profile "Local API/PostgreSQL"

.PHONY: build clean inmemory postgresql

