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

namespace OnixLabs.Units;

public readonly partial struct DataSize<T>
{
    /// <inheritdoc/>
    public static DataSize<T> Zero => new(T.Zero);

    private const string BitsSpecifier = "b";
    private const string BytesSpecifier = "B";
    private const string KibiBitsSpecifier = "Kib";
    private const string KibiBytesSpecifier = "KiB";
    private const string KiloBitsSpecifier = "Kb";
    private const string KiloBytesSpecifier = "KB";
    private const string MebiBitsSpecifier = "Mib";
    private const string MebiBytesSpecifier = "MiB";
    private const string MegaBitsSpecifier = "Mb";
    private const string MegaBytesSpecifier = "MB";
    private const string GibiBitsSpecifier = "Gib";
    private const string GibiBytesSpecifier = "GiB";
    private const string GigaBitsSpecifier = "Gb";
    private const string GigaBytesSpecifier = "GB";
    private const string TebiBitsSpecifier = "Tib";
    private const string TebiBytesSpecifier = "TiB";
    private const string TeraBitsSpecifier = "Tb";
    private const string TeraBytesSpecifier = "TB";
    private const string PebiBitsSpecifier = "Pib";
    private const string PebiBytesSpecifier = "PiB";
    private const string PetaBitsSpecifier = "Pb";
    private const string PetaBytesSpecifier = "PB";
    private const string ExbiBitsSpecifier = "Eib";
    private const string ExbiBytesSpecifier = "EiB";
    private const string ExaBitsSpecifier = "Eb";
    private const string ExaBytesSpecifier = "EB";
    private const string ZebiBitsSpecifier = "Zib";
    private const string ZebiBytesSpecifier = "ZiB";
    private const string ZettaBitsSpecifier = "Zb";
    private const string ZettaBytesSpecifier = "ZB";
    private const string YobiBitsSpecifier = "Yib";
    private const string YobiBytesSpecifier = "YiB";
    private const string YottaBitsSpecifier = "Yb";
    private const string YottaBytesSpecifier = "YB";
}
