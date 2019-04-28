FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8099
#EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8099
#RUN dotnet dev-certs https

FROM base as final
COPY ./bin/docker .
ENTRYPOINT ["dotnet", "VsExample.AspAPI.dll"]