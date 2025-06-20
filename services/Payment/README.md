# Payment Service

This service handles payment processing for the e-commerce platform. It is implemented in Go 1.22 using Gin, gRPC and gRPC Gateway. The service exposes both gRPC and HTTP/JSON APIs.

## Minimum Requirements

- **Database**: local SQLite file `payment.db`.
- **Ports**:
  - gRPC: `7001`
  - HTTP (gRPC Gateway): `8080`

## Development

1. Install Go 1.22 and the following CLI tools:
   - `go-swagger`
   - `protoc` with `protoc-gen-go` and `protoc-gen-grpc-gateway`
   - `cobra` (for optional CLI commands)
2. Build and run:
   ```bash
   go run ./cmd/server
   ```
3. Docker build:
   ```bash
   docker build -t payment.api .
   ```
4. Or run locally with Docker Compose:
   ```bash
   docker compose up --build
   ```
