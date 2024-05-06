![ONIX Labs](https://raw.githubusercontent.com/onix-labs/onixlabs-website/main/src/assets/images/logo/full/original/original-md.png)

# ONIXLabs .NET Library

Welcome to the ONIXLabs .NET Library, a comprehensive and meticulously crafted suite of APIs engineered to empower developers and enrich the developer experience. With the ONIXLabs .NET Library, developers gain access to a wealth of tools and resources aimed at streamlining development workflows, enhancing code quality, and fortifying application security. Whether you're a seasoned .NET developer or embarking on your coding journey, ONIXLabs is your trusted companion for building robust and secure software solutions.

## ONIXLabs Core

At the heart of the ONIXLabs .NET Library lies the Core module, serving as the bedrock for building resilient and scalable applications. Here, developers will find essential APIs and interfaces meticulously designed to promote code reusability, maintainability, and readability. From foundational interfaces to extension methods enriching object manipulation, ONIXLabs Core sets the stage for seamless development experiences.

- **Core APIs**: Includes interfaces such as `IValueEquatable<T>`, `IValueComparable<T>`, and `IBinaryConvertible`, providing a foundation for implementing common patterns and functionalities.
- **Extension Methods**: Extends the capabilities of objects, arrays, strings, and enumerables, making data manipulation more intuitive and efficient.
- **Strong Enumeration Pattern**: Offers an implementation of the strong enumeration pattern, for strongly typed enumerations which can be extended with value and associated behaviour.
- **Text Services**: Provides codecs for various encoding schemes like Base16, Base32, Base58, and Base64, along with primitive structs representing these bases for semantic clarity.

## ONIXLabs Numerics

Numerical computations are a fundamental aspect of many software applications, often requiring precision, flexibility, and performance. The ONIXLabs Numerics module equips developers with a suite of tools tailored to meet these demands. Whether it's obtaining detailed insights into numeric values or performing complex arithmetic operations, this module empowers developers to tackle numerical challenges with ease and confidence.

- **NumberInfo**: A versatile struct that provides detailed insights into any numeric value, including unscaled value, scale, significand, exponent, precision, and sign.
- **BigDecimal**: An arbitrary-length decimal number representation leveraging .NET's generic math interfaces. It supports a comprehensive range of arithmetic operations, parsing, conversion, and culture-specific formatting, facilitating precise numerical calculations.
- **Generic Math Extension Methods**: Extends the functionality of numerical types with useful generic math extension methods, enhancing flexibility and productivity in numerical computing tasks.

## ONIXLabs Cryptography

In an era marked by increasing cybersecurity threats, robust cryptographic implementations are paramount. The ONIXLabs Cryptography module offers developers a comprehensive set of APIs for handling cryptographic operations securely. From digital signatures and hash functions to Merkle trees and FIPS-202 compliant SHA-3 implementations, developers can leverage ONIXLabs Cryptography to safeguard sensitive data and ensure the integrity and confidentiality of their applications.

- **DigitalSignature**: Wraps digital signatures, providing a convenient abstraction for cryptographic signature operations.
- **Hash**: Wraps hash functions, facilitating secure hashing of data with ease.
- **MerkleTree**: Represents Merkle trees, enabling efficient verification of data integrity in distributed systems.
- **Salt**: Represents cryptographically secure random numbers, enhancing security in cryptographic operations.
- **Public and Private Key Abstractions**: Offers abstractions for public and private keys, including implementations for RSA and ECDSA, along with functions for signing and verification.
- **SHA-3 Implementation**: A complete, platform-independent, FIPS-202 implementation of SHA-3, including variants like SHA-3 224, 256, 384, 512, SHAKE128, and SHAKE256, ensuring compliance with cryptographic standards.

