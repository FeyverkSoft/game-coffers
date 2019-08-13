cp -r build ./docker
find ./docker -name "*.css.map" -delete
find ./docker -name "*.js.map" -delete
docker build ./docker -t coffers-client
docker tag coffers-client maiznpetr/coffers-client:latest
docker push maiznpetr/coffers-client:latest