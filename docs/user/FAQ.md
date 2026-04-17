# FAQ

## Why no Application Insights / Sentry?
CI bans telemetry SDKs. Log to stdout; aggregation lives outside the service.

## Why Native AOT?
Cold start < 100ms, ~15MB image. Tradeoff: no reflection-heavy libs.
