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
using System.Security.Cryptography;
using System.Text;
using OnixLabs.Security.Cryptography.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests;

/// <summary>
/// Verifies ECDH-1PU against the worked examples of draft-madden-jose-ecdh-1pu-04, using only the library's primitives.
/// </summary>
public sealed class Ecdh1PuTests
{
    /// <summary>
    /// The FixedInfo (Concat KDF OtherInfo) of draft-madden-jose-ecdh-1pu-04 Appendix A, for "A256GCM", "Alice", "Bob" and 256 bits.
    /// </summary>
    private const string AppendixAFixedInfo =
        "00 00 00 07 41 32 35 36 47 43 4d 00 00 00 05 41" +
        "6c 69 63 65 00 00 00 03 42 6f 62 00 00 01 00";

    /// <summary>
    /// The FixedInfo of draft-madden-jose-ecdh-1pu-04 Appendix B.9, for "ECDH-1PU+A128KW", "Alice", "Bob and Charlie", 128 bits and the
    /// cctag.
    /// </summary>
    private const string AppendixBFixedInfo =
        "00 00 00 0f 45 43 44 48 2d 31 50 55 2b 41 31 32" +
        "38 4b 57 00 00 00 05 41 6c 69 63 65 00 00 00 0f" +
        "42 6f 62 20 61 6e 64 20 43 68 61 72 6c 69 65 00" +
        "00 00 80 00 00 00 20 1c b6 f8 7d 39 66 f2 ca 46" +
        "9a 28 f7 47 23 ac da 02 78 0e 91 cc e2 18 55 47" +
        "07 45 fe 11 9b dd 64";

    /// <summary>
    /// Verifies draft-madden-jose-ecdh-1pu-04 Appendix A (Direct Key Agreement, P-256, A256GCM), from both the sender's and the recipient's
    /// keys: Ze, Zs and the derived key are as given there.
    /// </summary>
    [Fact(DisplayName = "ECDH-1PU must reproduce draft-madden-jose-ecdh-1pu-04 Appendix A with P-256, from sender and recipient")]
    public void Ecdh1PuMustReproduceAppendixA()
    {
        // Given
        EcdhPrivateKey aliceStatic = ImportP256(
            "WKn-ZIGevcwGIyyrzFoZNBdaq9_TsqzGl96oc0CWuis",
            "y77t-RvAHRKTsSGdIYUfweuOvwrvDD-Q3Hv5J0fSKbE",
            "Hndv7ZZjs_ke8o9zXYo3iq-Yr8SewI5vrqd0pAvEPqg");
        EcdhPrivateKey bobStatic = ImportP256(
            "weNJy2HscCSM6AEDTDg04biOvhFhyyWvOHQfeF_PxMQ",
            "e8lnCO-AlStT-NJVX-crhB7QRYhiix03illJOVAOyck",
            "VEmDZpDXXK8p8N0Cndsxs924q6nS1RXFASRl6BfUqdw");
        EcdhPrivateKey aliceEphemeral = ImportP256(
            "gI0GAILBdu7T53akrFmMyGcsF3n5dO7MmwNBHKW5SV0",
            "SLW_xSffzlPWrHEVI30DHM_4egVwt3NQqeUD7nMFpps",
            "0_NxaRPUMQoAJt50Gz8YiTr8gRTwyEaCumd-MToTmIo");
        byte[] otherInfo = Hex(AppendixAFixedInfo);
        byte[] senderKey = new byte[32];
        byte[] recipientKey = new byte[32];

        // When
        byte[] senderZe = aliceEphemeral.DeriveRawSharedSecret(bobStatic.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] senderZs = aliceStatic.DeriveRawSharedSecret(bobStatic.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] recipientZe = bobStatic.DeriveRawSharedSecret(aliceEphemeral.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] recipientZs = bobStatic.DeriveRawSharedSecret(aliceStatic.GetPublicKey()).AsReadOnlySpan().ToArray();
        Ecdh1Pu.DeriveKey(senderZe, senderZs, otherInfo, senderKey);
        Ecdh1Pu.DeriveKey(recipientZe, recipientZs, otherInfo, recipientKey);

        // Then
        Assert.Equal(Hex("9e56d91d817135d372834283bf84269cfb316ea3da806a48f6daa7798cfe90c4"), senderZe);
        Assert.Equal(Hex("e3ca3474384c9f62b30bfd4c688b3e7d4110a1b4badc3cc54ef7b81241efd50d"), senderZs);
        Assert.Equal(senderZe, recipientZe);
        Assert.Equal(senderZs, recipientZs);
        Assert.Equal(Hex("6caf13723d14850ad4b42cd6dde935bffd2fff00a9ba70de05c203a5e1722ca7"), senderKey);
        Assert.Equal(senderKey, recipientKey);
        Assert.Equal(Base64Url.Decode("bK8Tcj0UhQrUtCzW3ek1v_0v_wCpunDeBcIDpeFyLKc"), senderKey);
    }

