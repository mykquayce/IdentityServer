docker pull eassbhhtgu/identity-server:latest
if (!$?) { return; }
docker stack deploy --compose-file .\docker-compose.yml identity-server
