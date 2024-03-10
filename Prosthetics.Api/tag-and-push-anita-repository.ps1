docker build -t juniordevops/arm64/prosthetics.api -f dockerfile-arm64 .
docker tag juniordevops/arm64/prosthetics.api 192.168.1.20:5000/juniordevops/arm64/prosthetics.api
docker push 192.168.1.20:5000/juniordevops/arm64/prosthetics.api
ssh lukas@192.168.1.20 docker pull 192.168.1.20:5000/juniordevops/arm64/prosthetics.api
ssh lukas@192.168.1.20 docker run -it --rm --name prosthetics.api-container -p 8080:8080 -e ASPNETCORE_URLS=http://0.0.0.0:8080 192.168.1.20:5000/juniordevops/arm64/prosthetics.api