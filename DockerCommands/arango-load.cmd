set image=%1
docker run --name temp-data-container --mount source=arango-data,target=/mount -d %image%
docker exec -ti temp-data-container sh -c "rm -rf /mount/*"
docker exec -ti temp-data-container sh -c "cp -rf /data/* /mount/"
docker stop temp-data-container -t 0
docker rm temp-data-container