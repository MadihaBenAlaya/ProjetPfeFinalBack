FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env

WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim

WORKDIR /app
COPY --from=build-env /app/out ./

COPY Resources ./Resources

EXPOSE 80

CMD ["dotnet", "AppRecrutement.dll", "--environment", "Development"]
