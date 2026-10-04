# VetFlow

> Multi-tenant veterinary practice management SaaS platform.

## Technology Stack

| Layer | Technology |
|---|---|
| Frontend | Angular 22 PWA |
| Backend | ASP.NET Core / .NET 10 — Modular Monolith |
| Architecture | Vertical Slice + CQRS-lite + Selective DDD |
| Auth | Keycloak (OIDC/OAuth2) |
| ORM | EF Core Code First |
| Local Dev DB | SQLite |
| CI/Staging/Prod DB | PostgreSQL |
| Multi-tenancy | Database-per-tenant |
| Object Storage | MinIO |
| Observability | OpenTelemetry |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js 22+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Keycloak](https://www.keycloak.org/) (via Docker Compose for local dev)

## Quick Start

```bash
# 1. Start local infrastructure (Keycloak, PostgreSQL, MinIO)
docker compose -f infra/compose/docker-compose.dev.yml up -d

# 2. Run backend
dotnet run --project src/VetFlow.Api

# 3. Run frontend
cd frontend/vetflow-pwa && ng serve
```

## Local databases

SQLite databases are stored in `.devdata/` (git-ignored):
```
.devdata/
├── platform.db
├── tenant-demo.db
└── tenant-test.db
```

## Repository structure

```
src/                    ← .NET backend solution
  VetFlow.sln
  VetFlow.Api/          ← ASP.NET Core API host
  VetFlow.Application/  ← Vertical slice handlers, CQRS commands/queries
  VetFlow.Domain/       ← Domain model, aggregates, value objects
  VetFlow.Infrastructure/ ← EF Core, Keycloak, MinIO, Outbox adapters
  VetFlow.Shared/       ← Shared kernel, contracts, cross-cutting concerns
  modules/              ← Feature modules (Platform, Clinical, Pharmacy, etc.)
frontend/
  vetflow-pwa/          ← Angular PWA workspace
tests/
  VetFlow.Domain.Tests/
  VetFlow.Application.Tests/
  VetFlow.Integration.Tests/
infra/
  docker/               ← Dockerfiles
  compose/              ← Docker Compose definitions
  k8s/                  ← Kubernetes manifests (future)
docs/
  01-product/
  02-business/
  03-architecture/
  04-domain/
  05-api/
  06-workflows/
  07-stories/
  08-ai/
scripts/                ← Build and utility scripts
.devdata/               ← Local SQLite databases (git-ignored)
.devweave/              ← DevWeave workspace state
ProjectDoc/             ← Architecture documentation pack
```

## DevWeave

This project uses [DevWeave AI-DLC](https://devweave.io) for story-driven development orchestration.

- Story lifecycle: `Backlog → Contexted → Ready → InProgress → Verification → Review → Approved → Completed`
- 55 stories across 12 epics (VF-001 to VF-055)
- Database policy: SQLite locally, PostgreSQL for CI/staging/production

---

**VetFlow v1.0** — Built with DevWeave + Google Antigravity
