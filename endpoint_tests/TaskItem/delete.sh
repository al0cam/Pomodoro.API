#!/bin/bash
# Debug
# set -x

DEFAULT_BASE_URL="${1:-localhost:5000}"
DEFAULT_ENDPOINT="${2:-/api/TaskItem}"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"

# Default to 1 if no ID is provided
id="${3:-1}"
http DELETE "${DEFAULT_URL}/${id}"
