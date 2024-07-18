#!/bin/bash

# Ensure that the script stops if any command fails
set -e

# Function to display usage instructions
usage() {
    echo "Usage: $0 <repository-path> <start-tag> <end-tag> [<grep-pattern>]"
    exit 1
}

# Check if the correct number of arguments are provided
if [ "$#" -lt 3 ]; then
    usage
fi

REPO_PATH=$1
START_TAG=$2
END_TAG=$3
GREP_PATTERN=${4:-} # Optional fourth argument for grep pattern

# Change to the specified repository path
cd "$REPO_PATH"

# Ensure we are in a git repository
if [ ! -d .git ]; then
    echo "Error: $REPO_PATH is not a git repository."
    exit 1
fi

# Fetch the commit messages between the specified tags, including all commits
if [ -z "$GREP_PATTERN" ]; then
    # No grep pattern provided, just list the commit messages
    git log --oneline "$START_TAG".."$END_TAG"
else
    # Grep pattern provided, filter the commit messages
    git log --oneline "$START_TAG".."$END_TAG" | grep "$GREP_PATTERN"
fi
