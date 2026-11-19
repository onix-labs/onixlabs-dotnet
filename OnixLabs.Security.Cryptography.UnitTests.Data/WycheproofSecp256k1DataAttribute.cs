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
using System.Security.Cryptography;
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

/// <summary>
/// Provides Project Wycheproof secp256k1 ECDSA (SHA-256) verification test vectors as theory data.
/// </summary>
/// <param name="format">The signature format of the file to load: DER or IEEE P1363.</param>
/// <remarks>
/// The vectors are sourced from <c>https://github.com/C2SP/wycheproof/blob/main/testvectors_v1/ecdsa_secp256k1_sha256_test.json</c> (DER)
/// and <c>ecdsa_secp256k1_sha256_p1363_test.json</c> (IEEE P1363), and are embedded as resources of the same names prefixed with
/// <c>wycheproof_</c>. Each row contains a test-case identifier, the signature format, the DER-encoded SubjectPublicKeyInfo of the group,
/// the message, the signature, the upstream <c>result</c> (<c>"valid"</c> or <c>"invalid"</c>; neither file has acceptable cases), and the
/// upstream flags joined with commas.
/// </remarks>
public sealed class WycheproofSecp256k1DataAttribute(DSASignatureFormat format) : DataAttribute
{
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        string suffix = format == DSASignatureFormat.IeeeP1363FixedFieldConcatenation ? "_p1363" : string.Empty;
        string resourceName = $"OnixLabs.Security.Cryptography.UnitTests.Data.wycheproof_ecdsa_secp256k1_sha256{suffix}_test.json";
        List<ITheoryDataRow> rows = [];

        using Stream stream = typeof(WycheproofSecp256k1DataAttribute).Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {resourceName}");

        using JsonDocument document = JsonDocument.Parse(stream);

        foreach (JsonElement group in document.RootElement.GetProperty("testGroups").EnumerateArray())
        {
            byte[] publicKey = Convert.FromHexString(group.GetProperty("publicKeyDer").GetString() ?? string.Empty);

            foreach (JsonElement test in group.GetProperty("tests").EnumerateArray())
            {
                int tcId = test.GetProperty("tcId").GetInt32();
                byte[] message = Convert.FromHexString(test.GetProperty("msg").GetString() ?? string.Empty);
                byte[] signature = Convert.FromHexString(test.GetProperty("sig").GetString() ?? string.Empty);
                string result = test.GetProperty("result").GetString() ?? "invalid";
                string flags = string.Join(",", test.GetProperty("flags").EnumerateArray().Select(flag => flag.GetString()));

                rows.Add(new TheoryDataRow(tcId, format, publicKey, message, signature, result, flags));
            }
        }

        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
    }

    public override bool SupportsDiscoveryEnumeration() => false;
}
