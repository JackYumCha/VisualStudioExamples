set image=%1
docker run --name temp-data-container --mount source=mysql-data,target=/mount -d registry.cn-beijing.aliyuncs.com/ruibeitest/mysql-data:%image%
docker exec -ti temp-data-container sh -c "rm -rf /mount/*"
docker exec -ti temp-data-container sh -c "cp -rf /data/* /mount/"
docker stop temp-data-container -t 0
docker rm temp-data-container