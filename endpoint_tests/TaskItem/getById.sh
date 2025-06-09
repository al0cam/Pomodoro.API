#!/bin/bash

# This script retrieves a TaskItem by its ID from a REST API.


DEFAULT_BASE_URL="${1:-localhost:5000}"
DEFAULT_ENDPOINT="${2:-/api/TaskItem}"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"

# Default to 1 if no ID is provided
id="${3:-1}"
http --check-status --ignore-stdin --timeout=2.5 \
GET "${DEFAULT_URL}/${id}" \
  --verify=no \
  --timeout=10 \
  --ignore-stdin \
  --print=HhBb
