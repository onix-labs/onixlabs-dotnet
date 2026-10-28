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

using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using OnixLabs.Security.Cryptography.Benchmarks.Diagnostics;

namespace OnixLabs.Security.Cryptography.Benchmarks;

/// <summary>
/// Represents the entry point for the OnixLabs.Security.Cryptography benchmarks.
/// </summary>
/// <remarks>
/// Run with <c>dotnet run -c Release -f net10.0 --project OnixLabs.Security.Cryptography.Benchmarks -- [arguments]</c>. See this project's README.
/// <list type="bullet">
/// <item><description><c>finalizers</c> prints how many finalizers run per operation for native-resource-owning paths.</description></item>
/// <item><description>Any other arguments are passed to BenchmarkDotNet, for example <c>--filter *Ecdsa*</c> or <c>--list tree</c>.</description></item>
/// </list>
/// Every benchmark reports allocations as well as time.
/// </remarks>
internal static class Program
{
    private const string FinalizersCommand = "finalizers";

    /// <summary>
    /// Runs the benchmarks or the finalizer report, as selected by the specified arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    private static void Main(string[] args)
    {
        if (args is [FinalizersCommand])
        {
            FinalizerReport.Run(Console.Out);
            return;
        }

        IConfig config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }
}
