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
  - Inherits `IPrivateKeyDerivable<EddsaPublicKey>`, `IBinaryConvertible`, plus the granular import/export interfaces per §3a: `IPrivateKeyRawImportable<EddsaPrivateKey>` + `IPrivateKeyPkcs8Importable<EddsaPrivateKey>` + `IPrivateKeyPemImportable<EddsaPrivateKey>` + `IPrivateKeyRawExportable` + `IPrivateKeyPkcs8Exportable` + `IPrivateKeyPkcs8PemExportable`. Deliberately omits `IPrivateKeyRawPemExportable` — no `ExportPem()` on EDDSA.

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
| `EddsaPrivateKey.Export.cs` | `Export`, `ExportPkcs8` (× 3), `ExportPkcs8Pem` (× 3) — stubs. No `ExportPem` (Ed25519 has no raw-form PEM). |
| `EddsaPrivateKey.Get.cs` | `GetPublicKey()` — stub |
| `EddsaPrivateKey.Import.cs` | Four `Import` overloads — stubs |
| `EddsaPrivateKey.ImportPem.cs` | Three `ImportPem` overloads — stubs |
| `EddsaPrivateKey.ImportPkcs8.cs` | Twelve `ImportPkcs8` overloads — stubs |
| `EddsaPrivateKey.Parse.cs` | `Parse` / `TryParse` (Base16) — wired, works |
| `EddsaPrivateKey.Sign.cs` | Four `SignData` overloads — stubs |
| `EddsaPrivateKey.To.cs` | `ToNamedPrivateKey()`, `KeyName = "EDDSA"` |

## Design choices

### 1. Narrower API surface than `EcdsaPublicKey` / `EcdsaPrivateKey` — **decided: narrow**

`IEcdsaPrivateKey.SignData` and `IEcdsaPublicKey.IsDataValid` take `HashAlgorithm` / `HashAlgorithmName` and `DSASignatureFormat`. For Ed25519 / PureEdDSA per RFC 8032 these are meaningless:

- The hash is fixed: SHA-512, internally, as part of the algorithm.
- The signature format is fixed: 64 raw bytes (`R || S`) with `S < L`.
- `SignHash` / `IsHashValid` / `VerifyHash` are dropped: PureEdDSA signs messages, not pre-hashes. (Ed25519ph exists in RFC 8032 §5.1 but is a separate algorithm and not in scope.)

`IEddsaPublicKey` / `IEddsaPrivateKey` therefore omit these parameters entirely rather than accepting and ignoring them.

### 2. `KeyName = "EDDSA"` — **decided**

Used by `ToNamedPublicKey` / `ToNamedPrivateKey`. Chosen for symmetry with `ECDSA` / `RSA`. (Algorithm-specific `"Ed25519"` was the alternative.)

### 3. `ExportPem` vs `ExportPkcs8Pem` on the private key — **resolved by interface split (§3a)**

For ECDSA they differ: `ExportPem` emits SEC1 `EC PRIVATE KEY`, `ExportPkcs8Pem` emits PKCS#8 `PRIVATE KEY`.

For Ed25519, RFC 8410 defines only PKCS#8 PEM; there is no separate raw-form PEM label. Rather than have both methods emit identical output, `EddsaPrivateKey` simply does not declare `ExportPem` at all — see §3a below.

### 3a. Interface segregation for Export / Import — **decided: 4-way max granularity**

