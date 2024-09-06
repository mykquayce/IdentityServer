docker pull mcr.microsoft.com/dotnet/sdk:8.0
if (!$?) { return; }
docker pull mcr.microsoft.com/dotnet/aspnet:8.0
if (!$?) { return; }
docker build `
	--secret "id=ca_crt,src=${env:userprofile}\.aspnet\https\ca.crt" `
	--tag eassbhhtgu/identity-server:latest `
	.
