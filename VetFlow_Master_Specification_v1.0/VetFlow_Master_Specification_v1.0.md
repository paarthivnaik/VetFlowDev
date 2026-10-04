# VetFlow Master Specification v1.0

This is the consolidated implementation baseline for VetFlow using DevWeave + Google Antigravity.

## Final architecture
- Angular PWA
- ASP.NET Core / .NET 10
- Modular monolith + Vertical Slice + CQRS-lite + selective DDD
- Keycloak
- EF Core Code First
- SQLite for local development
- PostgreSQL for CI, staging and production
- Database-per-tenant
- Transactional Outbox, no mandatory broker in V1
- Open-source-first
- AI-ready, but AI not in the V1 transactional path

## Document hierarchy
BRD → PRD → Business Workflows/Rules → SDD → Domain/DB → API/Security → Provider Policy → Stories.

## DevWeave
DevWeave is the project orchestrator. The existing 55 stories remain the backlog baseline. DevWeave enriches them into executable stories and selects only the context needed for each story.

## Story lifecycle
Backlog → Contexted → Ready → InProgress → Verification → Review → Approved → Completed

Additional: Blocked, FailedVerification, NeedsDecision, Cancelled.

## Database policy
SQLite is a development convenience. PostgreSQL is the architectural truth. Persistence stories must pass PostgreSQL validation before completion.

## Repository documentation
```text
docs/
├── 01-product/
├── 02-business/
├── 03-architecture/
├── 04-domain/
├── 05-api/
├── 06-workflows/
├── 07-stories/
└── 08-ai/

.devweave/
├── project.json
├── state.json
├── knowledge-graph.json
├── graph-delta/
└── rules/
```

## Antigravity
Do not provide the entire documentation pack for each story. DevWeave creates a minimum-context execution packet containing the story, relevant requirements/workflows/architecture/domain/API/security/provider policy, current code context, constraints and acceptance criteria.

## Final principle
**DevWeave decides what/why/context. Antigravity decides how to implement.**
