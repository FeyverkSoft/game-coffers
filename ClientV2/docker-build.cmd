DEL .\docker\build /s /f /q
xcopy build\*.* docker\build\ /E
docker build ./docker -t coffers-client
docker tag coffers-client maiznpetr/coffers-client:v2
docker push maiznpetr/coffers-client:v2
DEL .\docker\build /s /f /q