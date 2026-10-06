# MMM-Chama Backend

Basic ASP.NET Core 8 MVC-style REST API for chama and member management.

## Endpoints

- `GET /api/chamas` — list chamas
- `GET /api/chamas/{id}` — retrieve a chama and its members
- `POST /api/chamas` — create a chama
- `POST /api/chamas/{id}/members` — add a member to a chama

The first version uses an in-memory store so the API can be wired to the frontend immediately. Data resets when the application restarts. Replace `InMemoryChamaStore` with EF Core or another persistent store when database requirements are defined.

## Run locally

```bash
dotnet run --project Back/MMM.Chama.Api.csproj
```

The API allows cross-origin requests for local frontend development. Tighten the CORS policy before production deployment.
