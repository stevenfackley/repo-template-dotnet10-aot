# {{PROJECT_NAME}} — System Design Document

## Stack
.NET 10 Native AOT, minimal API, distroless runtime. Deploy target: {{DEPLOY_TARGET}}.

## Runtime topology
<!-- Services, network zones, external dependencies. -->

## Data
<!-- Stores, schema location, migrations. -->

## Object storage (opt-in)
S3-compatible via `AWSSDK.S3` (AOT-compatible). Cloudflare R2 in prod, MinIO locally.
Wiring: `services.AddS3Storage(builder.Configuration)` in `Program.cs`. Options bind from `S3_*` env vars. `IAmazonS3` is only registered when `S3Options.IsConfigured` — features must guard on that.

## Auth
<!-- Who calls this service, how they prove identity. -->

## Observability
<!-- Stdout JSON logs. Log aggregator TBD. No Application Insights / Sentry / Datadog (CI-enforced). -->

## Failure modes
<!-- What happens when each dependency is down? -->

## Scaling
<!-- Bottlenecks, expected traffic, horizontal vs vertical. -->
