#!/usr/bin/env bash

set -euo pipefail
export USE_FULL_NUMERIC_PROVIDER=true


echo "Pulling the latest repository changes"
git pull


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
git merge main --no-edit


echo "Pushing release branch to origin"
git push origin release


echo "Building and testing .NET solution"
dotnet build --configuration Release
dotnet test --configuration Release


echo "Creating new tag with version: $VERSION"
git tag "$VERSION"
git push origin "$VERSION"


echo "Pushing packages to GitHub package registry"
dotnet nuget push "OnixLabs.Core/bin/Release/OnixLabs.Core.$VERSION.nupkg" --source "github" 
dotnet nuget push "OnixLabs.DependencyInjection/bin/Release/OnixLabs.DependencyInjection.$VERSION.nupkg" --source "github"
dotnet nuget push "OnixLabs.Numerics/bin/Release/OnixLabs.Numerics.$VERSION.nupkg" --source "github"
dotnet nuget push "OnixLabs.Security/bin/Release/OnixLabs.Security.$VERSION.nupkg" --source "github"
dotnet nuget push "OnixLabs.Security.Cryptography/bin/Release/OnixLabs.Security.Cryptography.$VERSION.nupkg" --source "github"


echo "Pushing packages to NuGet package registry"
dotnet nuget push "OnixLabs.Core/bin/Release/OnixLabs.Core.$VERSION.nupkg" --source "nuget.org" --api-key "$NUGET_API_KEY"
dotnet nuget push "OnixLabs.DependencyInjection/bin/Release/OnixLabs.DependencyInjection.$VERSION.nupkg" --source "nuget.org" --api-key "$NUGET_API_KEY"
dotnet nuget push "OnixLabs.Numerics/bin/Release/OnixLabs.Numerics.$VERSION.nupkg" --source "nuget.org" --api-key "$NUGET_API_KEY"
dotnet nuget push "OnixLabs.Security/bin/Release/OnixLabs.Security.$VERSION.nupkg" --source "nuget.org" --api-key "$NUGET_API_KEY"
dotnet nuget push "OnixLabs.Security.Cryptography/bin/Release/OnixLabs.Security.Cryptography.$VERSION.nupkg" --source "nuget.org" --api-key "$NUGET_API_KEY"


echo "Build and release process completed successfully!"
