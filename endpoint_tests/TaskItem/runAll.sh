#!/bin/bash

# This script runs a series of HTTP requests to test a REST API for TaskItem management.

DEFAULT_BASE_URL="localhost:5000"
DEFAULT_ENDPOINT="/api/TaskItem"
DEFAULT_URL="${DEFAULT_BASE_URL}${DEFAULT_ENDPOINT}"


# key-value map of passed tests
declare -A tests

check_status() {
  local test_name="$1"
  if [ $? -ne 0 ]; then
    tests[ "$test_name" ]="\e[31mFAIL\e[0m"
    echo "$test_name: FAIL"
  else
    tests[ "$test_name" ]="\e[33mPASS\e[0m"
    echo "$test_name: PASS"
  fi
}

# Create a new TaskItem
echo "Creating a new TaskItem..."
create_response=$(bash post.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}")
created_id=$(echo "$create_response" | jq -r '.id')

if [ -z "$created_id" ] || [ "$created_id" == "null" ]; then
  echo "FAIL: Could not retrieve created ID."
  exit 1
fi
check_status "Create TaskItem"

# Get the newly created TaskItem by ID
echo "Retrieving newly created TaskItem with ID $created_id..."
bash getById.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}" "$created_id"
check_status "Get TaskItem by ID"

# Update the TaskItem by ID
echo "Updating TaskItem with ID $created_id..."
bash put.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}" "$created_id"
check_status "Update TaskItem by ID"

# Get the updated TaskItem by ID
echo "Retrieving updated TaskItem with ID $created_id..."
bash getById.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}" "$created_id"
check_status "Get Updated TaskItem by ID"

# Delete the TaskItem by ID
echo "Deleting TaskItem with ID $created_id..."
bash delete.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}" "$created_id"
check_status "Delete TaskItem by ID"

# Get all TaskItems after deletion
echo "Retrieving all TaskItems after deletion..."
bash getAll.sh "${DEFAULT_BASE_URL}" "${DEFAULT_ENDPOINT}"
check_status "Get All TaskItems after Deletion"


# Print the results of the tests
echo -e "\n--- Test Results Summary ---"
for test_name in "${!tests[@]}"; do
  printf "%-45s: %b\n" "$test_name" "${tests[$test_name]}"
done
echo "------------------------------------------------------------"
# End of script
