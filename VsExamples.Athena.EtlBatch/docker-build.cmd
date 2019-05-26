dotnet publish -c Release -o ./bin/docker
docker build ./ -f vsexamples-athena-etl.dockerfile -t 714626288919.dkr.ecr.ap-southeast-2.amazonaws.com/vs-examples-athena-etl:%1
call aws-ecr-login.cmd
docker push 714626288919.dkr.ecr.ap-southeast-2.amazonaws.com/vs-examples-athena-etl:%1