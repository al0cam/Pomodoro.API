#!/bin/bash

DEFAULT_BASE_URL="${1:-localhost:5000}"
DEFAULT_ENDPOINT="${2:-/login}"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"

email="${3:-jakov@email.com}"
password="${4:-StrongPassword!123}"

http --check-status --ignore-stdin --timeout=2.5 \
  POST "${DEFAULT_URL}" \
  "email=${email}" \
  "password=${password}"

