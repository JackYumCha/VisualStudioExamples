dotnet publish VsExample.AspAPI.csproj --framework netcoreapp2.2 -c Release -o ./bin/docker
docker build ./ -f vsexamples.dockerfile -t vsexamples-image
docker tag vsexamples-image 714626288919.dkr.ecr.ap-southeast-2.amazonaws.com/vs-examples:%1
call aws-ecr-login.cmd
docker push 714626288919.dkr.ecr.ap-southeast-2.amazonaws.com/vs-examples:%1