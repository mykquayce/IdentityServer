docker pull mcr.microsoft.com/dotnet/sdk:8.0
if (!$?) { return; }
docker pull mcr.microsoft.com/dotnet/aspnet:8.0
if (!$?) { return; }
docker build --tag eassbhhtgu/identity-server:latest .
if (!$?) { return; }
docker push eassbhhtgu/identity-server:latest
