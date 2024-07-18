![ONIX Labs](https://raw.githubusercontent.com/onix-labs/onixlabs-website/main/src/assets/images/logo/full/original/original-md.png)

# ONIXLabs .NET Library

The world of software development is ever-evolving, driven by the constant need for innovation, efficiency, and maintainability. In this landscape, libraries and frameworks play a crucial role in simplifying complex tasks, promoting code reuse, and fostering community collaboration. The ONIXLabs .NET Library is born out of this very ethos—a desire to create a robust, versatile, and community-friendly library that fills the gaps often encountered in various development projects.

[![.NET](https://github.com/onix-labs/onixlabs-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/onix-labs/onixlabs-dotnet/actions/workflows/dotnet.yml)

## ONIXLabs Core

At the heart of the ONIXLabs .NET Library lies the Core module, serving as the bedrock for building resilient and scalable applications. Here, developers will find essential APIs and interfaces meticulously designed to promote code reusability, maintainability, and readability. From foundational interfaces to extension methods enriching object manipulation, ONIXLabs Core sets the stage for seamless development experiences.

### Interfaces

The ONIXLabs .NET Library includes interfaces such as `IValueEquatable<T>`, `IValueComparable<T>`, and `IBinaryConvertible`, providing a foundation for implementing common patterns and functionalities.

### Extension Methods

The ONIXLabs .NET Library extends the capabilities of objects, arrays, strings, sequences, comparables, and enumerables, making data manipulation more intuitive and efficient.

### Preconditions

The ONIXLabs .NET Library offers a comprehensive set of preconditions designed to facilitate clean, consistent, and reliable guard clauses. This promotes a "fail-fast" programming approach, ensuring that potential issues are identified and addressed early in the execution process.

### Strong Enumeration Pattern

The ONIXLabs .NET Library provides an abstraction of the strong enumeration pattern, allowing consumers to build strict, strongly typed enumerations which can be extended with value and associated behavior.

### Functional Concepts

The ONIXLabs .NET Library includes robust implementations of the `Result`, `Result<T>`, and `Optional<T>` patterns, which are widely used in functional programming languages. These implementations address two prevalent challenges in object-oriented programming: exception handling and null reference management. By integrating these patterns, the library provides more predictable and maintainable code structures.

#### Result Pattern

Enables methods to return a success or failure state, encapsulating both the outcome and the associated data or error message. This approach simplifies error handling and improves code readability.

#### Optional Pattern

Facilitates the handling of potentially null values, thereby reducing the risk of `NullReferenceException` and promoting safer code practices.

#### Asynchronous Programming

The ONIXLabs .NET Library extends these patterns with asynchronous programming support, including extension methods that allow `Result`, `Result<T>`, and `Optional<T>` to be seamlessly integrated into asynchronous workflows. This makes it easier to work with these patterns in modern, async-based applications, ensuring consistent and reliable error and null handling across synchronous and asynchronous contexts.

### LINQ Extension Methods

The ONIXLabs .NET Library enhances the standard LINQ capabilities with additional extension methods for working with `IEnumerable` and `IEnumerable<T>`, making it easier to perform complex queries and manipulations on collections.

### Text Encoding Services

The ONIXLabs .NET Library provides codecs for various encoding schemes like `Base16`, `Base32`, `Base58`, and `Base64`, along with primitive structs representing these bases for semantic clarity.

## ONIXLabs Numerics

Numerical computations are a fundamental aspect of many software applications, often requiring precision, flexibility, and performance. The ONIXLabs .NET Library Numerics module equips developers with a suite of tools tailored to meet these demands. Whether it's obtaining detailed insights into numeric values or performing complex arithmetic operations, this module empowers developers to tackle numerical challenges with ease and confidence.

### Number Information

The ONIXLabs .NET Library includes a versatile struct called `NumberInfo` that provides detailed insights into any numeric value, including unscaled value, scale, significand, exponent, precision, and sign.

### Number Information Formatting

The ONIXLabs .NET Library features a comprehensive number formatting API that allows any number type convertible to `NumberInfo` to be formatted using various number styles. These styles include currency, decimal, exponential (scientific), fixed, general, number, and percentage formats. Additionally, the API supports automatic cultural formatting, ensuring that numbers are appropriately formatted according to different cultural conventions.

### Big Decimal

The ONIXLabs .NET Library provides an arbitrary-length `BigDecimal` number representation leveraging .NET's generic math interfaces. It supports a comprehensive range of arithmetic operations, parsing, conversion, and culture-specific formatting, facilitating precise numerical calculations.

### Generic Math Extension Methods

The ONIXLabs .NET Library extends the functionality of numerical types with useful generic math extension methods, enhancing flexibility and productivity in numerical computing tasks.

## ONIXLabs Security

The Security module in the ONIXLabs .NET Library focuses on non-cryptographic security measures, addressing various aspects of application security beyond encryption. This module provides tools and utilities to enhance the security of applications through mechanisms such as secure data handling, input validation, and access control. By implementing best practices and standards for secure coding, the Security module helps developers protect their applications from common vulnerabilities and threats, ensuring robust and reliable security across different components of the software.

### Security Tokens

The ONIXLabs .NET Library includes a powerful API for generating security tokens. These tokens are customizable sequences of random characters designed to function as temporary passwords or authentication codes. The API offers extensive configuration options, allowing developers to specify the length, character set, and complexity of the generated tokens to meet various security requirements. This flexibility ensures that the tokens can be tailored to different use cases, such as user verification, session management, or one-time password generation, thereby enhancing the security and usability of applications.

## ONIXLabs Cryptography

In an era marked by increasing cybersecurity threats, robust cryptographic implementations are paramount. The ONIXLabs Cryptography module offers developers a comprehensive set of APIs for handling cryptographic operations securely. From digital signatures and hash functions to Merkle trees and FIPS-202 compliant SHA3 implementations, developers can leverage ONIXLabs Cryptography to safeguard sensitive data and ensure the integrity and confidentiality of their applications.

### Public/Private Key Cryptography

The ONIXLabs .NET Library offers comprehensive support for public/private key cryptography, essential for secure communication and data encryption. The library includes abstractions such as `PublicKey` and `PrivateKey`, which encapsulate the raw byte data of cryptographic keys, providing a structured and secure way to handle key material. Additionally, the `NamedPublicKey` and `NamedPrivateKey` classes extend these abstractions by associating the keys with the name of the cryptographic algorithm used, enhancing clarity and ease of use in multi-algorithm contexts.

The library supports various cryptographic algorithms through interfaces and concrete implementations, including ECDSA (Elliptic Curve Digital Signature Algorithm), ECDH (Elliptic Curve Diffie-Hellman), and RSA (Rivest-Shamir-Adleman). These implementations enable secure key generation, digital signing, encryption, and decryption operations. For instance, ECDSA and RSA are used for generating and verifying digital signatures, ensuring data integrity and authenticity, while ECDH facilitates secure key exchange mechanisms.

By providing these abstractions and implementations, the ONIXLabs .NET Library ensures that developers can seamlessly integrate robust cryptographic functionality into their applications, leveraging the strengths of various cryptographic algorithms to meet diverse security requirements. This makes the library an invaluable tool for building secure, reliable, and high-performance applications in the .NET ecosystem.

### Digital Signatures

The ONIXLabs .NET Library provides several APIs for creating, storing, and verifying cryptographic digital signatures. The `DigitalSignature` struct is designed to encapsulate the underlying raw byte data of a cryptographic digital signature, ensuring the integrity and authenticity of digital messages, whilst the `DigitalSignatureAndPublicKey` struct combines a `DigitalSignature` with its corresponding `NamedPublicKey`, which is used to verify the authenticity of the digital signature.

### Hashing

The ONIXLabs .NET Library features a comprehensive set of APIs dedicated to creating and storing cryptographic hash values. At the core of these APIs is the `Hash` struct, which is designed to encapsulate the raw byte data of a cryptographic hash. This struct provides a robust and efficient way to handle hash values, ensuring data integrity and security across various applications.

In addition to basic hashing capabilities, the ONIXLabs .NET Library includes a fully managed implementation of the FIPS-202 standardized SHA3 algorithm. This implementation covers all major variants of SHA3, including SHA3-224, SHA3-256, SHA3-384, and SHA3-512, as well as the extendable-output functions SHAKE128 and SHAKE256. By adhering to the FIPS-202 standards, the library ensures compliance with cryptographic standards.

### Merkle Trees

The ONIXLabs .NET Library includes implementations of the `MerkleTree` and `MerkleTree<T>`, which are essential for constructing and managing Merkle trees. A Merkle tree is a cryptographic data structure that allows efficient and secure verification of the contents of large data sets. The `MerkleTree` class provides a general implementation, while `MerkleTree<T>` offers a generic version, where `T` implements `IHashable`. These implementations support the creation of Merkle trees by recursively hashing pairs of nodes until a single root hash, known as the Merkle root, is obtained. This root hash can then be used to verify the integrity and consistency of the entire data set with minimal computational overhead. The library's implementation ensures compatibility with various hash functions, making it a versatile tool for applications requiring secure data verification, such as blockchain or distributed ledger technologies, file integrity checks, and distributed systems.

### Salts

The ONIXLabs .NET Library includes a `Salt` struct designed to encapsulate cryptographically secure random numbers, which are essential for various security operations. Salts are random data added to passwords or other data before hashing to ensure that identical inputs produce unique hash values. This approach prevents attackers from using precomputed tables, such as rainbow tables, to reverse-engineer hashed data. The `Salt` struct in ONIXLabs provides a simple yet powerful way to generate and manage these cryptographic salts, ensuring they are sufficiently random and secure. By incorporating salts into hashing and encryption workflows, the library significantly enhances security, making it more resistant to attacks and ensuring the robustness of applications against potential vulnerabilities. This struct is particularly useful in scenarios like password storage, token generation, and any application requiring enhanced cryptographic security.

### Secrets

The ONIXLabs .NET Library includes a `Secret` struct, designed to securely handle sensitive data such as passwords, encryption keys, and other confidential information. The `Secret` struct ensures that sensitive data is managed with the highest level of security by leveraging secure memory management techniques to minimize the risk of exposure. It provides methods for securely storing, accessing, and disposing of sensitive information, ensuring that data is encrypted in memory and cleared immediately after use. This approach prevents unauthorized access and reduces the risk of memory-based attacks. The `Secret` struct is particularly valuable in applications that require stringent security measures, such as authentication systems, encryption services, and any scenario where sensitive data must be protected. By incorporating the `Secret` struct, the ONIXLabs .NET Library helps developers build more secure applications, safeguarding critical information against potential breaches and ensuring compliance with security best practices.

### In-Memory Encryption

The ONIXLabs .NET Library ensures the highest level of security for sensitive data by implementing in-memory encryption for `Secret` and `PrivateKey` data. This technique encrypts these critical pieces of information while they are stored in memory, thereby protecting them from memory dump attacks, unauthorized access, and other forms of in-memory exploitation. By encrypting sensitive data in memory, the library reduces the risk of exposure even if an attacker gains access to the system's memory. This approach is particularly effective in defending against certain classes of attacks, such as those that exploit vulnerabilities to read memory content directly. The encrypted memory handling for `Secret` and `PrivateKey` data is a key feature that enhances the overall security posture of applications using the ONIXLabs .NET Library, ensuring that sensitive information remains confidential and secure throughout its lifecycle.
