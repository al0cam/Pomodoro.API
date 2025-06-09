#!/bin/bash

DEFAULT_BASE_URL="${1:-localhost:5000}"
DEFAULT_ENDPOINT="${2:-/api/TaskItem}"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"

http --check-status --ignore-stdin --timeout=2.5 \
  GET "${DEFAULT_URL}" \
  --verify=no \
  --timeout=10 \
  --ignore-stdin \
  --print=HhBb

