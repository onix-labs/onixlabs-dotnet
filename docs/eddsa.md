# Ed25519 / EdDSA Implementation Notes

Working notes for GitHub issue [#122](https://github.com/onix-labs/onixlabs-dotnet/issues/122) — adding `EddsaPublicKey` / `EddsaPrivateKey` to `OnixLabs.Security.Cryptography`.

Branch: `feature/style-cop-docs`.

## Direction

Issue offered two paths:

- **A.** Wrap an existing implementation (BouncyCastle / NSec / Geralt). Low risk, few days, adds a dependency.
- **B.** From-scratch RFC 8032 implementation. ~1–2 weeks for a correct reference impl, materially more for constant-time / side-channel-resistant production code. C# is a hostile environment for it (no easy SIMD for 255-bit field arithmetic, `BigInteger` is allocating and non-constant-time, JIT can reintroduce branches on secret data).

**Chosen: B (from scratch).** Issue retains all `(B)` acceptance criteria — RFC 8032 §7.1 vectors, Project Wycheproof vectors, cross-validation against an independent implementation, `Span<byte>`-based APIs, documented benchmarks.

## Current state

TDD red phase complete. Build is clean. 15/15 `EddsaKeyTests` fail with `NotImplementedException`.

```
total: 15
failed: 15
succeeded: 0
```

No production code has been wired up yet — stubs only.

## Files added (22)

### Tests (1)
- `OnixLabs.Security.Cryptography.UnitTests/EddsaKeyTests.cs`
  - Mirrors `EcdsaKeyTests.cs` round-trip suite (Export/Import, PEM, PKCS#8, encrypted PKCS#8, encrypted PKCS#8 PEM)
  - `EddsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed` — equality of keys constructed from same key data
  - `EddsaSignaturesShouldBeSixtyFourBytes` — RFC 8032 fixed-length invariant
  - `EddsaTamperedSignaturesShouldFailVerification` — bit-flip rejection
  - `EddsaPkcs8RoundTripSignAndVerifyShouldSucceed` — encrypted PKCS#8 round-trip + sign/verify
  - `EddsaMustSatisfyRfc8032KnownAnswerTestVectors` — `[Theory]` with RFC 8032 §7.1 TEST 1, TEST 2, TEST 3
    - TEST 1024 deliberately omitted (long message); add when implementing
    - Wycheproof vectors deferred to next phase per issue acceptance criteria

### Interfaces (2)
- `OnixLabs.Security.Cryptography/IEddsaPublicKey.cs`
  - `IsDataValid` / `VerifyData` overloads × {`ReadOnlySpan<byte>`, `DigitalSignature`} × {`ReadOnlySpan<byte>`, `ReadOnlySpan<byte>+offset+count`, `Stream`, `IBinaryConvertible`}
  - No `HashAlgorithm` / `HashAlgorithmName` / `DSASignatureFormat` parameters — see [Design choices](#design-choices)
  - No `IsHashValid` / `VerifyHash` — PureEdDSA signs messages, not hashes
- `OnixLabs.Security.Cryptography/IEddsaPrivateKey.cs`
  - `SignData(ReadOnlySpan<byte> [, int offset, int count])`, `SignData(Stream)`, `SignData(IBinaryConvertible)`
  - Inherits `IPrivateKeyDerivable<EddsaPublicKey>`, `IPrivateKeyImportable<EddsaPrivateKey>`, `IPrivateKeyExportable`, `IBinaryConvertible`

### `EddsaPublicKey` partials (8)
| File | Contents |
|---|---|
| `EddsaPublicKey.cs` | `public sealed partial class EddsaPublicKey : PublicKey, IEddsaPublicKey, ISpanParsable<EddsaPublicKey>` + three public ctors (`ReadOnlySpan` / `ReadOnlyMemory` / `ReadOnlySequence<byte>`) |
| `EddsaPublicKey.Convertible.cs` | Three implicit operators from span types |
| `EddsaPublicKey.Export.cs` | `Export()`, `ExportPem()` — stubs |
| `EddsaPublicKey.Import.cs` | Four `Import` overloads — stubs |
| `EddsaPublicKey.ImportPem.cs` | Three `ImportPem` overloads — stubs |
| `EddsaPublicKey.Parse.cs` | `Parse` / `TryParse` via `ISpanParsable` (Base16) — wired to `IBaseCodec.TryGetBytes` (works) |
| `EddsaPublicKey.To.cs` | `ToNamedPublicKey()`, `KeyName = "EDDSA"` |
| `EddsaPublicKey.Verify.cs` | `IsDataValid` (8 overloads, stubs) + `VerifyData` (8 overloads, delegate to `IsDataValid` via `CheckSignature`) |

### `EddsaPrivateKey` partials (11)
| File | Contents |
|---|---|
| `EddsaPrivateKey.cs` | `public sealed partial class EddsaPrivateKey : PrivateKey, IEddsaPrivateKey, ISpanParsable<EddsaPrivateKey>` + three public ctors |
| `EddsaPrivateKey.Convertible.cs` | Three implicit operators |
| `EddsaPrivateKey.Create.cs` | Single parameterless `Create()` (no curve overloads — Ed25519 is fixed) — stub |
| `EddsaPrivateKey.Export.cs` | `Export`, `ExportPkcs8` (× 3), `ExportPem`, `ExportPkcs8Pem` (× 3) — stubs |
| `EddsaPrivateKey.Get.cs` | `GetPublicKey()` — stub |
| `EddsaPrivateKey.Import.cs` | Four `Import` overloads — stubs |
| `EddsaPrivateKey.ImportPem.cs` | Three `ImportPem` overloads — stubs |
| `EddsaPrivateKey.ImportPkcs8.cs` | Twelve `ImportPkcs8` overloads — stubs |
| `EddsaPrivateKey.Parse.cs` | `Parse` / `TryParse` (Base16) — wired, works |
| `EddsaPrivateKey.Sign.cs` | Four `SignData` overloads — stubs |
| `EddsaPrivateKey.To.cs` | `ToNamedPrivateKey()`, `KeyName = "EDDSA"` |

## Design choices (worth confirming before implementation)

### 1. Narrower API surface than `EcdsaPublicKey` / `EcdsaPrivateKey`

`IEcdsaPrivateKey.SignData` and `IEcdsaPublicKey.IsDataValid` take `HashAlgorithm` / `HashAlgorithmName` and `DSASignatureFormat`. For Ed25519 / PureEdDSA per RFC 8032 these are meaningless:

- The hash is fixed: SHA-512, internally, as part of the algorithm.
- The signature format is fixed: 64 raw bytes (`R || S`) with `S < L`.
- `SignHash` / `IsHashValid` / `VerifyHash` are dropped: PureEdDSA signs messages, not pre-hashes. (Ed25519ph exists in RFC 8032 §5.1 but is a separate algorithm and not in scope.)

**If you'd prefer literal mirror of the Ecdsa surface with ignored parameters,** widen `IEddsaPublicKey` / `IEddsaPrivateKey` accordingly. Cheap to do — the stubs are all `=> throw new NotImplementedException();`.

### 2. `KeyName = "EDDSA"`

Used by `ToNamedPublicKey` / `ToNamedPrivateKey`. Could be `"Ed25519"` for algorithm specificity. `EDDSA` was chosen for symmetry with `ECDSA` / `RSA`.

### 3. `ExportPem` vs `ExportPkcs8Pem` on the private key

For ECDSA they differ: `ExportPem` emits SEC1 `EC PRIVATE KEY`, `ExportPkcs8Pem` emits PKCS#8 `PRIVATE KEY`.

For Ed25519, RFC 8410 defines only PKCS#8 PEM as the standard form; there is no separate raw-form PEM label. The stubs keep both methods but the implementation will likely have them produce identical output. Alternative: drop `ExportPem` (and `ImportPem` reads PKCS#8 PEM). Decide during implementation.

### 4. No `Create(ECCurve)` / `Create(ECParameters)` overloads

Ed25519 has a single fixed curve and fixed algorithm — only `Create()` makes sense. Removing the overloads is a deliberate divergence from Ecdsa.

### 5. Storage format inside the encrypted `KeyData` field

Not yet decided — the stubs don't assign `KeyData`. Options:

- **Raw 32-byte seed for private, raw 32-byte compressed point for public.** Smallest. Equality via `FixedTimeEquals` over 32 bytes. Means `Export()` returns 32 bytes.
- **PKCS#8 / SPKI DER for both.** Matches Ecdsa internal pattern. Larger. Means `Export()` returns DER blobs.

Probably go with **raw bytes internally**, with the various `Export*` / `Import*` methods doing PKCS#8 / SPKI / PEM transformations on the fly. Equality comparisons stay simple.

### 6. `Stream` overloads will buffer

PureEdDSA needs the whole message in memory (computes `R = H(prefix || M)` then re-hashes for `S`). The `SignData(Stream)` / `IsDataValid(..., Stream)` overloads will read the stream to an array. Worth a `<remarks>` block in the XML docs when implementing.

## Roadmap from here

Rough ordering. Probably ~3–5 commits for the core, more for hardening.

### Phase 1 — RFC 8032 PureEdDSA core (gets sign/verify tests green)
1. `Edwards25519` internals (private): field arithmetic over GF(2²⁵⁵−19) with `ulong` limbs; Edwards extended coordinates; scalar multiplication; point encoding/decoding per RFC 8032 §5.1.2–§5.1.3.
2. `Ed25519` sign/verify per RFC 8032 §5.1.6 / §5.1.7. Uses `SHA512.HashData` from the BCL.
3. Public-key derivation from seed.
4. Wire up `EddsaPrivateKey.Create()` (32 random bytes), `SignData`, `GetPublicKey`. Wire up `EddsaPublicKey.IsDataValid`.
5. **Gate: RFC 8032 §7.1 TEST 1, 2, 3 pass. Tamper test passes. 64-byte invariant passes.**

### Phase 2 — Raw Export/Import
1. `Export()` / `Import(span)` for both keys — round-trip raw 32 bytes.
2. Storage decision (see [§5 above](#5-storage-format-inside-the-encrypted-keydata-field)).
3. **Gate: `EddsaPrivateKeyShouldBeExportableAndImportable` and `EddsaPublicKeyShouldBeExportableAndImportable` pass.**

### Phase 3 — PKCS#8 + PEM
1. ASN.1 encode/decode per RFC 8410 (`id-Ed25519` OID `1.3.101.112`). Use `System.Formats.Asn1.AsnReader` / `AsnWriter` from the BCL — no external deps needed.
2. PEM wrap/unwrap via `System.Security.Cryptography.PemEncoding`.
3. Encrypted PKCS#8 via `Pkcs12Kdf` / `Aes` / `Rfc2898DeriveBytes` (PBES2). The BCL does NOT have a one-shot encrypted-PKCS#8 helper that's algorithm-agnostic, but the building blocks are there.
4. **Gate: all `ExportPkcs8` / `ImportPkcs8` / `ExportPem` / `ImportPem` round-trip tests pass.**

### Phase 4 — Hardening (issue acceptance criteria)
1. Project Wycheproof Ed25519 test vectors. Source: <https://github.com/C2SP/wycheproof/blob/main/testvectors/eddsa_test.json>. Add as test data alongside `OnixLabs.Security.Cryptography.UnitTests.Data` or pull in at test time.
2. RFC 8032 §7.1 TEST 1024 (long message).
3. Cross-validation: sign with `EddsaPrivateKey`, verify against a known-good independent impl (e.g. embedded test vectors from BouncyCastle as oracle data, without taking a runtime dependency).
4. Negative tests: `S ≥ L` rejection, low-order point rejection, malleability, non-canonical encodings.
5. Benchmarks (BenchmarkDotNet) for sign/verify throughput. Document numbers.

### Phase 5 (stretch, per issue)
1. Constant-time scalar multiplication review.
2. dudect-style timing analysis.
3. Public abstraction reshape so the from-scratch impl can later be swapped for a verified third-party impl behind the same interface.

## Things to be careful about during implementation

- **`BigInteger` is allocating and not constant-time.** Build the field arithmetic on top of `ulong` (or `Span<ulong>`), not `BigInteger`. Even for the reference impl phase — switching later is invasive.
- **JIT branch elimination on secret-dependent comparisons.** Avoid `if (a == b)` on secret data. Use `CryptographicOperations.FixedTimeEquals` for byte arrays; for limb comparisons, build conditional swaps from bitwise ops.
- **Zero key material.** Wipe scratch buffers with `CryptographicOperations.ZeroMemory` (or `Array.Clear`) before they leave scope. The existing `PrivateKey` base encrypts `KeyData` via `ProtectedData` — but transient working buffers during sign/verify aren't covered. See commits `a9603c7`, `1327c78`, `0290070` for the established pattern.
- **`S < L` check on verify.** RFC 8032 §5.1.7 step 1. Missing this opens malleability. Wycheproof tests will catch it; do it from the start.
- **Point decoding must reject non-canonical y-coordinates.** Specifically `y ≥ p` per RFC 8032 §5.1.3. Wycheproof will catch.
- **Low-order point check on the public key.** RFC 8032 doesn't strictly require it but Wycheproof's strict mode does. Decide policy and document.

## Repo conventions to follow (already reflected in the stubs)

- Apache-2.0 header at the top of every file. Copy from any existing `Ecdsa*.cs`.
- `using System; using OnixLabs.Core; using OnixLabs.Core.Text;` order — System first, then OnixLabs.
- Public ctors take `ReadOnlySpan<byte>`, `ReadOnlyMemory<byte>`, `ReadOnlySequence<byte>` and delegate to base.
- Implicit operators from the three span types live in `*.Convertible.cs`.
- Parse/TryParse via `ISpanParsable<T>` decode Base16 hex.
- `ToString()` is provided by base via `Base16FormatProvider`.
- StyleCop docs: `<summary>`, `<param>`, `<returns>`, `<inheritdoc/>` where appropriate. Match the established phrasing from `Ecdsa*` files.
- File partitioning: one concern per partial-class file. Names map directly: `.Create`, `.Export`, `.Get`, `.Import`, `.ImportPem`, `.ImportPkcs8`, `.Parse`, `.Sign`, `.To`, `.Verify`, `.Convertible`.

## Sanity checklist for the next session

Before writing implementation code, on the new machine:

```sh
git status                          # confirm clean tree on feature/style-cop-docs
dotnet build -c Debug               # confirm green
cd OnixLabs.Security.Cryptography.UnitTests
dotnet run -f net10.0 -c Debug --no-build -- --filter-class 'OnixLabs.Security.Cryptography.UnitTests.EddsaKeyTests'
# expect: 15/15 failed, all NotImplementedException
```

Then start Phase 1.
