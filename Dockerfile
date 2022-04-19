FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /app
COPY . .
RUN dotnet restore --source https://api.nuget.org/v3/index.json
RUN dotnet publish IdentityServer.WebApplication/IdentityServer.WebApplication.csproj --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 443/tcp
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.WebApplication.dll"]
