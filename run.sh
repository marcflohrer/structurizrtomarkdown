# /bin/bash 
# pass first parameters to docker run as path to mount as volume
docker run -it --rm -v "$1:/app/input" lohrer/structurizr2markdown /app/input/workspace.json
