// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents the entry point for the OnixLabs.Core benchmarks.
/// </summary>
/// <remarks>
/// Run with <c>dotnet run -c Release -f net10.0 --project OnixLabs.Core.Benchmarks -- [arguments]</c>; arguments are passed to BenchmarkDotNet,
/// for example <c>--filter *</c> or <c>--list tree</c>. Every benchmark reports allocations as well as time. See this project's README.
/// </remarks>
internal static class Program
{
    /// <summary>
    /// Runs the benchmarks selected by the specified arguments.
    /// </summary>
    /// <param name="args">The command-line arguments, passed to BenchmarkDotNet.</param>
    private static void Main(string[] args)
    {
        IConfig config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }
}
