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
/// Represents a unit of frequency.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public readonly partial struct Frequency<T> :
    IValueEquatable<Frequency<T>>,
    IValueComparable<Frequency<T>>,
    ISpanFormattable
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Frequency{T}"/> struct.
    /// </summary>
    /// <param name="value">The frequency unit in <see cref="YoctoHertz"/>.</param>
    private Frequency(T value) => YoctoHertz = value;

    /// <summary>
    /// Gets the frequency in yoctohertz (yHz).
    /// </summary>
    public T YoctoHertz { get; }

    /// <summary>
    /// Gets the frequency in zeptohertz (zHz).
    /// </summary>
    public T ZeptoHertz => YoctoHertz / T.CreateChecked(1e3);

    /// <summary>
    /// Gets the frequency in attohertz (aHz).
    /// </summary>
    public T AttoHertz => YoctoHertz / T.CreateChecked(1e6);

    /// <summary>
    /// Gets the frequency in femtohertz (fHz).
    /// </summary>
    public T FemtoHertz => YoctoHertz / T.CreateChecked(1e9);

    /// <summary>
    /// Gets the frequency in picohertz (pHz).
    /// </summary>
    public T PicoHertz => YoctoHertz / T.CreateChecked(1e12);

    /// <summary>
    /// Gets the frequency in nanohertz (nHz).
    /// </summary>
    public T NanoHertz => YoctoHertz / T.CreateChecked(1e15);

    /// <summary>
    /// Gets the frequency in microhertz (µHz).
    /// </summary>
    public T MicroHertz => YoctoHertz / T.CreateChecked(1e18);

    /// <summary>
    /// Gets the frequency in millihertz (mHz).
    /// </summary>
    public T MilliHertz => YoctoHertz / T.CreateChecked(1e21);

    /// <summary>
    /// Gets the frequency in hertz (Hz).
    /// </summary>
    public T Hertz => YoctoHertz / T.CreateChecked(1e24);

    /// <summary>
    /// Gets the frequency in kilohertz (kHz).
    /// </summary>
    public T KiloHertz => YoctoHertz / T.CreateChecked(1e27);

    /// <summary>
    /// Gets the frequency in megahertz (MHz).
    /// </summary>
    public T MegaHertz => YoctoHertz / T.CreateChecked(1e30);

    /// <summary>
    /// Gets the frequency in gigahertz (GHz).
    /// </summary>
    public T GigaHertz => YoctoHertz / T.CreateChecked(1e33);

    /// <summary>
    /// Gets the frequency in terahertz (THz).
    /// </summary>
    public T TeraHertz => YoctoHertz / T.CreateChecked(1e36);

    /// <summary>
    /// Gets the frequency in petahertz (PHz).
    /// </summary>
    public T PetaHertz => YoctoHertz / T.CreateChecked(1e39);

    /// <summary>
    /// Gets the frequency in exahertz (EHz).
    /// </summary>
    public T ExaHertz => YoctoHertz / T.CreateChecked(1e42);

    /// <summary>
    /// Gets the frequency in zettahertz (ZHz).
    /// </summary>
    public T ZettaHertz => YoctoHertz / T.CreateChecked(1e45);

    /// <summary>
    /// Gets the frequency in yottahertz (YHz).
    /// </summary>
    public T YottaHertz => YoctoHertz / T.CreateChecked(1e48);

    /// <summary>
    /// Gets the frequency in revolutions per minute (rpm).
    /// </summary>
    public T RevolutionsPerMinute => Hertz * T.CreateChecked(60);
}
