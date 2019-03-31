docker stop mongo-server -t 0
docker rm mongo-server
docker run --name mongo-server --mount source=mongo-data,target=/data/db -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=root -e MONGO_INITDB_ROOT_PASSWORD=root -d mongo:4.0.6
rem username: root password: root