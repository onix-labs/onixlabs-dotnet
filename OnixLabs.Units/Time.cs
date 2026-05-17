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
/// Represents a unit of time.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Time<T> : IAdditiveUnit<Time<T>>, IMultiplicativeUnit<Time<T>> where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Time{T}"/> struct.
    /// </summary>
    /// <param name="value">The time unit in <see cref="QuectoSeconds"/>.</param>
    private Time(T value) => QuectoSeconds = value;

    /// <summary>
    /// Gets the time in Quectoseconds (qs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is qs.
    /// </remarks>
    public T QuectoSeconds { get; }

    /// <summary>
    /// Gets the time in Rontoseconds (rs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is rs.
    /// </remarks>
    public T RontoSeconds => QuectoSeconds.ToRontoUnits();

    /// <summary>
    /// Gets the time in Yoctoseconds (ys).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ys.
    /// </remarks>
    public T YoctoSeconds => QuectoSeconds.ToYoctoUnits();

    /// <summary>
    /// Gets the time in Zeptoseconds (zs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is zs.
    /// </remarks>
    public T ZeptoSeconds => QuectoSeconds.ToZeptoUnits();

    /// <summary>
    /// Gets the time in Attoseconds (as).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is as.
    /// </remarks>
    public T AttoSeconds => QuectoSeconds.ToAttoUnits();

    /// <summary>
    /// Gets the time in Femtoseconds (fs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is fs.
    /// </remarks>
    public T FemtoSeconds => QuectoSeconds.ToFemtoUnits();

    /// <summary>
    /// Gets the time in Picoseconds (ps).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ps.
    /// </remarks>
    public T PicoSeconds => QuectoSeconds.ToPicoUnits();

    /// <summary>
    /// Gets the time in Nanoseconds (ns).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ns.
    /// </remarks>
    public T NanoSeconds => QuectoSeconds.ToNanoUnits();

    /// <summary>
    /// Gets the time in Microseconds (µs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is us.
    /// </remarks>
    public T MicroSeconds => QuectoSeconds.ToMicroUnits();

    /// <summary>
    /// Gets the time in Milliseconds (ms).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ms.
    /// </remarks>
    public T MilliSeconds => QuectoSeconds.ToMilliUnits();

    /// <summary>
    /// Gets the time in Centiseconds (cs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is cs.
    /// </remarks>
    public T CentiSeconds => QuectoSeconds.ToCentiUnits();

    /// <summary>
    /// Gets the time in Deciseconds (ds).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ds.
    /// </remarks>
    public T DeciSeconds => QuectoSeconds.ToDeciUnits();

    /// <summary>
    /// Gets the time in Seconds (s).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is s.
    /// </remarks>
    public T Seconds => QuectoSeconds.ToBaseUnits();

    /// <summary>
    /// Gets the time in Decaseconds (das).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is das.
    /// </remarks>
    public T DecaSeconds => QuectoSeconds.ToDecaUnits();

    /// <summary>
    /// Gets the time in Hectoseconds (hs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is hs.
    /// </remarks>
    public T HectoSeconds => QuectoSeconds.ToHectoUnits();

    /// <summary>
    /// Gets the time in Kiloseconds (ks).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is ks.
    /// </remarks>
    public T KiloSeconds => QuectoSeconds.ToKiloUnits();

    /// <summary>
    /// Gets the time in Megaseconds (Ms).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ms.
    /// </remarks>
    public T MegaSeconds => QuectoSeconds.ToMegaUnits();

    /// <summary>
    /// Gets the time in Gigaseconds (Gs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Gs.
    /// </remarks>
    public T GigaSeconds => QuectoSeconds.ToGigaUnits();

    /// <summary>
    /// Gets the time in Teraseconds (Ts).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ts.
    /// </remarks>
    public T TeraSeconds => QuectoSeconds.ToTeraUnits();

    /// <summary>
    /// Gets the time in Petaseconds (Ps).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ps.
    /// </remarks>
    public T PetaSeconds => QuectoSeconds.ToPetaUnits();

    /// <summary>
    /// Gets the time in Exaseconds (Es).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Es.
    /// </remarks>
    public T ExaSeconds => QuectoSeconds.ToExaUnits();

    /// <summary>
    /// Gets the time in Zettaseconds (Zs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Zs.
    /// </remarks>
    public T ZettaSeconds => QuectoSeconds.ToZettaUnits();

    /// <summary>
    /// Gets the time in Yottaseconds (Ys).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Ys.
    /// </remarks>
    public T YottaSeconds => QuectoSeconds.ToYottaUnits();

    /// <summary>
    /// Gets the time in Ronnaseconds (Rs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Rs.
    /// </remarks>
    public T RonnaSeconds => QuectoSeconds.ToRonnaUnits();

    /// <summary>
    /// Gets the time in Quettaseconds (Qs).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is Qs.
    /// </remarks>
    public T QuettaSeconds => QuectoSeconds.ToQuettaUnits();

    /// <summary>
    /// Gets the time in Minutes (min).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is min.
    /// </remarks>
    public T Minutes => QuectoSeconds / T.CreateChecked(6e31);

    /// <summary>
    /// Gets the time in Hours (h).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is h.
    /// </remarks>
    public T Hours => QuectoSeconds / T.CreateChecked(3.6e33);

    /// <summary>
    /// Gets the time in Days (d).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is d.
    /// </remarks>
    public T Days => QuectoSeconds / T.CreateChecked(8.64e34);

    /// <summary>
    /// Gets the time in Weeks (wk).
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is wk.
    /// </remarks>
    public T Weeks => QuectoSeconds / T.CreateChecked(6.048e35);

    /// <summary>
    /// Gets the time in Julian Years (yr), defined as 365.25 days.
    /// </summary>
    /// <remarks>
    /// The format specifier for this value is yr.
    /// </remarks>
    public T JulianYears => QuectoSeconds / T.CreateChecked(3.15576e37);
}
