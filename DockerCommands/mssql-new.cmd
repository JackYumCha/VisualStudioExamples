docker stop mssql-server -t 0
docker rm mssql-server
docker run --name mssql-server -p 1433:1433 -e SA_PASSWORD=rootR0@t -e ACCEPT_EULA=Y -d microsoft/mssql-server-linux:2017-latest
rem password is rootR0@t