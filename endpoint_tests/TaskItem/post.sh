#!/bin/bash
# Debug
# set -x

DEFAULT_BASE_URL="${1:-localhost:5000}"
DEFAULT_ENDPOINT="${2:-/api/TaskItem}"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"

TASK_TITLE="Test item"

http --check-status --ignore-stdin POST "${DEFAULT_URL}" \
  "title=${TASK_TITLE}" \
  "description=This is a test task." \
  "dueAt=2025-06-16T13:41:00" \
  "estimatedPomodoros=2" \

