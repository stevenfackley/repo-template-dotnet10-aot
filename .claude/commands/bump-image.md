---
description: Roll the image tag in docker-compose.prod.yml / helm values to a new sha-<short>.
argument-hint: "<short-sha>"
---

Given `$ARGUMENTS` = 8-char short SHA:

1. Confirm the tag exists in GHCR: `gh api /user/packages/container/${REPO}/versions | jq '.[] | .metadata.container.tags' | grep sha-$ARGUMENTS`
2. Edit `docker-compose.prod.yml` — replace any existing `sha-[0-9a-f]+` with `sha-$ARGUMENTS`.
3. If `helm/values.yaml` exists, bump `image.tag` there too.
4. Run `git diff` and show the user what changed; do NOT commit.
