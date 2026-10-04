# VetFlow – DevWeave Database Provider & Development Strategy
**Final Architecture Decision v1.0**

## Final decision

VetFlow uses **EF Core Code First**.

- **SQLite** = default local developer database
- **PostgreSQL** = canonical CI, staging and production database
- Domain and application layers remain provider-independent.
- Database-per-tenant remains the production architecture.
- EF Core migrations are version-controlled.
- Persistence stories must be validated against PostgreSQL before completion.

## Provider matrix

| Environment | Provider |
|---|---|
| Developer laptop | SQLite |
| CI | PostgreSQL |
| Staging | PostgreSQL |
| Production | PostgreSQL |

## Development flow

```text
Business Requirements
  → Business Workflows
  → Domain Model
  → EF Core Entity Model
  → Explicit EF Core Configuration
  → EF Core Migrations
  → SQLite locally
  → PostgreSQL in CI/Staging/Production
```

## Local database

```text
.devdata/
├── platform.db
├── tenant-demo.db
└── tenant-test.db
```

SQLite is a resource-saving development choice, not the production architecture.

## Production

```text
Platform PostgreSQL
  ├── Tenant A PostgreSQL DB
  ├── Tenant B PostgreSQL DB
  ├── Tenant C PostgreSQL DB
  └── Tenant D PostgreSQL DB
```

## DevWeave DATABASE-PROVIDER-POLICY

1. VetFlow uses EF Core Code First.
2. Business workflows and the domain model drive persistence design.
3. SQLite is the default provider for local developer machines.
4. PostgreSQL is the canonical provider for CI, staging and production.
5. Domain and application layers must remain database-provider independent.
6. Important keys, relationships, constraints, indexes and precision must be explicitly configured.
7. SQLite-specific behavior must not enter business logic.
8. PostgreSQL-specific functionality requires an ADR when architecturally significant.
9. Database migrations are version-controlled.
10. Persistence-related stories require PostgreSQL validation before completion.
11. SQLite passing alone does not establish production compatibility.
12. Database-per-tenant architecture must remain intact in local and deployed environments.

## Stories to update

| Story | Required change |
|---|---|
| VF-003 | Configure SQLite local development |
| VF-004 | Rename to “Configure EF Core Code First and Database Providers”; support SQLite + PostgreSQL |
| VF-009 | Add SQLite local testing and PostgreSQL integration testing |
| VF-010 | Support PostgreSQL CI/staging-like execution |
| VF-011 | Support local SQLite and deployed PostgreSQL |
| VF-012 | Tenant DB resolver supports both providers |
| VF-013 | Local SQLite file provisioning + deployed PostgreSQL provisioning |
| VF-016 | Provider-neutral metadata persistence + PostgreSQL validation |
| VF-019+ | Persistence stories inherit the policy and require PostgreSQL validation |
| VF-051 | Tenant isolation validation against PostgreSQL |

The other stories should **not** repeat “use SQLite”; they inherit the project-level DevWeave policy.

## VF-004 acceptance criteria

- Application starts locally with SQLite without a database server.
- EF Core can create/update the local development database.
- The same model can migrate a PostgreSQL database.
- Domain/application code contains no provider-specific business logic.
- Important constraints/indexes are explicitly configured.
- CI runs PostgreSQL migrations and integration tests.
- Provider-specific deviations are documented.

## Antigravity execution rule

For persistence stories:

1. Read the story and database provider policy.
2. Inspect existing domain and EF Core configuration.
3. Implement provider-neutral domain/application code.
4. Validate locally with SQLite.
5. Run unit tests.
6. Run PostgreSQL migration/integration validation.
7. Verify acceptance criteria.
8. Record files, migrations and test evidence.
9. Update DevWeave state/knowledge graph.
10. Do not perform unrelated refactoring.

## Final statement

**VetFlow is a Business/Domain-Driven Code First application using EF Core. SQLite is a lightweight local-development provider. PostgreSQL is the canonical production provider.**

The architecture, tenant model, workflows and domain model must never be weakened merely to accommodate SQLite.
