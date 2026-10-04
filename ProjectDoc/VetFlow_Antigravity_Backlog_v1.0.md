# VetFlow — Antigravity Implementation Backlog v1.0

## Implementation rule
Implement in dependency order. Each story must be independently testable, buildable, tenant-safe and limited in scope. Never ask Antigravity to implement an entire epic in one prompt.

## Standard story contract
Each story contains: ID, Epic, Goal, Dependencies, Description, Implementation, Acceptance Criteria, Definition of Done, and Antigravity execution instructions.

## VF-001 — Create repository structure and solution
**Epic:** R0 Foundation
**Goal:** Create the initial VetFlow repository and approved project structure.
**Dependencies:** None

### Description
Establish the solution without implementing business workflows. The repository must clearly separate backend modules, frontend, workers, tests, infrastructure and documentation.
### Implementation
- Create the .NET solution and projects defined by the SDD.
- Create the Angular PWA workspace.
- Create test projects.
- Create Docker/infrastructure/documentation directories.
- Configure solution/project references so dependencies point inward toward domain/application contracts rather than creating circular module references.
- Add baseline README and developer setup instructions.
- Do not add speculative libraries or microservices.
### Acceptance Criteria
- Repository contains the approved structure.
- Backend builds successfully.
- Angular builds successfully.
- Tests execute successfully.
- No circular project dependencies.
- A developer can clone and understand where each future feature belongs.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-002 — Configure coding standards and quality gates
**Epic:** R0 Foundation
**Goal:** Make the repository enforce consistent engineering standards.
**Dependencies:** VF-001

### Description
Establish formatting, analyzers, TypeScript linting, test conventions, nullable/reference safety and CI validation.
### Implementation
- Configure .editorconfig and .NET analyzers.
- Configure TypeScript/Angular lint and formatting.
- Define naming conventions.
- Define test naming conventions.
- Add CI build/test workflow.
- Fail CI on compilation or critical quality failures.
### Acceptance Criteria
- Backend and frontend checks run in CI.
- Formatting/analyzer violations are surfaced.
- Build/test failures fail the pipeline.
- No application behavior is changed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-003 — Create local development environment
**Epic:** R0 Foundation
**Goal:** Provide a reproducible local environment for PostgreSQL, Keycloak and object storage.
**Dependencies:** VF-001

### Description
Create Docker Compose for local infrastructure using open-source components. Application containers may be added later.
### Implementation
- Add PostgreSQL for Platform DB and tenant development DB.
- Add Keycloak and its own database.
- Add MinIO.
- Create persistent local volumes.
- Create environment-variable templates.
- Document startup/shutdown and credentials strategy for local use.
### Acceptance Criteria
- All infrastructure starts with one documented command.
- PostgreSQL, Keycloak and MinIO are reachable.
- Persistent volumes survive restart.
- Secrets are not committed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-004 — Configure PostgreSQL and EF Core
**Epic:** R0 Foundation
**Goal:** Create the persistence foundation without business tables.
**Dependencies:** VF-001,VF-003

### Description
Establish EF Core conventions, connection handling, migrations infrastructure and tenant database context abstractions.
### Implementation
- Create PlatformDbContext and TenantDbContext foundations.
- Configure PostgreSQL provider.
- Create migration commands/documentation.
- Configure naming, timestamps, concurrency strategy and common persistence conventions.
- Do not create all domain entities yet.
### Acceptance Criteria
- Both contexts can connect.
- A test migration can be generated/applied.
- Connection strings come from configuration/secrets.
- No business tables are introduced outside the intended scope.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-005 — Create API foundation
**Epic:** R0 Foundation
**Goal:** Create ASP.NET Core API pipeline and common API behavior.
**Dependencies:** VF-001

### Description
Set up REST/OpenAPI, health checks, exception handling, validation pipeline and correlation IDs.
### Implementation
- Configure ASP.NET Core hosting.
- Enable OpenAPI.
- Add health/readiness endpoints.
- Create consistent error contract.
- Add request correlation/trace ID handling.
- Add structured logging hooks.
- Do not implement authorization yet beyond pipeline placeholders.
### Acceptance Criteria
- API starts successfully.
- OpenAPI is generated.
- Health endpoint works.
- Validation/business errors have consistent shape.
- Unhandled errors do not leak internals.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-006 — Create Angular PWA shell
**Epic:** R0 Foundation
**Goal:** Create one Angular application with route-separated Platform, Business and Portal areas.
**Dependencies:** VF-001

