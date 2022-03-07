docker pull mcr.microsoft.com/dotnet/sdk:7.0
docker pull mcr.microsoft.com/dotnet/aspnet:7.0
docker build --tag eassbhhtgu/identity-server:latest .
docker push eassbhhtgu/identity-server:latest
docker stack deploy --compose-file .\docker-compose.yml identity-server
