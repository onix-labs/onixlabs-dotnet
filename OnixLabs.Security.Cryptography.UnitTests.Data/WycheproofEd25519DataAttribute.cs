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
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

/// <summary>
/// Provides Project Wycheproof Ed25519 (EDDSA) verification test vectors as theory data.
/// </summary>
/// <remarks>
/// The vectors are sourced from <c>https://github.com/C2SP/wycheproof/blob/main/testvectors_v1/ed25519_test.json</c>
/// and are embedded as the resource <c>wycheproof_eddsa_test.json</c>. Each row contains a
/// test-case identifier, the public-key bytes, the message bytes, the signature bytes, and
/// the expected verification outcome (<see langword="true"/> when the upstream <c>result</c>
/// field is <c>"valid"</c>; <see langword="false"/> when it is <c>"invalid"</c>). Truncated or
/// over-long signatures are passed through as-is so the implementation's length checks are
/// exercised. Cofactored verification is permitted to accept signatures flagged with
/// <c>SignatureMalleability</c> only when the upstream <c>result</c> is <c>"acceptable"</c>;
/// in this corpus all malleability vectors are marked <c>"invalid"</c> because the upstream
/// expectation is that <c>S</c> is checked to be canonical.
/// </remarks>
public sealed class WycheproofEd25519DataAttribute : DataAttribute
{
    private const string ResourceName = "OnixLabs.Security.Cryptography.UnitTests.Data.wycheproof_eddsa_test.json";

    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        List<ITheoryDataRow> rows = [];

        using Stream stream = typeof(WycheproofEd25519DataAttribute).Assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {ResourceName}");

        using JsonDocument document = JsonDocument.Parse(stream);
        JsonElement root = document.RootElement;

        foreach (JsonElement group in root.GetProperty("testGroups").EnumerateArray())
        {
            string publicKeyHex = group.GetProperty("publicKey").GetProperty("pk").GetString()
                ?? throw new InvalidOperationException("Missing public key value.");
            byte[] publicKey = HexToBytes(publicKeyHex);

            foreach (JsonElement test in group.GetProperty("tests").EnumerateArray())
            {
                int tcId = test.GetProperty("tcId").GetInt32();
                string messageHex = test.GetProperty("msg").GetString() ?? string.Empty;
                string signatureHex = test.GetProperty("sig").GetString() ?? string.Empty;
                string result = test.GetProperty("result").GetString() ?? "invalid";
                bool expected = result == "valid" || result == "acceptable";

                rows.Add(new TheoryDataRow(tcId, publicKey, HexToBytes(messageHex), HexToBytes(signatureHex), expected));
            }
        }

        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
    }

    public override bool SupportsDiscoveryEnumeration() => false;

    private static byte[] HexToBytes(string hex)
    {
        if (hex.Length == 0) return [];
        if ((hex.Length & 1) != 0) throw new FormatException("Odd-length hex string.");
        byte[] bytes = new byte[hex.Length >> 1];
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)((HexDigit(hex[i << 1]) << 4) | HexDigit(hex[(i << 1) + 1]));
        return bytes;
    }

    private static int HexDigit(char c) => c switch
    {
        >= '0' and <= '9' => c - '0',
        >= 'a' and <= 'f' => c - 'a' + 10,
        >= 'A' and <= 'F' => c - 'A' + 10,
        _ => throw new FormatException($"Invalid hex digit '{c}'.")
    };
}
