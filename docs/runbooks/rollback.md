# Rollback

## EC2 (SSH)
1. SSH to the instance: `ssh $EC2_USER@$EC2_HOST`.
2. `cd $EC2_APP_DIR`
3. Set `IMAGE_REF` to the last-known-good digest: `export IMAGE_REF=ghcr.io/owner/repo@sha256:<digest>`.
4. `IMAGE_REF=$IMAGE_REF docker compose pull && IMAGE_REF=$IMAGE_REF docker compose up -d`.
5. Verify health: `curl http://localhost:8080/health`.

## K3s (Helm)
1. `helm history <release> -n <namespace>`
2. `helm rollback <release> <revision> -n <namespace>`
3. `kubectl rollout status deployment/<name> -n <namespace>`
