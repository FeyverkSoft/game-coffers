dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o docker/build
docker build ./docker -t coffers-api
docker tag coffers-api maiznpetr/coffers-api:latest
docker push maiznpetr/coffers-api:latest