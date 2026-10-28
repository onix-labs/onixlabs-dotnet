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
using System.Diagnostics.Tracing;
using System.Threading;

namespace OnixLabs.Security.Cryptography.Benchmarks.Diagnostics;

/// <summary>
/// Represents a counter of the finalizers run by the runtime, read from the runtime's own GC events.
/// </summary>
/// <remarks>
/// Every finalizer run means an object owning a native resource, such as a <see cref="System.Runtime.InteropServices.SafeHandle"/>,
/// was not disposed. The count is process-wide, so measure in a quiet process and settle the GC before each reading.
/// </remarks>
internal sealed class FinalizerCounter : EventListener
{
    private const string RuntimeEventSourceName = "Microsoft-Windows-DotNETRuntime";
    private const string FinalizersEndEventName = "GCFinalizersEnd";
    private const EventKeywords GcKeyword = (EventKeywords)0x1;

    private long count;

    /// <summary>
    /// Gets the number of finalizers run since the counter was created.
    /// </summary>
    /// <value>The number of finalizers run since the counter was created.</value>
    public long Count => Interlocked.Read(ref count);

    /// <summary>
    /// Forces full garbage collections and waits for finalizers, so that every unreachable object has been finalized and counted.
    /// </summary>
    public static void Settle()
    {
        for (int pass = 0; pass < 3; pass++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // Why: runtime events reach the listener asynchronously, shortly after the GC that raised them.
        Thread.Sleep(millisecondsTimeout: 200);
    }

    /// <inheritdoc/>
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name == RuntimeEventSourceName)
            EnableEvents(eventSource, EventLevel.Informational, GcKeyword);
    }

    /// <inheritdoc/>
    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        if (eventData.EventName?.StartsWith(FinalizersEndEventName, StringComparison.Ordinal) is not true) return;
        if (eventData.Payload is not [object finalizerCount, ..]) return;

        Interlocked.Add(ref count, Convert.ToInt64(finalizerCount));
    }
}
