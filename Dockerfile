FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
COPY . ./

RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim

WORKDIR /app

COPY --from=build-env /app/out .

CMD ["dotnet", "interface-description-list.dll"]
