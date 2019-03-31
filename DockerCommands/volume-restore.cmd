set image=%1
set volume=%2

echo Restore data from %image% to %volume%

docker volume create %volume%
docker run --name temp-data-container --mount source=%volume%,target=/mount -d %image%
echo Remove all content in the volume
docker exec -ti temp-data-container sh -c "rm -rf /mount/*"
echo copy data to volume
docker exec -ti temp-data-container sh -c "cp -rf /data/* /mount/"
docker stop temp-data-container -t 0
docker rm temp-data-container