### Description
Create the single PWA shell and layout boundaries without implementing screens.
### Implementation
- Create core/shared/public/platform/business/portal/pwa directories.
- Configure lazy routes.
- Create route placeholders for /platform/login, /business/login and /portal/login.
- Create base layouts.
- Configure PWA service worker.
- Create basic responsive design tokens.
### Acceptance Criteria
- One Angular application exists.
- All three route areas resolve.
- No second Angular application is created.
- PWA install/service-worker configuration is valid.
- Responsive shell works on desktop and mobile widths.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-007 — Create design system foundation
**Epic:** R0 Foundation
**Goal:** Create the reusable UI primitives without creating generic business components.
**Dependencies:** VF-006

### Description
Establish typography, spacing, forms, tables, dialogs, buttons, alerts and navigation primitives using Angular Material/CDK as the foundation.
### Implementation
- Create VetFlow theme tokens.
- Create shared UI primitives only where stable.
- Create staff and portal layout variants.
- Do not create universal domain forms/grids prematurely.
### Acceptance Criteria
- Core UI primitives render consistently.
- Theme matches approved clinical SaaS direction.
- No business-specific behavior is hidden in generic components.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-008 — Configure OpenTelemetry
**Epic:** R0 Foundation
**Goal:** Add trace/log/metric instrumentation foundation.
**Dependencies:** VF-005

### Description
Instrument API and worker foundations using OpenTelemetry while avoiding sensitive clinical payload logging.
### Implementation
- Configure traces, metrics and structured logs.
- Add correlation/trace identifiers.
- Add tenant-safe dimensions where appropriate.
- Do not log passwords, tokens or clinical payloads.
### Acceptance Criteria
- Telemetry is visible locally.
- Sensitive values are excluded.
- Trace IDs correlate API operations.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-009 — Create test architecture
**Epic:** R0 Foundation
**Goal:** Create unit, integration, API, security and workflow test foundations.
**Dependencies:** VF-004,VF-005,VF-006

### Description
Provide test infrastructure that later stories can extend without redesigning it.
### Implementation
- Create test projects.
- Add test database strategy.
- Create API test host.
- Create frontend testing foundation.
- Create authorization/tenant isolation test helpers.
### Acceptance Criteria
- A sample unit test passes.
- A sample API integration test passes.
- Test infrastructure can create isolated test data.
- No production DB is used by automated tests.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-010 — Create deployment containerization
**Epic:** R0 Foundation
**Goal:** Containerize backend, frontend and workers at a baseline level.
**Dependencies:** VF-005,VF-006

### Description
Create production-oriented Dockerfiles and local compose integration without choosing Kubernetes yet.
### Implementation
- Multi-stage builds.
- Non-root runtime where supported.
- Health checks.
- Environment-driven configuration.
- No secrets baked into images.
### Acceptance Criteria
- Images build reproducibly.
- Containers start with required environment variables.
- Images contain no secrets.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-011 — Create Platform DB tenant model
**Epic:** R1 Platform
**Goal:** Implement control-plane tenant and subscription metadata.
**Dependencies:** VF-004

### Description
Create Tenant, Plan, Subscription, TenantDatabase, Feature and TenantFeature models required for provisioning and runtime tenant resolution.
### Implementation
- Implement entities/configurations/migrations.
- Define tenant lifecycle states.
- Define subscription status.
- Protect database credentials; store references/secret identifiers, not raw passwords where possible.
- Add audit fields.
### Acceptance Criteria
- Platform migration creates required tables.
- Tenant lifecycle can be persisted.
- Tenant database mapping is represented.
- Unit tests cover lifecycle transitions.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-012 — Implement tenant database resolver
**Epic:** R1 Platform
**Goal:** Resolve the tenant database from trusted authenticated context.
**Dependencies:** VF-011,VF-005

### Description
Create middleware/service that resolves the tenant after authentication and loads tenant DB configuration from the Platform DB.
### Implementation
- Define TenantContext.
- Resolve tenant from trusted identity mapping/host strategy.
- Reject missing/inactive tenants.
- Create scoped TenantDbContext.
- Never accept an arbitrary browser tenant ID as authoritative.
### Acceptance Criteria
- Authorized request gets correct tenant context.
- Inactive tenant is rejected.
- Missing tenant context is rejected.
- Cross-tenant test fails safely.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-013 — Implement tenant provisioning workflow
**Epic:** R1 Platform
**Goal:** Provision a tenant database and initial configuration.
**Dependencies:** VF-011,VF-012,VF-004

