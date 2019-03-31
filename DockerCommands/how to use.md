# 使用方法
先安装docker

# 安装和使用mysql

快速安装使用，依次运行下列命令:
mysql-install
mysql-pull test01
mysql-load test01
mysql-new

1. 运行mysql-install
2. 拉取mysql数据镜像 运行: mysql-pull test01
3. 将镜像当中的数据加载给本地数据 运行: mysql-load test01
4. 创建启动mysql服务器: mysql-new
5. 停止mysql: mysql-stop
6. 重新运行mysql: mysql-start
7. 将mysql数据存到镜像 test02 (需要确认与其他人命名没有冲突) 运行: mysql-save test02
8. 将mysql镜像test02推送到镜像服务 (需要确认与其他人命名没有冲突) 运行: mysql-pull test02

# 安装和使用mongo

快速安装使用，依次运行下列命令:
mongo-install
mongo-pull test01
mongo-load test01
mongo-new

1. 运行mongo-install
2. 拉取mongo数据镜像 运行: mongo-pull test01
3. 将镜像当中的数据加载给本地数据 运行: mongo-load test01
4. 创建启动mongo服务器: mongo-new
5. 停止mongo: mongo-stop
6. 重新运行mongo: mongo-start
7. 将mongo数据存到镜像 test02 (需要确认与其他人命名没有冲突) 运行: mongo-save test02
8. 将mongo镜像test02推送到镜像服务 (需要确认与其他人命名没有冲突) 运行: mongo-pull test02

# common commands
1. 查看本地镜像列表: docker images
2. 查看正在运行的容器: docker ps
3. 查看所有容器: docker ps -a
4. 停止docker容器: docker stop [container-name] -t 0
5. 列出当前所有docker volume: docker volume ls
6. 删除docker volume: docker volume rm [volume-name]
7. 创建docker volume: docker volume create [volume-name]

# 主要应用
1. 同步数据给别人
2. 用来回复自己的数据到之前的状态