![ONIX Labs](https://raw.githubusercontent.com/onix-labs/onixlabs-website/refs/heads/main/OnixLabs.Web/wwwroot/onixlabs/images/logo/logo-full-light.svg)

# ONIXLabs .NET Library

[![NET](https://github.com/onix-labs/onixlabs-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/onix-labs/onixlabs-dotnet/actions/workflows/dotnet.yml)

The ONIXLabs .NET Library is a suite of general-purpose, production-ready libraries designed to support modern application development with a strong emphasis on correctness, composability, and value semantics. They are designed to integrate cleanly into both small utilities and large-scale systems, without imposing architectural constraints or framework lock-in.

At a high level, ONIXLabs provides:

-   Foundational primitives and patterns missing or underrepresented in the BCL
-   Functional and value-oriented abstractions for safer code
-   High-precision numeric and generic math utilities
-   Security and cryptography APIs with strong typing and value semantics
-   Infrastructure helpers that reduce boilerplate while preserving clarity

Current .NET Support includes .NET 8.0, 9.0 and 10.0, and all packages are published to [Nuget](https://www.nuget.org/packages?q=onixlabs).

## Package Overview

-   **OnixLabs.Core** – Foundational primitives, patterns, and extensions
-   **OnixLabs.DependencyInjection** – Configuration-driven DI registration
-   **OnixLabs.Numerics** – Arbitrary-precision numbers and generic math
-   **OnixLabs.Security** – Non-cryptographic security utilities
-   **OnixLabs.Security.Cryptography** – Cryptographic primitives and structures

## OnixLabs.Core

[See it on Nuget](https://www.nuget.org/packages/OnixLabs.Core)

Foundational primitives, patterns, and extensions that address common gaps in the .NET Base Class Library. This package provides low-level building blocks used throughout the ONIXLabs ecosystem, but is also useful as a standalone utility library in any .NET application.

-   Strongly typed enumeration pattern (`Enumeration<T>`)
-   Functional patterns (`Optional<T>`, `Result`, `Result<T>`)
-   Preconditions and guard clause utilities
-   Specification pattern with LINQ-compatible expressions (`Specification<T>`)
-   Value semantics interfaces (`IValueEquatable<T>`, `IValueComparable<T>`)
-   Binary conversion abstractions (`IBinaryConvertible`, span- and memory-based variants)
-   Collection generators with LINQ-style query support
-   Extension methods for arrays, objects, strings, comparables, random, and more
-   Extensions for `IEnumerable`, `IEnumerable<T>`, and `IQueryable<T>`
-   Reflection helpers for `Type`
-   Strongly typed `Base16`, `Base32`, `Base58`, and `Base64` text types
-   Extensions for `StringBuilder` and `Encoding`

## OnixLabs.DependencyInjection

[See it on Nuget](https://www.nuget.org/packages/OnixLabs.DependencyInjection)

Lightweight extensions for Microsoft’s dependency injection abstractions, focused on reducing boilerplate and improving configurability of service registration.

-   `IServiceCollection` extensions
-   Configuration-driven service lifetime selection
-   Cleaner, more consistent registration APIs

## OnixLabs.Numerics

[See it on Nuget](https://www.nuget.org/packages/OnixLabs.Numerics)

High-precision numeric types and generic math utilities for scenarios where built-in numeric types are insufficient. This package emphasizes correctness, precision, and introspection of numeric values.

-   Arbitrary-precision `BigDecimal` based on generic math (`IFloatingPoint<T>`)
-   `NumberInfo` for dissecting rational numbers into constituent parts
-   Generic numeric abstractions (`IBaseNumber<T>`, `INumber<T>`)
-   Extension methods for `Decimal`, `BigDecimal`, `BigInteger`, and other numeric types

## OnixLabs.Security

[See it on Nuget](https://www.nuget.org/packages/OnixLabs.Security)

Non-cryptographic security utilities focused on safe generation and handling of security-related values.

-   `SecurityToken` generation
-   Support for pseudo-random and cryptographically secure RNGs
-   Configurable token alphabets (upper, lower, numeric, special characters)

## OnixLabs.Security.Cryptography

[See it on Nuget](https://www.nuget.org/packages/OnixLabs.Security.Cryptography)

Strongly typed cryptographic primitives and structures with value semantics, designed to make cryptographic operations safer and more explicit.

-   Public and private key abstractions (`PublicKey`, `PrivateKey`)
-   ECDH, ECDSA, and RSA key implementations
-   Digital signature types (`DigitalSignature`, `DigitalSignatureAndPublicKey`)
-   Cryptographic hash value types (`Hash`, `NamedHash`)
-   Extensions for `HashAlgorithm`
-   Fully managed FIPS-202 SHA-3 implementation (SHA3-224/256/384/512, SHAKE128/256)
-   Merkle tree implementations (`MerkleTree`, `MerkleTree<T>`)
-   Cryptographic salt value type (`Salt`)
-   Encrypted-in-memory secret handling (`Secret`)
-   Common cryptographic primitive abstraction (`ICryptoPrimitive`)