### Description
Implement the stateful provisioning process: create DB, migrate, seed, create admin mapping, mark ready.
### Implementation
- Implement provisioning states.
- Create tenant DB.
- Apply tenant migrations.
- Seed baseline roles/permissions/configuration.
- Create tenant administrator identity mapping.
- Record provisioning audit.
- Make retries idempotent.
### Acceptance Criteria
- A tenant can be provisioned from Platform UI/API.
- Partial failures leave a recoverable provisioning state.
- Repeated provisioning does not corrupt the tenant.
- Ready is only reached after all required steps succeed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-014 — Integrate Keycloak OIDC
**Epic:** R1 Identity
**Goal:** Use Keycloak for authentication.
**Dependencies:** VF-003,VF-005

### Description
Configure OIDC/OAuth2 for platform, business and portal entry points.
### Implementation
- Configure realms/clients according to final security design.
- Configure Angular OIDC flow.
- Configure API JWT validation.
- Map IdentityProviderUserId.
- Implement logout/token renewal behavior.
### Acceptance Criteria
- Valid Keycloak user can authenticate.
- Invalid/expired token is rejected.
- API receives trusted identity.
- Passwords are not stored in VetFlow.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-015 — Implement application roles and permissions
**Epic:** R1 Authorization
**Goal:** Implement VetFlow authorization independent of Keycloak authentication.
**Dependencies:** VF-012,VF-014

### Description
Create Role, Permission, UserRole, UserBranch and policy evaluation.
### Implementation
- Create authorization policies.
- Implement tenant and branch scope.
- Implement permission checks in API.
- Add frontend permission directives/guards as UX only.
- Create baseline roles.
### Acceptance Criteria
- Unauthorized API calls return 403.
- Cross-tenant access is denied.
- Branch restrictions are enforced.
- Frontend cannot bypass server authorization.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-016 — Implement global reference metadata
**Epic:** R1 Platform
**Goal:** Centralize global metadata in Platform DB.
**Dependencies:** VF-011

### Description
Create the reference-data model and seed mechanism for species, breeds, units, countries, currencies, time zones, system permissions and similar metadata.
### Implementation
- Create versioned seed packages.
- Create deterministic seed runner.
- Expose read APIs/cache strategy.
- Do not duplicate global metadata into tenant DBs.
### Acceptance Criteria
- Seed is repeatable.
- Global metadata is available to tenant workflows.
- No cross-DB foreign keys are required.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-017 — Implement branches and practice settings
**Epic:** R1 Practice
**Goal:** Create branch and practice configuration.
**Dependencies:** VF-015

### Description
Support multi-branch practices, operating settings and user-to-branch mapping.
### Implementation
- Create Branch, PracticeSetting, UserBranch.
- Implement branch authorization.
- Support timezone/currency/business hours.
### Acceptance Criteria
- Tenant can create multiple branches.
- Users can be restricted to branches.
- Settings are tenant/branch scoped as designed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-018 — Implement platform admin UI
**Epic:** R1 Platform
**Goal:** Create the Super Admin tenant lifecycle UI.
**Dependencies:** VF-013,VF-015,VF-006

### Description
Build /platform screens for tenant list, create, provisioning status, subscription and basic audit.
### Implementation
- Create platform layout/dashboard.
- Tenant list/search.
- Tenant creation form.
- Provisioning progress/state display.
- Tenant activation/deactivation controls with authorization.
### Acceptance Criteria
- Super Admin can create and inspect tenants.
- Provisioning state is visible.
- Non-platform users cannot access routes or APIs.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-019 — Implement client management
**Epic:** R2 Client
**Goal:** Implement client registration, search and duplicate detection.
**Dependencies:** VF-017

### Description
Build client workflows around real reception needs, not generic CRUD.
### Implementation
- Client entity/contact/address/preferences.
- Normalized search.
- Duplicate candidate detection.
- Create/update/deactivate rules.
- Audit sensitive changes.
### Acceptance Criteria
- Staff can search clients.
- Likely duplicates are surfaced before creation.
- Client can own multiple animals.
- Unauthorized tenant access is impossible.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-020 — Implement animal registration
**Epic:** R2 Patient
**Goal:** Implement animal/patient profile and ownership.
**Dependencies:** VF-019,VF-016

