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

using System.IO;
using System.Linq;
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

/// <summary>
/// Provides Project Wycheproof X25519 (XDH) key agreement test vectors as theory data.
/// </summary>
/// <remarks>
/// The vectors are sourced from <c>https://github.com/C2SP/wycheproof/blob/main/testvectors_v1/x25519_test.json</c>
/// and are embedded as the resource <c>wycheproof_x25519_test.json</c>. Each row contains a test-case identifier, the private key
/// bytes, the public key bytes, the expected shared secret bytes, the upstream <c>result</c> (<c>"valid"</c> or <c>"acceptable"</c>),
/// and the upstream flags joined with commas. Every case in this file has a defined RFC 7748 output, including the
/// <c>"acceptable"</c> ones (twist points, non-canonical u-coordinates and low-order points), so every <c>shared</c> value is checked.
/// </remarks>
public sealed class WycheproofX25519DataAttribute : DataAttribute
{
    private const string ResourceName = "OnixLabs.Security.Cryptography.UnitTests.Data.wycheproof_x25519_test.json";

    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        List<ITheoryDataRow> rows = [];

        using Stream stream = typeof(WycheproofX25519DataAttribute).Assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {ResourceName}");

        using JsonDocument document = JsonDocument.Parse(stream);

        foreach (JsonElement group in document.RootElement.GetProperty("testGroups").EnumerateArray())
        foreach (JsonElement test in group.GetProperty("tests").EnumerateArray())
        {
            int tcId = test.GetProperty("tcId").GetInt32();
            byte[] privateKey = Convert.FromHexString(test.GetProperty("private").GetString() ?? string.Empty);
            byte[] publicKey = Convert.FromHexString(test.GetProperty("public").GetString() ?? string.Empty);
            byte[] shared = Convert.FromHexString(test.GetProperty("shared").GetString() ?? string.Empty);
            string result = test.GetProperty("result").GetString() ?? "invalid";
            string flags = string.Join(",", test.GetProperty("flags").EnumerateArray().Select(flag => flag.GetString()));

            rows.Add(new TheoryDataRow(tcId, privateKey, publicKey, shared, result, flags));
        }

        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
    }

    public override bool SupportsDiscoveryEnumeration() => false;
}
