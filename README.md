# Myflix Api Gateway
 
## Description

A microservice part of `Myflix` project which servers as the api gateway.
The service is built using ASP.NET Core, Ocelot and Docker.

## Features

- **Routing Requests**: Routes incoming requests downstream.
- **Load Balancing**: Route "watch video" request between multiple video providers.
- **Authentication**: Verifies request authentication using JWT.
- **Rate Limiting**: Implements rate limiting for requests.

## Customization

- For development configuration, modify `ocelot.development.json`.
- For deployment configuration, modify `ocelot.production.json`.

## Deployment

### Using Docker:
1. Build the Docker image from `Dockerfile` in `/src` folder.

```bash
docker build -t {image-name} .
```

2. Run the docker image.

```bash
docker run -p 5000:5000 --name {container-name} {image-name}
```