    /// <summary>
    /// Verifies draft-madden-jose-ecdh-1pu-04 Appendix B (Key Wrapping, X25519, A128KW, A256CBC-HS512, two recipients): the content
    /// encryption of B.8, the key agreement and derived keys of B.9 and B.10, and the encrypted keys, as given there.
    /// </summary>
    /// <remarks>
    /// The associated data is the ASCII of the encoded protected header from B.11, as RFC 7516 §5.1 step 14 defines. B.4 says the same in
    /// words, but lists the bytes of the decoded header JSON instead; the ciphertext and tag of B.8 match the encoded form.
    /// </remarks>
    [Fact(DisplayName = "ECDH-1PU must reproduce draft-madden-jose-ecdh-1pu-04 Appendix B with X25519 and A128KW for both recipients")]
    public void Ecdh1PuMustReproduceAppendixB()
    {
        // Given
        XdhPrivateKey aliceStatic = XdhPrivateKey.Import(Base64Url.Decode("i9KuFhSzEBsiv3PKVL5115OCdsqQai5nj_Flzfkw5jU"));
        XdhPrivateKey bob = XdhPrivateKey.Import(Base64Url.Decode("1gDirl_r_Y3-qUa3WXHgEXrrEHngWThU3c9zj9A2uBg"));
        XdhPrivateKey charlie = XdhPrivateKey.Import(Base64Url.Decode("Jcv8gklhMjC0b-lsk5onBbppWAx5ncNtbM63Jr9xBQE"));
        XdhPrivateKey aliceEphemeral = XdhPrivateKey.Import(Base64Url.Decode("x8EVZH4Fwk673_mUujnliJoSrLz0zYzzCWp5GUX2fc8"));
        const string protectedHeader =
            "eyJhbGciOiJFQ0RILTFQVStBMTI4S1ciLCJlbmMiOiJBMjU2Q0JDLUhTNTEyIiwi" +
            "YXB1IjoiUVd4cFkyVSIsImFwdiI6IlFtOWlJR0Z1WkNCRGFHRnliR2xsIiwiZXBr" +
            "Ijp7Imt0eSI6Ik9LUCIsImNydiI6IlgyNTUxOSIsIngiOiJrOW9mX2NwQWFqeTBwb1c1Z2FpeFhHczluSGt3ZzFBRnFVQUZhMzlkeUJjIn19";
        byte[] cek = Hex(
            "fffefdfcfbfaf9f8f7f6f5f4f3f2f1f0efeeedecebeae9e8e7e6e5e4e3e2e1e0" +
            "dfdedddcdbdad9d8d7d6d5d4d3d2d1d0cfcecdcccbcac9c8c7c6c5c4c3c2c1c0");
        byte[] iv = Hex("000102030405060708090a0b0c0d0e0f");
        byte[] plaintext = Encoding.UTF8.GetBytes("Three is a magic number.");
        byte[] tag = new byte[32];
        byte[] otherInfo = Hex(AppendixBFixedInfo);
        byte[] bobKek = new byte[16];
        byte[] charlieKek = new byte[16];

        // When
        using AesCbcHmacSha2 cipher = new(cek);
        byte[] ciphertext = cipher.Encrypt(iv, plaintext, tag, Encoding.ASCII.GetBytes(protectedHeader));

        byte[] bobZe = aliceEphemeral.DeriveRawSharedSecret(bob.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] bobZs = aliceStatic.DeriveRawSharedSecret(bob.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] charlieZe = aliceEphemeral.DeriveRawSharedSecret(charlie.GetPublicKey()).AsReadOnlySpan().ToArray();
        byte[] charlieZs = aliceStatic.DeriveRawSharedSecret(charlie.GetPublicKey()).AsReadOnlySpan().ToArray();
        Ecdh1Pu.DeriveKey(bobZe, bobZs, otherInfo, bobKek);
        Ecdh1Pu.DeriveKey(charlieZe, charlieZs, otherInfo, charlieKek);

        // Then
        Assert.Equal(Base64Url.Decode("Az2IWsISEMDJvyc5XRL-3-d-RgNBOGolCsxFFoUXFYw"), ciphertext);
        Assert.Equal(Base64Url.Decode("HLb4fTlm8spGmij3RyOs2gJ4DpHM4hhVRwdF_hGb3WQ"), tag);
        Assert.Equal(Base64Url.Decode("k9of_cpAajy0poW5gaixXGs9nHkwg1AFqUAFa39dyBc"), aliceEphemeral.GetPublicKey().Export());
        Assert.Equal(Hex("32810896e0fe4d570ed1acfcedf67117dc194ed5daac21d8ff7af3244694897f"), bobZe);
        Assert.Equal(Hex("2157612c9048edfae77cb2e4237140605967c05c7f77a48eeaf2cf29a5737c4a"), bobZs);
        Assert.Equal(Hex("89dcfe4c37c1dc0271f346b5b3b19c3b705ca2a72f9a237785c34406fcb75f10"), charlieZe);
        Assert.Equal(Hex("78fe63fc661cf8d18f92a8422a6418e4ed5e20a9168185fdeedca1c3d8e6a61c"), charlieZs);
        Assert.Equal(Hex("df4c37a0668306a11e3d6b0074b5d8df"), bobKek);
        Assert.Equal(Hex("57d8126f1b7ec4ccb0584dac03cb27cc"), charlieKek);
        Assert.Equal(Base64Url.Decode(
            "pOMVA9_PtoRe7xXW1139NzzN1UhiFoio8lGto9cf0t8PyU-sjNXH8-LIRLycq8CH" +
            "JQbDwvQeU1cSl55cQ0hGezJu2N9IY0QN"), AesKeyWrap.Wrap(bobKek, cek));
        Assert.Equal(Base64Url.Decode(
            "56GVudgRLIMEElQ7DpXsijJVRSWUSDNdbWkdV3g0GUNq6hcT_GkxwnxlPIWrTXCq" +
            "RpVKQC8fe4z3PQ2YH2afvjQ28aiCTWFE"), AesKeyWrap.Wrap(charlieKek, cek));
        Assert.Equal(bobZe, bob.DeriveRawSharedSecret(aliceEphemeral.GetPublicKey()).AsReadOnlySpan().ToArray());
        Assert.Equal(bobZs, bob.DeriveRawSharedSecret(aliceStatic.GetPublicKey()).AsReadOnlySpan().ToArray());
    }