### Description
Create animal demographics, identifiers, medical profile and ownership relationships.
### Implementation
- Species/breed references.
- Microchip/identification.
- Sex/DOB/weight.
- AnimalOwner relationships.
- Prevent destructive deletion after clinical activity.
### Acceptance Criteria
- Animal can be created for a client.
- Multiple responsible parties are supported where configured.
- Reference IDs resolve centrally.
- Clinical history protection is enforced.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-021 — Implement patient timeline
**Epic:** R2 Patient
**Goal:** Create the read-oriented patient timeline.
**Dependencies:** VF-020

### Description
Build a chronological timeline over major clinical and operational events without forcing a single giant domain table.
### Implementation
- Define timeline query/projection.
- Include encounters, diagnosis, prescriptions, diagnostics, vaccinations, procedures, hospitalization and documents as available.
- Pagination/filtering.
- Authorization.
### Acceptance Criteria
- Timeline is chronological.
- Each event links to its source.
- Tenant/branch/client authorization applies.
- Large timelines remain paginated.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-022 — Implement client portal account linkage
**Epic:** R2 Portal
**Goal:** Allow a client to receive secure portal access and authorized pet access.
**Dependencies:** VF-014,VF-020

### Description
Create ClientPortalAccount and ClientPortalPetAccess, with secure invitation/linking flow.
### Implementation
- Link Keycloak identity to ClientId.
- Grant explicit pet access.
- Create portal-specific policies.
- Do not expose staff user records.
### Acceptance Criteria
- Client can authenticate.
- Only authorized pets appear.
- Removing access removes portal visibility.
- Portal APIs enforce the same rule server-side.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-023 — Implement appointment types and availability foundation
**Epic:** R3 Scheduling
**Goal:** Create appointment configuration and provider/branch availability.
**Dependencies:** VF-017,VF-016

### Description
Support appointment types, durations, buffers and business hours required for booking.
### Implementation
- Create configurable appointment types.
- Provider/branch availability.
- Timezone-aware scheduling.
- Prevent invalid bookings.
### Acceptance Criteria
- Configured appointment type can be selected.
- Availability respects branch/provider rules.
- Timezone behavior is tested.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-024 — Implement appointment booking
**Epic:** R3 Scheduling
**Goal:** Implement create/confirm/reschedule/cancel appointment workflows.
**Dependencies:** VF-023,VF-020

### Description
Build the booking workflow with state transitions and notifications as events, not direct external calls.
### Implementation
- Appointment states.
- Conflict validation.
- Client/animal/provider/branch linkage.
- Reschedule/cancel audit.
- Outbox events.
### Acceptance Criteria
- Valid appointment can be booked.
- Conflicting appointment is rejected.
- State transitions are server controlled.
- Outbox event is created atomically.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-025 — Implement check-in and waiting queue
**Epic:** R3 Reception
**Goal:** Implement arrival, check-in and queue management.
**Dependencies:** VF-024

### Description
Support scheduled arrivals and walk-ins, with queue status and emergency path.
### Implementation
- Check-in timestamp.
- Walk-in creation.
- Queue priority rules configurable.
- Waiting queue UI.
- No-show handling.
### Acceptance Criteria
- Reception can check in an appointment.
- Walk-in can enter queue.
- Queue reflects current state.
- Check-in creates/links visit context.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-026 — Implement encounter lifecycle
**Epic:** R4 Clinical
**Goal:** Create encounter aggregate/workflow and finalization.
**Dependencies:** VF-025

### Description
Implement Draft → InProgress → Completed with controlled cancellation where applicable.
### Implementation
- Encounter entity.
- History/exam/vitals sections.
- Clinician ownership.
- Finalization validation.
- Audit and outbox event.
### Acceptance Criteria
- Encounter can be started from checked-in patient.
- Incomplete required data blocks completion.
- Completed encounter cannot be casually edited.
- EncounterCompleted outbox message is persisted.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-027 — Implement diagnosis and treatment
**Epic:** R4 Clinical
**Goal:** Add diagnosis and treatment capture to encounter.
**Dependencies:** VF-026,VF-016

