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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of electric charge.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Charge<T> : IUnit<Charge<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Charge{T}"/> struct.
    /// </summary>
    /// <param name="value">The charge unit in <see cref="QuectoCoulombs"/>.</param>
    private Charge(T value) => QuectoCoulombs = value;

    /// <summary>
    /// Gets the charge in Quectocoulombs (qC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qC.
    /// </remarks>
    public T QuectoCoulombs { get; }

    /// <summary>
    /// Gets the charge in Rontocoulombs (rC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rC.
    /// </remarks>
    public T RontoCoulombs => QuectoCoulombs.ToRontoUnits();

    /// <summary>
    /// Gets the charge in Yoctocoulombs (yC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yC.
    /// </remarks>
    public T YoctoCoulombs => QuectoCoulombs.ToYoctoUnits();

    /// <summary>
    /// Gets the charge in Zeptocoulombs (zC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zC.
    /// </remarks>
    public T ZeptoCoulombs => QuectoCoulombs.ToZeptoUnits();

    /// <summary>
    /// Gets the charge in Attocoulombs (aC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is aC.
    /// </remarks>
    public T AttoCoulombs => QuectoCoulombs.ToAttoUnits();

    /// <summary>
    /// Gets the charge in Femtocoulombs (fC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fC.
    /// </remarks>
    public T FemtoCoulombs => QuectoCoulombs.ToFemtoUnits();

    /// <summary>
    /// Gets the charge in Picocoulombs (pC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is pC.
    /// </remarks>
    public T PicoCoulombs => QuectoCoulombs.ToPicoUnits();

    /// <summary>
    /// Gets the charge in Nanocoulombs (nC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is nC.
    /// </remarks>
    public T NanoCoulombs => QuectoCoulombs.ToNanoUnits();

    /// <summary>
    /// Gets the charge in Microcoulombs (µC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is uC.
    /// </remarks>
    public T MicroCoulombs => QuectoCoulombs.ToMicroUnits();

    /// <summary>
    /// Gets the charge in Millicoulombs (mC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mC.
    /// </remarks>
    public T MilliCoulombs => QuectoCoulombs.ToMilliUnits();

    /// <summary>
    /// Gets the charge in Centicoulombs (cC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cC.
    /// </remarks>
    public T CentiCoulombs => QuectoCoulombs.ToCentiUnits();

    /// <summary>
    /// Gets the charge in Decicoulombs (dC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is dC.
    /// </remarks>
    public T DeciCoulombs => QuectoCoulombs.ToDeciUnits();

    /// <summary>
    /// Gets the charge in Coulombs (C).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is C.
    /// </remarks>
    public T Coulombs => QuectoCoulombs.ToBaseUnits();

    /// <summary>
    /// Gets the charge in Decacoulombs (daC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is daC.
    /// </remarks>
    public T DecaCoulombs => QuectoCoulombs.ToDecaUnits();

    /// <summary>
    /// Gets the charge in Hectocoulombs (hC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hC.
    /// </remarks>
    public T HectoCoulombs => QuectoCoulombs.ToHectoUnits();

    /// <summary>
    /// Gets the charge in Kilocoulombs (kC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is kC.
    /// </remarks>
    public T KiloCoulombs => QuectoCoulombs.ToKiloUnits();

    /// <summary>
    /// Gets the charge in Megacoulombs (MC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is MC.
    /// </remarks>
    public T MegaCoulombs => QuectoCoulombs.ToMegaUnits();

    /// <summary>
    /// Gets the charge in Gigacoulombs (GC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is GC.
    /// </remarks>
    public T GigaCoulombs => QuectoCoulombs.ToGigaUnits();

    /// <summary>
    /// Gets the charge in Teracoulombs (TC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is TC.
    /// </remarks>
    public T TeraCoulombs => QuectoCoulombs.ToTeraUnits();

    /// <summary>
    /// Gets the charge in Petacoulombs (PC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is PC.
    /// </remarks>
    public T PetaCoulombs => QuectoCoulombs.ToPetaUnits();

    /// <summary>
    /// Gets the charge in Exacoulombs (EC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is EC.
    /// </remarks>
    public T ExaCoulombs => QuectoCoulombs.ToExaUnits();

    /// <summary>
    /// Gets the charge in Zettacoulombs (ZC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ZC.
    /// </remarks>
    public T ZettaCoulombs => QuectoCoulombs.ToZettaUnits();

    /// <summary>
    /// Gets the charge in Yottacoulombs (YC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is YC.
    /// </remarks>
    public T YottaCoulombs => QuectoCoulombs.ToYottaUnits();

    /// <summary>
    /// Gets the charge in Ronnacoulombs (RC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is RC.
    /// </remarks>
    public T RonnaCoulombs => QuectoCoulombs.ToRonnaUnits();

    /// <summary>
    /// Gets the charge in Quettacoulombs (QC).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is QC.
    /// </remarks>
    public T QuettaCoulombs => QuectoCoulombs.ToQuettaUnits();

    /// <summary>
    /// Gets the charge in Ampere-hours (A·h).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ah.
    /// </remarks>
    public T AmpereHours => QuectoCoulombs / T.CreateChecked(3.6e33);

    /// <summary>
    /// Gets the charge in Milliampere-hours (mA·h).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is mAh.
    /// </remarks>
    public T MilliampereHours => QuectoCoulombs / T.CreateChecked(3.6e30);

    /// <summary>
    /// Gets the charge in elementary charges (e), where e = 1.602176634×10⁻¹⁹ C.
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is e.
    /// </remarks>
    public T ElementaryCharges => QuectoCoulombs / T.CreateChecked(1.602176634e11);
}
