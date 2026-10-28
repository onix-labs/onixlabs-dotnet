# OnixLabs.Core.Benchmarks

BenchmarkDotNet benchmarks for OnixLabs.Core: Base16, Base32, Base58 and Base64 encoding and decoding, and `Enumeration<T>` lookups. Every benchmark reports allocations as well as time.

`Directory.Build.props` configures any project ending in `.Benchmarks` as an executable that references BenchmarkDotNet. It is neither packed nor run as a test. CI builds it, so it cannot rot, but never runs it.

## Running

Always run in Release, for one target framework:

```sh
# Everything
dotnet run -c Release -f net10.0 --project OnixLabs.Core.Benchmarks -- --filter '*'

# One class or method
dotnet run -c Release -f net10.0 --project OnixLabs.Core.Benchmarks -- --filter '*Codec*'

# Compare runtimes
dotnet run -c Release -f net10.0 --project OnixLabs.Core.Benchmarks -- --filter '*Codec*' --runtimes net8.0 net10.0

# List what exists
dotnet run -c Release -f net10.0 --project OnixLabs.Core.Benchmarks -- --list tree
```

Benchmark on a quiet machine. Other CPU-heavy work, including another project's benchmarks, changes clock speed, cache and memory bandwidth enough to skew results. Discard a run that overlapped other load rather than patching it.

## Reporting a change

A performance change should include before and after numbers for the benchmarks it affects in its pull request description. Use the same machine and target framework for both runs. Add a benchmark here whenever a change targets a path that none of the existing ones exercise.

Benchmark classes are not sealed, because BenchmarkDotNet generates a class that derives from each one.
