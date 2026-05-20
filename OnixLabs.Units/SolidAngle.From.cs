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

public readonly partial struct SolidAngle<T>
{
    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Quectosteradians value.</summary>
    public static SolidAngle<T> FromQuectosteradians(T value) => new(value);

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Rontosteradians value.</summary>
    public static SolidAngle<T> FromRontosteradians(T value) => new(value.FromRontoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Yoctosteradians value.</summary>
    public static SolidAngle<T> FromYoctosteradians(T value) => new(value.FromYoctoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Zeptosteradians value.</summary>
    public static SolidAngle<T> FromZeptosteradians(T value) => new(value.FromZeptoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from an Attosteradians value.</summary>
    public static SolidAngle<T> FromAttosteradians(T value) => new(value.FromAttoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Femtosteradians value.</summary>
    public static SolidAngle<T> FromFemtosteradians(T value) => new(value.FromFemtoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Picosteradians value.</summary>
    public static SolidAngle<T> FromPicosteradians(T value) => new(value.FromPicoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Nanosteradians value.</summary>
    public static SolidAngle<T> FromNanosteradians(T value) => new(value.FromNanoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Microsteradians value.</summary>
    public static SolidAngle<T> FromMicrosteradians(T value) => new(value.FromMicroUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Millisteradians value.</summary>
    public static SolidAngle<T> FromMillisteradians(T value) => new(value.FromMilliUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Centisteradians value.</summary>
    public static SolidAngle<T> FromCentisteradians(T value) => new(value.FromCentiUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Decisteradians value.</summary>
    public static SolidAngle<T> FromDecisteradians(T value) => new(value.FromDeciUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Steradians value.</summary>
    public static SolidAngle<T> FromSteradians(T value) => new(value.FromBaseUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Decasteradians value.</summary>
    public static SolidAngle<T> FromDecasteradians(T value) => new(value.FromDecaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Hectosteradians value.</summary>
    public static SolidAngle<T> FromHectosteradians(T value) => new(value.FromHectoUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Kilosteradians value.</summary>
    public static SolidAngle<T> FromKilosteradians(T value) => new(value.FromKiloUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Megasteradians value.</summary>
    public static SolidAngle<T> FromMegasteradians(T value) => new(value.FromMegaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Gigasteradians value.</summary>
    public static SolidAngle<T> FromGigasteradians(T value) => new(value.FromGigaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Terasteradians value.</summary>
    public static SolidAngle<T> FromTerasteradians(T value) => new(value.FromTeraUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Petasteradians value.</summary>
    public static SolidAngle<T> FromPetasteradians(T value) => new(value.FromPetaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from an Exasteradians value.</summary>
    public static SolidAngle<T> FromExasteradians(T value) => new(value.FromExaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Zettasteradians value.</summary>
    public static SolidAngle<T> FromZettasteradians(T value) => new(value.FromZettaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Yottasteradians value.</summary>
    public static SolidAngle<T> FromYottasteradians(T value) => new(value.FromYottaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Ronnasteradians value.</summary>
    public static SolidAngle<T> FromRonnasteradians(T value) => new(value.FromRonnaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Quettasteradians value.</summary>
    public static SolidAngle<T> FromQuettasteradians(T value) => new(value.FromQuettaUnits());

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Square Degrees value (1 deg² = (π/180)² sr).</summary>
    public static SolidAngle<T> FromSquareDegrees(T value) => new(value * QuectoSteradiansPerSquareDegree);

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Square Arcminutes value (1 arcmin² = (π/10800)² sr).</summary>
    public static SolidAngle<T> FromSquareArcminutes(T value) => new(value * QuectoSteradiansPerSquareArcminute);

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Square Arcseconds value (1 arcsec² = (π/648000)² sr).</summary>
    public static SolidAngle<T> FromSquareArcseconds(T value) => new(value * QuectoSteradiansPerSquareArcsecond);

    /// <summary>Creates a new <see cref="SolidAngle{T}"/> from a Spats value (1 spat = full sphere = 4π sr).</summary>
    public static SolidAngle<T> FromSpats(T value) => new(value * QuectoSteradiansPerSpat);
}