The four existing interfaces (`IPrivateKeyExportable`, `IPrivateKeyImportable<T>`, `IPublicKeyExportable`, `IPublicKeyImportable<T>`) are split into granular pieces along the orthogonal axes of **format** (Raw vs PKCS#8) × **encoding** (binary vs PEM). The originals become composites that inherit every granular piece, so `EcdsaPrivateKey` / `RsaPrivateKey` / `EcdhPrivateKey` need no source changes.

**Private key export (4 granular):**

| Interface | Methods |
|---|---|
| `IPrivateKeyRawExportable` | `byte[] Export()` |
| `IPrivateKeyRawPemExportable` | `string ExportPem()` |
| `IPrivateKeyPkcs8Exportable` | `ExportPkcs8()` (unencrypted) + 2 encrypted overloads |
| `IPrivateKeyPkcs8PemExportable` | `ExportPkcs8Pem()` (unencrypted) + 2 encrypted overloads |

**Private key import (3 granular):**

| Interface | Methods |
|---|---|
| `IPrivateKeyRawImportable<T>` | `Import(...)` family (span / `IBinaryConvertible`, with/without `bytesRead`) |
| `IPrivateKeyPkcs8Importable<T>` | `ImportPkcs8(...)` family (all 12 overloads) |
| `IPrivateKeyPemImportable<T>` | `ImportPem(...)` family (3 overloads, universal-PEM dispatcher) |

**Public key export (2 granular):** `IPublicKeyRawExportable` (`Export()`) + `IPublicKeyPemExportable` (`ExportPem()`).

**Public key import (2 granular):** `IPublicKeyRawImportable<T>` (`Import(...)` family) + `IPublicKeyPemImportable<T>` (`ImportPem(...)` family).

**Composites recompose the originals:**

```csharp
public interface IPrivateKeyExportable
    : IPrivateKeyRawExportable,
      IPrivateKeyRawPemExportable,
      IPrivateKeyPkcs8Exportable,
      IPrivateKeyPkcs8PemExportable;

public interface IPrivateKeyImportable<out T>
    : IPrivateKeyRawImportable<T>,
      IPrivateKeyPkcs8Importable<T>,
      IPrivateKeyPemImportable<T>
    where T : PrivateKey;

public interface IPublicKeyExportable
    : IPublicKeyRawExportable,
      IPublicKeyPemExportable;

public interface IPublicKeyImportable<out T>
    : IPublicKeyRawImportable<T>,
      IPublicKeyPemImportable<T>
    where T : PublicKey;
```

**Asymmetry (4 export / 3 import) is intentional.** The BCL provides separate exporters per format (`ExportECPrivateKeyPem` vs `ExportPkcs8PrivateKeyPem`) but a single dispatching importer (`AsymmetricAlgorithm.ImportFromPem` parses any supported PEM label). Splitting `ImportPem` into `ImportPkcs8Pem` + a hypothetical raw-PEM importer was considered and rejected — it would either lock in a misleading name (the method accepts SEC1 / PKCS#1 PEM too) or silently restrict behavior at runtime. The asymmetry honestly tracks the BCL surface.

**Bug fix bundled with the split:** the existing XML docstrings on `IPrivateKeyImportable<T>.ImportPem` claim "PKCS #8 RFC 7468 PEM format" — wrong, the method accepts any PEM label the underlying algorithm understands. Fix the docstrings as part of the refactor.

**`IEddsaPrivateKey` picks:**

```csharp
public interface IEddsaPrivateKey :
    IPrivateKeyRawExportable,
    IPrivateKeyPkcs8Exportable,
    IPrivateKeyPkcs8PemExportable,
    IPrivateKeyRawImportable<EddsaPrivateKey>,
    IPrivateKeyPkcs8Importable<EddsaPrivateKey>,
    IPrivateKeyPemImportable<EddsaPrivateKey>,
    IPrivateKeyDerivable<EddsaPublicKey>,
    IBinaryConvertible
{
    // SignData overloads
}
```

Note the deliberate omission of `IPrivateKeyRawPemExportable` — `ExportPem()` does not appear on `EddsaPrivateKey` at all, because Ed25519 has no raw-form PEM label.

**`IEddsaPublicKey` picks** both public granular pieces (`Raw` + `Pem`) for export and import — Ed25519 has a standard SPKI / PEM form per RFC 8410.

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

### Phase 4 — Hardening (issue acceptance criteria) — COMPLETE

1. **Project Wycheproof Ed25519 test vectors — DONE.** Source vectors fetched from
   `https://raw.githubusercontent.com/C2SP/wycheproof/main/testvectors_v1/ed25519_test.json`
   (the upstream `testvectors/` path referenced in the original issue has been renamed to
   `testvectors_v1/`). The JSON is embedded as an `EmbeddedResource` in
   `OnixLabs.Security.Cryptography.UnitTests.Data/wycheproof_eddsa_test.json` and surfaced
   to xUnit via `WycheproofEd25519DataAttribute` (a `DataAttribute` matching the in-repo
   convention used in `OnixLabs.Numerics.UnitTests.Data`). All 150 vectors pass: the
   88 `valid` cases verify, the 62 `invalid` cases are rejected. The corpus does not
   contain any `acceptable` cases.

2. **RFC 8032 §7.1 TEST 1024 — DONE.** Added as a fourth `[InlineData]` row on
   `EddsaMustSatisfyRfc8032KnownAnswerTestVectors` in `EddsaKeyTests.cs`. The 1023-byte
   message exercises the whole-message PureEdDSA path through `IncrementalHash.AppendData`.
   Public-key derivation, signature production, and signature verification all match the
   RFC's published values.

3. **Cross-validation — satisfied by Wycheproof.** The 88 `Valid`/`Ktv`/`TinkOverflow`
   vectors in Project Wycheproof are signatures produced by independent implementations
   (BoringSSL, Tink, NaCl, ...). Verifying all of them constitutes cross-validation against
   multiple oracles without adding a runtime dependency. No additional BouncyCastle oracle
   table was added.

4. **Negative tests — DONE.** Added to `EddsaKeyTests.cs`:
   - `EddsaVerificationShouldRejectSignaturesWithNonCanonicalS` — confirms
     `Ed25519Scalar.IsCanonical` rejects `S = L`.
   - `EddsaVerificationShouldRejectNonCanonicalY` — confirms `Ed25519Point.TryDecode`
     rejects a public key whose y-coordinate equals `p`.
   - `EddsaVerificationShouldFailWhenSIsBitFlipped` and
     `EddsaVerificationShouldFailWhenRIsBitFlipped` — malleability.
   - `EddsaCofactoredVerificationAcceptsLowOrderPublicKey` — documents that our cofactored
     verifier accepts the all-zero (order-4) public key with a constructed signature
     `R = B, S = 1`. RFC 8032 does not mandate rejection here. Switching to non-cofactored
     verification would change this behaviour; the test pins the current contract.

5. **Benchmarks — DONE.** New project `OnixLabs.Security.Cryptography.Benchmarks` using
   `BenchmarkDotNet 0.15.8`. Wired into `onixlabs-dotnet.slnx`. A local
   `Directory.Build.props` pins `TargetFrameworks=net10.0` so the BDN auto-generated host
   project does not try to inherit the repo-wide net8/9/10 multi-targeting. Results in the
   next section.

#### Phase 4 benchmarks

Reference run, x86_64 Linux/macOS hardware (the dev machine reported below — Apple/ARM64
not in use for this run). Run with:

```sh
dotnet build OnixLabs.Security.Cryptography.Benchmarks -c Release
dotnet run --project OnixLabs.Security.Cryptography.Benchmarks -c Release -f net10.0 --no-build -- --filter '*'
```

Raw report: `BenchmarkDotNet.Artifacts/results/OnixLabs.Security.Cryptography.Benchmarks.EddsaBenchmarks-report-github.md`.

```
BenchmarkDotNet v0.15.8, macOS Tahoe 26.3.1 (a) (25D771280a) [Darwin 25.3.0]
Intel Xeon W-3265 CPU 2.70GHz, 1 CPU, 48 logical and 24 physical cores
.NET SDK 10.0.100
  [Host]     : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
```

| Method                                         | Mean       | Error     | StdDev    | Allocated | Ops/sec  |
|----------------------------------------------- |-----------:|----------:|----------:|----------:|---------:|
| `EddsaPrivateKey.Create — key generation`      |   4.284 µs | 0.0700 µs | 0.0654 µs |   1,616 B | ~233,400 |
| `EddsaPrivateKey.SignData — 64-byte message`   | 331.193 µs | 1.5493 µs | 1.3734 µs |   2,376 B |   ~3,020 |
| `EddsaPublicKey.IsDataValid — 64-byte message` | 342.897 µs | 2.6553 µs | 2.3538 µs |     328 B |   ~2,915 |

Order-of-magnitude observations:

- The reference scalar-multiplication is double-and-add over 255 bits with no windowing —
  ~512 group operations per sign or verify on top of the SHA-512 hashes. Reaching the
  ~50 µs region typical of optimised Ed25519 implementations (NaCl, ed25519-donna) is a
  longer-term target — windowed scalar multiplication and a precomputed base-point table
  are the well-known speed-ups, neither of which is in scope for this issue.
- Allocations during sign/verify come from the `BigInteger`-based scalar arithmetic
  (`Ed25519Scalar`); the constant-time refactor in Phase 5 replaces it with `long`-limb
  code that allocates nothing and drops these numbers significantly (see the Phase 5 table).

### Phase 5 — Constant-time scalar arithmetic and scalar multiplication — COMPLETE

Phase 4 left two known side-channel hazards. Both have now been removed:

1. **Scalar arithmetic mod L** (`Ed25519Scalar.cs`) previously used
   `System.Numerics.BigInteger` for `ReduceFromWideBytes` and `MulAdd`. `BigInteger` is
   allocating and not constant-time (its arithmetic dispatches on operand bit-length, so a
   secret scalar whose high limb happened to be small could time differently from one that
   used the full 252 bits). Replaced with a direct port of SUPERCOP `ref10`'s `sc_reduce.c`
   and `sc_muladd.c`: twelve 21-bit signed limbs, the relation L = 2^252 + δ
   (δ = 27742317777372353535851937790883648493) folds the 24-limb intermediate back into
   12 limbs with a fixed sequence of multiply-and-add operations, and a final two-pass
   normalisation guarantees the result is strictly less than L. No branches depend on any
   limb value. `IsCanonical(scalar)` now does the `scalar < L` test as a byte-by-byte
   constant-time subtraction inspecting the final borrow, not an early-return loop.

2. **Scalar multiplication** (`Ed25519Point.ScalarMultiply`) previously used left-to-right
   double-and-add — `if (bit == 1) result = Add(result, point)` leaks every scalar bit through
   both the branch and the differing per-iteration operation count. Replaced with a
   Joye/Montgomery-style ladder: maintain `R0 = [k']P, R1 = R0 + P` and at each step
   `cswap(R0, R1, bit); R1 = R0 + R1; R0 = 2*R0; cswap(R0, R1, bit)`. Every iteration performs
   exactly one Add and one Double regardless of bit value, and the cswap is implemented as
   masked `Ed25519FieldElement.ConditionalSelect` operations over all four projective
   coordinates of both points — same memory writes either way. The HWCD a = -1 formulas were
   already unified (no special cases for `P + Q = identity`), so the ladder is safe even when
   the scalar's leading bits leave R0 at the identity for many iterations.

A skim of `Ed25519FieldElement.cs` for side-channel hazards turned up nothing worth
fixing: `IsZero` accumulates with bitwise OR, `IsNegative` reads a fixed byte position,
`ConditionalSelect` is mask-based, and the carry chains in `Add` / `Sub` / `Mul` / `Square`
are straight-line. `ValueEquals` calls `SequenceEqual`, which is variable-time, but its only
callers (`TryDecode`) operate on public-key bytes, not secret scalars.

#### Phase 5 benchmarks

Same machine as Phase 4 (Intel Xeon W-3265, .NET 10.0.100). Re-run with:

```sh
dotnet build OnixLabs.Security.Cryptography.Benchmarks -c Release
dotnet run --project OnixLabs.Security.Cryptography.Benchmarks -c Release -f net10.0 --no-build -- --filter '*'
```

| Method                                          | Phase 4 (variable-time) | Phase 5 (constant-time) | Δ |
|-------------------------------------------------|------------------------:|------------------------:|--:|
| `EddsaPrivateKey.Create — key generation`       |   4.284 µs / 1616 B    |   4.383 µs / 1616 B    | ~ |
| `EddsaPrivateKey.SignData — 64-byte message`    | 331.193 µs / 2376 B    | 473.077 µs / 1688 B    | +43% / −29% allocations |
| `EddsaPublicKey.IsDataValid — 64-byte message`  | 342.897 µs / 328 B     | 482.312 µs / 128 B     | +41% / −61% allocations |

Slowdown is smaller than the rough "2x" upper bound noted in the roadmap because the
double-and-add baseline performs ~1.5 group operations per bit on average (always Double,
Add only when bit is 1), so the ladder's fixed two-operations-per-bit cost is a 33%
arithmetic overhead, with the rest accounted for by the per-iteration cswap. Allocations
dropped because the `BigInteger` boxing inside `Ed25519Scalar` is gone.

#### Out of scope (deferred)

- **dudect-style timing analysis.** Not done here. The arithmetic and ladder are
  data-oblivious by construction, but actually demonstrating that on this JIT would mean
  bringing in a test harness, statistical machinery, and an exclusive-CPU runner. Worth
  doing eventually — not part of this phase.
- **Provider abstraction.** Reshaping the public API so the from-scratch implementation can
  later be swapped for a verified third-party implementation behind the same interface is
  also deferred. The current internal layout (`Ed25519.cs` plus the four `Ed25519*`
  helpers) is already a clean seam if and when that work is taken on.

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
