dotnet restore
dotnet build -c release /nowarn:CS1591
dotnet publish -c release -o ./docker/build
rmdir ./docker/build /S /Q
docker build ./docker -t coffers-webapi
docker tag coffers-webapi maiznpetr/coffers-webapi:v2
docker push maiznpetr/coffers-webapi:v2