### Description
Support diagnosis, assessment and treatment records with clinician attribution.
### Implementation
- Diagnosis reference/custom terminology strategy.
- Treatment instructions.
- Primary/secondary designation where applicable.
- Audit changes.
### Acceptance Criteria
- Clinician can record diagnosis/treatment.
- Finalization preserves attribution.
- Corrections are controlled/audited.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-028 — Implement follow-up workflow
**Epic:** R4 Clinical
**Goal:** Create reusable follow-up records for clinical workflows.
**Dependencies:** VF-026

### Description
Create FollowUp entity with due date, related source, type, status and reminders integration point.
### Implementation
- FollowUp CRUD only within business rules.
- Statuses pending/completed/cancelled/overdue as appropriate.
- Link to source clinical record.
- Portal visibility rules.
### Acceptance Criteria
- Clinical workflow can create follow-up.
- Follow-up appears on dashboard/portal when permitted.
- Completion is auditable.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-029 — Implement product and medication catalog
**Epic:** R5 Pharmacy
**Goal:** Create tenant product/medication catalog and batches.
**Dependencies:** VF-016,VF-017

### Description
Implement products, product batches, manufacturer, expiry and stock identity needed for dispensing.
### Implementation
- Product metadata.
- Batch/lot.
- Expiry.
- Units.
- Active/inactive status.
- Tenant overrides where applicable.
### Acceptance Criteria
- Product and batch can be created.
- Expired batch is identified.
- Units are validated.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-030 — Implement prescription workflow
**Epic:** R5 Clinical
**Goal:** Implement draft/issue prescription and clinical dosing details.
**Dependencies:** VF-027,VF-029

### Description
Capture medication, dose, unit, frequency, route, duration, instructions, quantity and refills.
### Implementation
- Prescription state machine.
- Prescriber attribution.
- Validation rules.
- Link to encounter/animal.
- Document generation hook.
### Acceptance Criteria
- Valid prescription can be issued.
- Required clinical fields are validated.
- Prescriber is recorded.
- Cancelled prescription cannot be dispensed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-031 — Implement dispensing and inventory deduction
**Epic:** R5 Pharmacy
**Goal:** Dispense prescriptions with batch/expiry validation and auditable stock movement.
**Dependencies:** VF-030,VF-029

### Description
Connect prescription dispensing to inventory transactions without coupling pharmacy to generic inventory abstractions beyond stable contracts.
### Implementation
- Select valid batch.
- Prevent expired batch.
- Support partial dispensing.
- Create inventory transaction atomically with dispensing.
- Record dispenser.
### Acceptance Criteria
- Expired batch cannot be dispensed.
- Partial dispense is supported.
- Stock movement is auditable.
- Repeated request cannot double-deduct stock.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-032 — Implement diagnostic orders
**Epic:** R6 Diagnostics
**Goal:** Create diagnostic order and item workflow.
**Dependencies:** VF-027

### Description
Support clinician ordering diagnostics and tracking order state.
### Implementation
- DiagnosticOrder and items.
- Order status.
- Priority.
- Link to encounter/animal.
- Billing hook.
### Acceptance Criteria
- Authorized clinician can order.
- Order appears in pending work queue.
- Cancellation is controlled.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-033 — Implement sample collection
**Epic:** R6 Diagnostics
**Goal:** Capture sample collection and chain-of-custody fields.
**Dependencies:** VF-032

### Description
Move orders through sample pending/collected and record collector/time/specimen details.
### Implementation
- Sample entity.
- Collection event.
- Specimen type.
- Collector.
- Timestamp.
- Outbox event.
### Acceptance Criteria
- Sample can be collected only for eligible order.
- Collection is auditable.
- State transition is enforced.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-034 — Implement results and veterinarian review
**Epic:** R6 Diagnostics
**Goal:** Implement result entry, report generation and review.
**Dependencies:** VF-033

### Description
Move diagnostics to ResultAvailable and Reviewed with controlled correction/versioning.
### Implementation
- Result values/units/reference range where applicable.
- Report document hook.
- Veterinarian review.
- Correction audit/versioning.
- Client notification event.
### Acceptance Criteria
- Result cannot be marked reviewed without required reviewer.
- Reviewed result changes are audited.
- Client notification occurs only according to configured policy.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-035 — Implement vaccination business workflow
**Epic:** R6 Vaccination
**Goal:** Implement vaccination as a real veterinary workflow, not a generic record.
**Dependencies:** VF-027,VF-016,VF-028

