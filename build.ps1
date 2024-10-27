docker pull mcr.microsoft.com/dotnet/sdk:8.0-alpine
if (!$?) { return; }
docker pull mcr.microsoft.com/dotnet/aspnet:8.0-alpine
if (!$?) { return; }
docker build `
	--file '.\IdentityServer.WebApplication\Dockerfile' `
	--secret "id=ca_crt,src=${env:userprofile}\.aspnet\https\ca.crt" `
	--tag eassbhhtgu/identity-server:latest `
	.
if (!$?) { return; }
docker push eassbhhtgu/identity-server:latest
