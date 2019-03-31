docker stop arango-server -t 0
docker rm arango-server
docker run --name arango-server --mount source=arango-data,target=/var/lib/arangodb3 -p 8529:8529 -e ARANGO_RANDOM_ROOT_PASSWORD=root -d arangodb:3.4.2
rem username: root password: root