### Description
Capture vaccine, administration, dose, route, batch/lot, manufacturer, expiry, validity, due date and follow-up separately. Create timeline event and follow-up.
### Implementation
- Create Vaccination entity and rules.
- Validate batch/manufacturer/expiry.
- Store administered date.
- Store ValidFrom and ValidUntil separately.
- Store NextDueDate separately.
- Store RecommendedFollowUpDate separately.
- Create FollowUp when appropriate.
- Create timeline event.
- Add certificate generation command/hook.
- Audit finalization.
### Acceptance Criteria
- All required vaccination fields are captured.
- Validity dates are not conflated with next due/follow-up.
- Expired vaccine cannot be administered where policy prohibits it.
- Follow-up is created with correct relationship.
- Vaccination appears on timeline.
- Finalized vaccination cannot be silently edited.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-036 — Implement procedure and surgery workflow
**Epic:** R7 Surgery
**Goal:** Support consent, pre-op, anesthesia, procedure, recovery and post-op.
**Dependencies:** VF-027

### Description
Implement the surgical workflow as independent clinical business logic.
### Implementation
- Procedure/Surgery entities.
- Consent.
- Pre-op assessment.
- Anesthesia record.
- Recovery.
- Post-op instructions.
- Follow-up and billing hooks.
### Acceptance Criteria
- Required pre-op/consent rules are enforced where configured.
- Each stage is attributable.
- Final surgery record is auditable.
- Post-op follow-up can be created.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-037 — Implement hospitalization
**Epic:** R7 Hospitalization
**Goal:** Support admission through discharge/transfer/referral.
**Dependencies:** VF-027,VF-030,VF-031

### Description
Implement planned/admitted/treatment/discharge states with monitoring and treatment records.
### Implementation
- Admission record.
- Location/bed.
- Vitals/monitoring.
- Treatments/medications.
- Progress notes.
- Discharge summary.
- Follow-up.
- Billing hook.
### Acceptance Criteria
- Patient can be admitted.
- Active hospitalized patients are visible.
- Discharge requires configured completion criteria.
- Discharge creates summary/follow-up as appropriate.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-038 — Implement inventory transactions
**Epic:** R8 Inventory
**Goal:** Implement auditable stock movement model.
**Dependencies:** VF-029

### Description
Create stock ledger rather than silently changing a quantity field.
### Implementation
- InventoryTransaction types.
- Receipt/adjustment/dispense/return/transfer.
- Running balance query.
- Batch tracking.
- Audit.
### Acceptance Criteria
- Every stock change has a transaction.
- Inventory can be reconciled.
- Negative stock policy is configurable/enforced.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-039 — Implement suppliers and purchase orders
**Epic:** R8 Purchasing
**Goal:** Support procurement and receiving.
**Dependencies:** VF-029,VF-038

### Description
Implement supplier, purchase order and receipt workflows with batch/expiry capture.
### Implementation
- Supplier.
- PO header/items.
- Receiving.
- Batch/expiry capture.
- Inventory transaction creation.
### Acceptance Criteria
- Received quantity matches inventory movement.
- Batch is traceable to supplier/PO.
- Partial receipt is supported.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-040 — Implement service catalog and charges
**Epic:** R8 Billing
**Goal:** Create tenant service catalog and clinical-to-billing charge capture.
**Dependencies:** VF-017,VF-016

### Description
Support service pricing, tax configuration and charges linked to clinical events.
### Implementation
- Service/price configuration.
- Tenant overrides.
- Charge entity.
- Tax/discount hooks.
- Link source clinical event.
### Acceptance Criteria
- Tenant can configure service prices.
- Clinical workflow can create charges.
- Charges are traceable.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-041 — Implement estimates and invoices
**Epic:** R8 Billing
**Goal:** Implement estimate-to-invoice workflow.
**Dependencies:** VF-040

### Description
Create estimates, charges, invoice items and invoice lifecycle.
### Implementation
- Estimate.
- Invoice.
- InvoiceItem.
- Tax/discount.
- State machine.
- PDF hook.
- Audit.
### Acceptance Criteria
- Estimate can be converted into charges/invoice.
- Invoice state transitions are enforced.
- Paid invoice cannot be silently edited.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-042 — Implement payments and refunds
**Epic:** R8 Billing
**Goal:** Implement payment allocation, receipts and controlled refunds.
**Dependencies:** VF-041

