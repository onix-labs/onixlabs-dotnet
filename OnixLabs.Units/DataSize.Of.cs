// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Gets the value of the current instance expressed in the scale identified by the specified format specifier.
    /// </summary>
    /// <param name="specifier">The format specifier identifying the scale (e.g. <c>m</c>, <c>km</c>, <c>mi</c>) at which to read the value.</param>
    /// <returns>Returns the value of the current instance expressed in the scale identified by the specified format specifier.</returns>
    public T ValueOf(ReadOnlySpan<char> specifier) => new string(specifier) switch
    {
        BitsSpecifier => Bits,
        BytesSpecifier => Bytes,
        KibiBitsSpecifier => KibiBits,
        KibiBytesSpecifier => KibiBytes,
        KiloBitsSpecifier => KiloBits,
        KiloBytesSpecifier => KiloBytes,
        MebiBitsSpecifier => MebiBits,
        MebiBytesSpecifier => MebiBytes,
        MegaBitsSpecifier => MegaBits,
        MegaBytesSpecifier => MegaBytes,
        GibiBitsSpecifier => GibiBits,
        GibiBytesSpecifier => GibiBytes,
        GigaBitsSpecifier => GigaBits,
        GigaBytesSpecifier => GigaBytes,
        TebiBitsSpecifier => TebiBits,
        TebiBytesSpecifier => TebiBytes,
        TeraBitsSpecifier => TeraBits,
        TeraBytesSpecifier => TeraBytes,
        PebiBitsSpecifier => PebiBits,
        PebiBytesSpecifier => PebiBytes,
        PetaBitsSpecifier => PetaBits,
        PetaBytesSpecifier => PetaBytes,
        ExbiBitsSpecifier => ExbiBits,
        ExbiBytesSpecifier => ExbiBytes,
        ExaBitsSpecifier => ExaBits,
        ExaBytesSpecifier => ExaBytes,
        ZebiBitsSpecifier => ZebiBits,
        ZebiBytesSpecifier => ZebiBytes,
        ZettaBitsSpecifier => ZettaBits,
        ZettaBytesSpecifier => ZettaBytes,
        YobiBitsSpecifier => YobiBits,
        YobiBytesSpecifier => YobiBytes,
        YottaBitsSpecifier => YottaBits,
        YottaBytesSpecifier => YottaBytes,
        _ => throw ArgumentException.InvalidFormat(specifier, ValidSpecifiers)
    };
}
