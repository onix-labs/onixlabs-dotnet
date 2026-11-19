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
/// Provides Project Wycheproof AES Key Wrap (RFC 3394) test vectors as theory data.
/// </summary>
/// <remarks>
/// The vectors are sourced from <c>https://github.com/C2SP/wycheproof/blob/main/testvectors_v1/aes_wrap_test.json</c> (the AES-WRAP file;
/// <c>testvectors_v1</c> has no file named <c>aes_kw_test.json</c>) and are embedded as the resource <c>wycheproof_aes_wrap_test.json</c>.
/// Each row contains a test-case identifier, the key-encryption key, the key to wrap, the wrapped key, the upstream <c>result</c>
/// (<c>"valid"</c>, <c>"acceptable"</c> or <c>"invalid"</c>), and the upstream flags joined with commas.
/// </remarks>
public sealed class WycheproofAesKeyWrapDataAttribute : DataAttribute
{
    private const string ResourceName = "OnixLabs.Security.Cryptography.UnitTests.Data.wycheproof_aes_wrap_test.json";

    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(MethodInfo testMethod, DisposalTracker disposalTracker)
    {
        List<ITheoryDataRow> rows = [];

        using Stream stream = typeof(WycheproofAesKeyWrapDataAttribute).Assembly.GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {ResourceName}");

        using JsonDocument document = JsonDocument.Parse(stream);

        foreach (JsonElement group in document.RootElement.GetProperty("testGroups").EnumerateArray())
        foreach (JsonElement test in group.GetProperty("tests").EnumerateArray())
        {
            int tcId = test.GetProperty("tcId").GetInt32();
            byte[] key = Convert.FromHexString(test.GetProperty("key").GetString() ?? string.Empty);
            byte[] message = Convert.FromHexString(test.GetProperty("msg").GetString() ?? string.Empty);
            byte[] ciphertext = Convert.FromHexString(test.GetProperty("ct").GetString() ?? string.Empty);
            string result = test.GetProperty("result").GetString() ?? "invalid";
            string flags = string.Join(",", test.GetProperty("flags").EnumerateArray().Select(flag => flag.GetString()));

            rows.Add(new TheoryDataRow(tcId, key, message, ciphertext, result, flags));
        }

        return new ValueTask<IReadOnlyCollection<ITheoryDataRow>>(rows);
    }

    public override bool SupportsDiscoveryEnumeration() => false;
}
