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
using System.IO;
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.UnitTests.Data.Jose;

/// <summary>
/// Provides the published JOSE and DIDComm test vectors, embedded as JSON resources.
/// </summary>
/// <remarks>
/// Each file was generated from its source document by extracting the published values, removing only the whitespace the documents insert
/// for line wrapping. Each file's <c>source</c> member names the document and the sections it was taken from:
/// <list type="bullet">
/// <item><description><c>rfc7520_4_3_es512.json</c>: RFC 7520 §4.3, Figures 2, 28 and 29.</description></item>
/// <item><description><c>rfc7520_5_4_ecdh_es_a128kw.json</c>: RFC 7520 §5.4, Figures 72 and 108 to 119.</description></item>
/// <item><description><c>rfc8037_appendix_a.json</c>: RFC 8037 Appendix A.1 to A.6.</description></item>
/// <item><description><c>rfc7638_3_1_thumbprint.json</c>: RFC 7638 §3.1.</description></item>
/// <item><description><c>ecdh_1pu_04_appendix_b.json</c>: draft-madden-jose-ecdh-1pu-04 Appendix B.</description></item>
/// <item><description><c>didcomm_v2_1_appendix.json</c>: DIDComm Messaging v2.1, Appendices A, B and C.</description></item>
/// </list>
/// </remarks>
public static class JoseTestVectors
{
    /// <summary>
    /// Loads the specified embedded test vector file.
    /// </summary>
    /// <param name="fileName">The file name, such as <c>rfc8037_appendix_a.json</c>.</param>
    /// <returns>Returns the root JSON element of the file.</returns>
    public static JsonElement Load(string fileName)
    {
        string resourceName = $"OnixLabs.Security.Cryptography.UnitTests.Data.Jose.{fileName}";

        using Stream stream = typeof(JoseTestVectors).Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource not found: {resourceName}");

        using JsonDocument document = JsonDocument.Parse(stream);
        return document.RootElement.Clone();
    }

    /// <summary>
    /// Gets the specified member of the specified element as JSON text.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the raw JSON text of the member.</returns>
    public static string GetJson(this JsonElement element, string name) => element.GetProperty(name).GetRawText();

    /// <summary>
    /// Gets the specified string member of the specified element.
    /// </summary>
    /// <param name="element">The JSON object.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns the string value.</returns>
    public static string GetText(this JsonElement element, string name) =>
        element.GetProperty(name).GetString() ?? throw new InvalidOperationException($"The member '{name}' is not a string.");
}
