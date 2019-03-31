set image=%1
docker run --name temp-data-container --mount source=mongo-data,target=/mount -t -d alpine:3.9 sleep 200
docker exec -ti temp-data-container sh -c "cp -rf /mount /data"
docker commit temp-data-container registry.cn-beijing.aliyuncs.com/ruibeitest/mongo-data:%image%
docker stop temp-data-container -t 0
docker rm temp-data-container