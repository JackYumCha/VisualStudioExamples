docker stop arango-server -t 0
docker rm arango-server
docker run --name arango-server --mount source=arango-data,target=/var/lib/arangodb3 -p 8599:8529 -e ARANGO_ROOT_PASSWORD=root -d arangodb:3.4.4
rem username: root password: root