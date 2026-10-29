---
description: Check a sha-<short> image exists in GHCR and show how to roll an environment to it (no file in the repo pins the tag).
argument-hint: "<short-sha>"
---

Given `$ARGUMENTS` = 8-char short SHA:

1. Confirm the tag exists in GHCR: `gh api /user/packages/container/${REPO}/versions | jq '.[] | .metadata.container.tags' | grep sha-$ARGUMENTS`
2. Nothing in the repo pins the image tag: `docker-compose.prod.yml` reads `image: ${IMAGE_REF}`, and the deploy workflows pass a digest-pinned `image-ref` to `deploy-ec2-ssh`. Do NOT edit compose files.
3. To ship new code to prod, push a `YYYYMMDD_{{PROJECT_NAME}}_Release` tag (runs `deploy-prod.yml`). To put a host back on an existing image, follow `docs/runbooks/rollback.md` with that image's digest.
4. Report what you found; do NOT commit or push anything.