### Description
Support full/partial payment, payment methods and refunds with audit.
### Implementation
- Payment entity.
- Payment allocation.
- Receipt document hook.
- Refund entity.
- Authorization rules.
- Audit.
### Acceptance Criteria
- Partial payment works.
- Invoice becomes Paid only when balance reaches zero.
- Refund is controlled and auditable.
- Receipt can be generated as PDF.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-043 — Implement document storage abstraction
**Epic:** R9 Documents
**Goal:** Create secure object storage infrastructure without a universal business document abstraction.
**Dependencies:** VF-005,VF-003

### Description
Provide MinIO/S3-compatible storage abstraction, secure paths and authorized download behavior.
### Implementation
- Storage interface.
- Tenant-scoped paths.
- Metadata persistence.
- Secure access.
- Upload/download/delete policies.
- Do not encode business rules here.
### Acceptance Criteria
- Tenant documents are isolated.
- Unauthorized download is rejected.
- Object path is not treated as authorization.
- Storage can later switch provider.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-044 — Implement vaccination certificate PDF
**Epic:** R9 Documents
**Goal:** Generate a professional printable/downloadable vaccination certificate.
**Dependencies:** VF-035,VF-043

### Description
Create an independent vaccination certificate composer. Include clinic branding, pet/owner details, vaccine details, batch/manufacturer/expiry, validity, next due, veterinarian, certificate number and generation/version metadata.
### Implementation
- Create business-specific PDF composition.
- Use shared low-level PDF rendering/storage only.
- Store PatientDocument metadata.
- Support Download PDF and Print.
- Version finalized certificate.
- Do not build a universal template engine for all documents.
### Acceptance Criteria
- Certificate is readable on A4.
- All required vaccination data appears.
- PDF can be downloaded.
- PDF can be printed consistently.
- Document is linked to source vaccination.
- Corrections produce a new version rather than silently overwriting.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-045 — Implement clinical and billing PDFs
**Epic:** R9 Documents
**Goal:** Implement separate PDF documents for prescription, lab report, discharge summary, invoice and receipt.
**Dependencies:** VF-030,VF-034,VF-037,VF-041,VF-042,VF-043

### Description
Each document owns its composition and business data mapping. Shared infrastructure only handles rendering/storage.
### Implementation
- Implement each document independently.
- Reuse only stable PDF primitives.
- Store PatientDocument/financial document metadata appropriately.
- Ensure authorization on download.
### Acceptance Criteria
- Each document renders correctly.
- Each source record maps to its own document rules.
- No universal business document abstraction is introduced.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-046 — Implement portal dashboard and pet records
**Epic:** R9 Portal
**Goal:** Expose authorized pet information through a dedicated portal experience.
**Dependencies:** VF-022,VF-021

### Description
Build /portal dashboard, pets, medical history and appointments using portal-specific APIs.
### Implementation
- Portal layout.
- My Pets.
- Pet profile.
- Timeline.
- Appointments.
- Authorization-aware queries.
### Acceptance Criteria
- Client sees only authorized pets.
- Mobile portal is usable.
- Staff-only fields are not exposed.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-047 — Implement portal documents, prescriptions, labs and billing
**Epic:** R9 Portal
**Goal:** Complete client self-service document and financial access.
**Dependencies:** VF-045,VF-046

### Description
Expose permitted prescriptions, lab reports, vaccination certificates, invoices and receipts.
### Implementation
- Portal document list.
- Secure download.
- Prescription display.
- Lab result display after configured review/share state.
- Invoice/payment display.
### Acceptance Criteria
- Unauthorized documents cannot be accessed.
- Reviewed/shareable lab results appear.
- PDF downloads are secure.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-048 — Implement notification orchestration
**Epic:** R9 Notifications
**Goal:** Create outbox-driven notification workflow without directly coupling clinical modules to providers.
**Dependencies:** VF-024,VF-028,VF-035