    [Fact(DisplayName = "Ecdh1Pu.DeriveKey should throw ArgumentException when the shared secrets differ in length or are empty")]
    public void Ecdh1PuDeriveKeyShouldThrowForMismatchedSecrets()
    {
        // When / Then
        Assert.Throws<ArgumentException>(() => Ecdh1Pu.DeriveKey(new byte[32], new byte[48], [], new byte[16]));
        Assert.Throws<ArgumentException>(() => Ecdh1Pu.DeriveKey([], [], [], new byte[16]));
    }

    /// <summary>
    /// Decodes hexadecimal copied from the draft, ignoring the spaces that separate its byte groups.
    /// </summary>
    /// <param name="value">The hexadecimal text.</param>
    /// <returns>Returns the decoded bytes.</returns>
    private static byte[] Hex(string value) => Convert.FromHexString(value.Replace(" ", string.Empty));

    /// <summary>
    /// Imports a P-256 private key from the base64url coordinates and scalar of a JWK.
    /// </summary>
    /// <param name="x">The base64url x-coordinate.</param>
    /// <param name="y">The base64url y-coordinate.</param>
    /// <param name="d">The base64url private scalar.</param>
    /// <returns>Returns a new <see cref="EcdhPrivateKey"/>.</returns>
    private static EcdhPrivateKey ImportP256(string x, string y, string d)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create(new ECParameters
        {
            Curve = ECCurve.NamedCurves.nistP256,
            Q = new ECPoint { X = Base64Url.Decode(x), Y = Base64Url.Decode(y) },
            D = Base64Url.Decode(d)
        });

        return EcdhPrivateKey.Import(algorithm.ExportECPrivateKey());
    }
}
