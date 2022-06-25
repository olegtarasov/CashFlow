# CashFlow

## Running with HTTP

Backend

```shell
docker run --rm -p 7092:80 olegtarasov/cashflow
```

## Running with HTTPS

Build
```shell
docker build -t cashflow_https -f CashFlow/Dockerfile_https --build-arg CERT_PASSWORD=123 .
```

Backend

```shell
docker run --rm -p 7092:443 cashflow_https
```