### Description
Create notification commands/events and provider interfaces for email/SMS/WhatsApp integration.
### Implementation
- Notification entity/status.
- Templates.
- Retry strategy.
- Provider adapter interfaces.
- Appointment/follow-up/result notification handlers.
### Acceptance Criteria
- Notification work is durable.
- Provider failure does not corrupt clinical transaction.
- Retries are bounded/idempotent.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-049 — Implement operational dashboards
**Epic:** R10 Reporting
**Goal:** Create dashboards for appointments, queue, clinical activity and follow-ups.
**Dependencies:** VF-025,VF-028

### Description
Create read-optimized queries; do not compromise transactional domain models to serve dashboards.
### Implementation
- Today's appointments.
- Waiting queue.
- Follow-ups due/overdue.
- Clinical workload.
- Vaccination due/overdue.
### Acceptance Criteria
- Dashboard data respects tenant/branch authorization.
- Queries are paginated/optimized.
- No cross-tenant data.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-050 — Implement financial and inventory reports
**Epic:** R10 Reporting
**Goal:** Create financial and inventory reporting.
**Dependencies:** VF-038,VF-042

### Description
Build invoice/payment, revenue, stock, expiry and purchase reporting.
### Implementation
- Revenue/payment report.
- Outstanding invoices.
- Stock report.
- Expiry report.
- Purchase report.
### Acceptance Criteria
- Totals reconcile with source transactions.
- Reports respect date/timezone and branch scope.
- Large reports do not block request threads unnecessarily.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-051 — Implement tenant isolation regression suite
**Epic:** R10 Security
**Goal:** Prove tenant and portal isolation.
**Dependencies:** VF-015,VF-022

### Description
Create automated negative tests for cross-tenant, cross-branch and cross-client access.
### Implementation
- Cross-tenant API tests.
- IDOR tests.
- Portal pet access tests.
- Branch scope tests.
- Document download authorization tests.
### Acceptance Criteria
- All isolation tests pass.
- Any unauthorized access returns safe denial.
- Regression suite runs in CI.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-052 — Implement backup and restore validation
**Epic:** R10 Production
**Goal:** Make data recovery testable.
**Dependencies:** VF-011,VF-043

### Description
Document and automate backup/restore validation for Platform DB, tenant DBs and object storage.
### Implementation
- Backup jobs/configuration.
- Tenant-level restore procedure.
- Object storage backup/versioning strategy.
- Restore test evidence.
### Acceptance Criteria
- Backup success is observable.
- A tenant can be restored in a controlled test.
- Recovery procedures are documented.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-053 — Production readiness hardening
**Epic:** R10 Production
**Goal:** Harden the application for production.
**Dependencies:** VF-051,VF-052

### Description
Perform security, performance, resilience and operational hardening before production release.
### Implementation
- Security headers.
- Rate limiting.
- Secret management.
- DB connection limits.
- Slow query review.
- OpenTelemetry dashboards.
- Container hardening.
- Migration safety review.
- Load tests for critical workflows.
### Acceptance Criteria
- Critical security findings resolved.
- Critical workflows meet agreed performance targets.
- Production runbook exists.
- Release checklist passes.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-054 — Create AI-safe data access boundary
**Epic:** R11 AI Readiness
**Goal:** Prepare future AI services without giving models direct unrestricted DB access.
**Dependencies:** VF-053

### Description
Create authorized query/service boundaries for patient history, documents and operational summaries.
### Implementation
- Define AI read models/contracts.
- Enforce tenant/client/clinical authorization.
- Return minimum necessary data.
- Audit AI data access.
### Acceptance Criteria
- AI cannot bypass normal authorization.
- Data access is tenant scoped.
- Sensitive data access is auditable.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.

## VF-055 — Create AI suggestion workflow contract
**Epic:** R11 AI Readiness
**Goal:** Allow future AI assistants to return drafts requiring human acceptance.
**Dependencies:** VF-054

### Description
Define a generic-but-small suggestion contract and acceptance path without coupling VetFlow to a specific model provider.
### Implementation
- Suggestion metadata.
- Source references.
- Confidence/limitations metadata where appropriate.
- Human acceptance/edit/reject.
- Audit accepted AI suggestions.
### Acceptance Criteria
- AI suggestions cannot directly finalize clinical records.
- Acceptance is explicit.
- Provider/model can change without changing clinical workflow contracts.
### Antigravity execution contract
- Inspect current repository and relevant docs before coding.
- Do not modify unrelated scope.
- Avoid speculative abstractions and over-DRY.
- Run build/tests and provide evidence.
- Report files changed, migrations, tests, and unresolved decisions.
