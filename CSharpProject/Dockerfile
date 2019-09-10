FROM microsoft/dotnet:2.1-sdk

RUN  apt-get update && apt-get install make -y

COPY . /server/
RUN cd /server/ && make

ENTRYPOINT [ "dotnet", "run", "-p", "/server" ]

EXPOSE 60151