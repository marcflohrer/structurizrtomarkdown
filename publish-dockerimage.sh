#/bin/bash

# This script is used to publish the docker image to docker hub
source .env

# Login to docker hub
echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin

# Build the image
docker build -t $DOCKER_IMAGE_NAME .

# Tag the image
docker tag $DOCKER_IMAGE_NAME $DOCKER_USERNAME/$DOCKER_IMAGE_NAME:$DOCKER_IMAGE_TAG

# Push the image
docker push $DOCKER_USERNAME/$DOCKER_IMAGE_NAME