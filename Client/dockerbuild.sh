cp -r build ./docker
docker build ./docker -t coffers-client
docker tag coffers-client maiznpetr/coffers-client:latest
docker push maiznpetr/coffers-client:latest