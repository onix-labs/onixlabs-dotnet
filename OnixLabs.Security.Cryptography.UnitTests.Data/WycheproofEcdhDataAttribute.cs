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
/// Provides Project Wycheproof EC Diffie-Hellman test vectors for one NIST curve as theory data.
/// </summary>
/// <param name="curve">The Wycheproof curve name: <c>secp256r1</c>, <c>secp384r1</c> or <c>secp521r1</c>.</param>
/// <remarks>
/// The vectors are sourced from <c>https://github.com/C2SP/wycheproof/blob/main/testvectors_v1/ecdh_&lt;curve&gt;_test.json</c>, whose
/// public keys are DER-encoded SubjectPublicKeyInfo, and are embedded as the resources <c>wycheproof_ecdh_&lt;curve&gt;_test.json</c>.
/// Each row contains a test-case identifier, the curve name, the raw big-endian private scalar, the public key, the expected shared secret,
/// the upstream <c>result</c> (<c>"valid"</c>, <c>"acceptable"</c> or <c>"invalid"</c>), and the upstream flags joined with commas.
/// </remarks>
public sealed class WycheproofEcdhDataAttribute(string curve) : DataAttribute
{
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        string resourceName = $"OnixLabs.Security.Cryptography.UnitTests.Data.wycheproof_ecdh_{curve}_test.json";
        List<ITheoryDataRow> rows = [];

        using Stream stream = typeof(WycheproofEcdhDataAttribute).Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {resourceName}");

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

            rows.Add(new TheoryDataRow(tcId, curve, privateKey, publicKey, shared, result, flags));
        }

        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
    }

    public override bool SupportsDiscoveryEnumeration() => false;
}
