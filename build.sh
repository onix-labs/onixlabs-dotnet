#!/usr/bin/env bash
set -euo pipefail

export USE_FULL_NUMERIC_PROVIDER=true

# Safeguards

if [ -z "${NUGET_API_KEY:-}" ]; then
  echo "ERROR: NUGET_API_KEY is not set in the environment."
  echo "Add: export NUGET_API_KEY=\"<your-nuget-api-key>\" to your shell profile."
  exit 1
fi

if [ -z "${GITHUB_TOKEN:-}" ]; then
  echo "ERROR: GITHUB_TOKEN is not set in the environment."
  echo "Add: export GITHUB_TOKEN=\"<your-github-pat-with-write:packages>\" to your shell profile."
  exit 1
fi

# Git Preparation

echo "Pulling the latest repository changes"
git pull --ff-only

echo "Checking out main branch"
git checkout main

echo "Obtaining release version"
VERSION=$(grep "<Version>" Directory.Build.props | sed -E 's/.*<Version>(.*)<\/Version>.*/\1/')

if [ -z "$VERSION" ]; then
  echo "Could not find <Version> in Directory.Build.props. Exiting..."
  exit 1
fi

echo "Detected release version: $VERSION"

echo "Checking out release branch"
git checkout release

echo "Merging main branch into release branch"
git merge --no-ff main -m "Merging main into release with version: $VERSION"

echo "Pushing release branch to origin"
git push origin release

# Build, Test, and Pack

echo "Restoring .NET solution"
dotnet restore

echo "Building .NET solution (Release)"
dotnet build --configuration Release --no-restore

echo "Running tests (Release)"
dotnet test --configuration Release --no-build

echo "Packing projects (Release)"
dotnet pack --configuration Release --no-build

# Create Git Tag

echo "Creating annotated tag: v$VERSION"
git tag -a "v$VERSION" -m "Release v$VERSION"
git push origin "v$VERSION"

# Publish Helpers

PACKAGES=(
  "OnixLabs.Core"
  "OnixLabs.DependencyInjection"
  "OnixLabs.Numerics"
  "OnixLabs.Security"
  "OnixLabs.Security.Cryptography"
)

# Push to GitHub Packages

echo "Pushing packages to GitHub package registry"
for pkg in "${PACKAGES[@]}"; do
  nupkg="\"$pkg/bin/Release/$pkg.$VERSION.nupkg\""
  # --skip-duplicate makes the script safe to re-run
  eval dotnet\ nuget\ push\ $nupkg\ --source\ \"github\"\ --api-key\ \"\$GITHUB_TOKEN\"\ --skip-duplicate
done

# Push to NuGet.org

echo "Pushing packages to NuGet package registry"
for pkg in "${PACKAGES[@]}"; do
  nupkg="\"$pkg/bin/Release/$pkg.$VERSION.nupkg\""
  eval dotnet\ nuget\ push\ $nupkg\ --source\ \"nuget.org\"\ --api-key\ \"\$NUGET_API_KEY\"\ --skip-duplicate
done

# DONE

echo "Build and release process completed successfully!"
