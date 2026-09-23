# Decisions

ADR log. One entry per architectural decision. Append-only; supersede with a new entry.

## Format

```
## {{DATE}} — {{title}}
**Status:** proposed | accepted | superseded by #N
**Context:** why we had to decide
**Decision:** what we chose
**Consequences:** what follows (pros, cons, risks)
```

---

## {{DATE}} — Initial stack: .NET 10 Native AOT

**Status:** accepted
**Context:** Greenfield service under portfolio `repo-template-dotnet10-aot`. Target: fast cold-start, small image, Linux deploys.
**Decision:** .NET 10 with `PublishAot=true`, `linux-musl-x64`, distroless static runtime.
**Consequences:**
- Cold start < 100ms, image ~15MB.
- Reflection, dynamic code gen restricted — must stay AOT-compatible.
- No Application Insights SDK (banned by CI); stdout logs only.

## 2026-09-08 — Dependabot sweep: AWSSDK.S3 3.7 → 4.0 (major)

**Status:** accepted (awareness-only stub per saved sweep policy)
**Context:** Dependabot #10 bumped `AWSSDK.S3` 3.7.411 → 4.0.102.5; restore/build/test and the AOT publish lane were green on the PR head, so it was squash-merged as-is.
**Decision:** Take the major. The template only wires the client; nothing exercises S3 at test time, so CI green proves compile-compatibility, not runtime behaviour.
**Consequences:** Things to watch in any service scaffolded from this template:
- v4 is async-only — the synchronous client methods are gone.
- Value-typed request/response properties became nullable (`long?`, `bool?`, `DateTime?`); code that read them directly now needs `.Value` or a null check.
- Uploads send request-integrity checksums by default; S3-compatible stores (R2, MinIO) can reject them — set `RequestChecksumCalculation` / `ResponseChecksumValidation` to `WHEN_REQUIRED` on the client config when not talking to real S3.
- Keep every `AWSSDK.*` package on the same major; a 3.x Core next to a 4.x service package fails at restore.

## 2026-09-22 — Dependabot sweep: AWSSDK.Extensions.NETCore.Setup 3.7 → 4.0 (major)

**Status:** accepted (awareness-only stub per saved sweep policy)
**Context:** Dependabot #18 bumped `AWSSDK.Extensions.NETCore.Setup` 3.7.301 → 4.0.101.4. Since #10, `AWSSDK.S3` was already on 4.x, so the template was mixing AWSSDK majors. template-check (render, restore, format, build, test) was green on the PR head, and it was squash-merged.
**Decision:** Take the major. This realigns every `AWSSDK.*` package on 4.x. The template references the package but never calls `AddAWSService`/`GetAWSOptions`, so CI proves restore/compile compatibility only.
**Consequences:** Scaffolded services that start using `AddAWSService` pick up the v4 async-only, nullable-property semantics described in the S3 entry above. template-check does not run the AOT publish, so re-check trim/AOT warnings the first time a child repo publishes with the Setup extensions wired in.
