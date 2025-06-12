# Auth Service Minimum Requirements

This document lists the minimal information required to integrate with the Auth microservice.

## Base URL
- `http://<host>:7000`

## Required Headers
- `Content-Type: application/x-www-form-urlencoded`

## Example Request
```
POST /connect/token
client_id=sample&client_secret=secret&grant_type=client_credentials&scope=api1
```

## Introspection Example
```
POST /connect/introspect
token=<ACCESS_TOKEN>
```

## Success Response
```
{
  "access_token": "<TOKEN>",
  "expires_in": 3600,
  "token_type": "Bearer"
}
```

Refer to `openapi.yaml` for full schema.

### Environment Variables
- `ConnectionStrings__AuthDb` – PostgreSQL connection string. Each tenant can use a dedicated schema.
