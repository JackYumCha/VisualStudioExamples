docker stop mysql-server -t 0
docker rm mysql-server
docker run --name mysql-server --mount source=mysql-data,target=/var/lib/mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -d mysql:5.7.25
rem username: root password: root