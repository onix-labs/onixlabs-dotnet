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
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Units;

/// <summary>
/// Represents a unit of force.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Force<T> :
    IValueEquatable<Force<T>>,
    IValueComparable<Force<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Force{T}"/> struct.
    /// </summary>
    /// <param name="value">The force unit in <see cref="YoctoNewtons"/>.</param>
    private Force(T value) => YoctoNewtons = value;

    /// <summary>
    /// Gets the force in yoctonewtons (yN).
    /// </summary>
    public T YoctoNewtons { get; }

    /// <summary>
    /// Gets the force in zeptonewtons (zN).
    /// </summary>
    public T ZeptoNewtons => YoctoNewtons / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the force in attonewtons (aN).
    /// </summary>
    public T AttoNewtons => YoctoNewtons / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the force in femtonewtons (fN).
    /// </summary>
    public T FemtoNewtons => YoctoNewtons / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the force in piconewtons (pN).
    /// </summary>
    public T PicoNewtons => YoctoNewtons / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the force in nanonewtons (nN).
    /// </summary>
    public T NanoNewtons => YoctoNewtons / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the force in micronewtons (µN).
    /// </summary>
    public T MicroNewtons => YoctoNewtons / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the force in millinewtons (mN).
    /// </summary>
    public T MilliNewtons => YoctoNewtons / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the force in newtons (N).
    /// </summary>
    public T Newtons => YoctoNewtons / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the force in kilonewtons (kN).
    /// </summary>
    public T KiloNewtons => YoctoNewtons / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the force in meganewtons (MN).
    /// </summary>
    public T MegaNewtons => YoctoNewtons / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the force in giganewtons (GN).
    /// </summary>
    public T GigaNewtons => YoctoNewtons / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the force in teranewtons (TN).
    /// </summary>
    public T TeraNewtons => YoctoNewtons / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the force in petanewtons (PN).
    /// </summary>
    public T PetaNewtons => YoctoNewtons / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the force in exanewtons (EN).
    /// </summary>
    public T ExaNewtons => YoctoNewtons / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the force in zettanewtons (ZN).
    /// </summary>
    public T ZettaNewtons => YoctoNewtons / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the force in yottanewtons (YN).
    /// </summary>
    public T YottaNewtons => YoctoNewtons / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the force in dynes (dyn).
    /// </summary>
    public T Dynes => YoctoNewtons / T.CreateChecked(1e19);

    /// <summary>
    /// Gets the force in kilogram-force (kgf).
    /// </summary>
    public T KilogramForce => YoctoNewtons / T.CreateChecked(9.80665e24);

    /// <summary>
    /// Gets the force in gram-force (gf).
    /// </summary>
    public T GramForce => YoctoNewtons / T.CreateChecked(9.80665e21);

    /// <summary>
    /// Gets the force in tonne-force (tf).
    /// </summary>
    public T TonneForce => YoctoNewtons / T.CreateChecked(9.80665e27);

    /// <summary>
    /// Gets the force in pound-force (lbf).
    /// </summary>
    public T PoundForce => YoctoNewtons / T.CreateChecked(4.4482216152605e24);

    /// <summary>
    /// Gets the force in ounce-force (ozf).
    /// </summary>
    public T OunceForce => YoctoNewtons / T.CreateChecked(2.780138850953781e23);

    /// <summary>
    /// Gets the force in poundals (pdl).
    /// </summary>
    public T Poundals => YoctoNewtons / T.CreateChecked(1.38255e23);

    /// <summary>
    /// Gets the force in US short ton-force (tonf).
    /// </summary>
    public T ShortTonForce => YoctoNewtons / T.CreateChecked(8.896443230521e27);

    /// <summary>
    /// Gets the force in imperial long ton-force (ltf).
    /// </summary>
    public T LongTonForce => YoctoNewtons / T.CreateChecked(9.964016418183519e27);
}
