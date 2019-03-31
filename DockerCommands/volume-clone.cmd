set volume=%1
set image=%2

echo Dump data from %volume% to %image%

docker run --name temp-data-container --mount source=%volume%,target=/mount -t -d alpine:3.9 sleep 200
docker exec -ti temp-data-container sh -c "cp -rf /mount /data"
docker commit temp-data-container %image%
docker stop temp-data-container -t 0
docker rm temp-data-container