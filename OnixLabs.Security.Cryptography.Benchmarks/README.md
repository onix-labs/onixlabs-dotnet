# OnixLabs.Security.Cryptography.Benchmarks

BenchmarkDotNet benchmarks for OnixLabs.Security.Cryptography: ECDSA and Ed25519 signing and verification, hashing, and Merkle trees, plus a finalizer report for native-resource hygiene. Every benchmark reports allocations as well as time.

`Directory.Build.props` configures any project ending in `.Benchmarks` as an executable that references BenchmarkDotNet. It is neither packed nor run as a test. CI builds it, so it cannot rot, but never runs it.

## Running

Always run in Release, for one target framework:

```sh
# Everything
dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- --filter '*'

# One class or method
dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- --filter '*Ecdsa*'

# Compare runtimes
dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- --filter '*Ecdsa*' --runtimes net8.0 net10.0

# List what exists
dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- --list tree
```

Benchmark on a quiet machine. Other CPU-heavy work, including another project's benchmarks, changes clock speed, cache and memory bandwidth enough to skew results. Discard a run that overlapped other load rather than patching it.

## Finalizer report

```sh
dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- finalizers
```

This prints how many finalizers run per operation for each scenario that owns native resources: signing, verification, key import, key agreement and secrets. It counts the runtime's own `GCFinalizersEnd` events. Every scenario should read **0.00**. Anything else means a native handle reached the finalizer instead of being disposed, which costs CPU on the finalizer thread under load (see [#156](https://github.com/onix-labs/onixlabs-dotnet/issues/156)).

Some scenarios deliberately leave the library's own objects undisposed. They check that the library releases its internal native resources without depending on callers to dispose.

## Reporting a change

A performance change should include before and after numbers for the benchmarks it affects in its pull request description. Use the same machine and target framework for both runs. Add a benchmark here whenever a change targets a path that none of the existing ones exercise.

Benchmark classes are not sealed, because BenchmarkDotNet generates a class that derives from each one.
