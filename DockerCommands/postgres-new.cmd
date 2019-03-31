docker stop postgres-server -t 0
docker rm postgres-server
docker run --name postgres-server --mount source=postgres-data,target=/var/lib/postgresql/data -p 5499:5432 -e POSTGRES_PASSWORD=root -d postgres
rem username: root password: root