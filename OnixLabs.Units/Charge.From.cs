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

public readonly partial struct Charge<T>
{
    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Quectocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromQuectocoulombs(T value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Rontocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromRontocoulombs(T value) => new(value.FromRontoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Yoctocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromYoctocoulombs(T value) => new(value.FromYoctoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Zeptocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromZeptocoulombs(T value) => new(value.FromZeptoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Attocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromAttocoulombs(T value) => new(value.FromAttoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Femtocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromFemtocoulombs(T value) => new(value.FromFemtoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Picocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromPicocoulombs(T value) => new(value.FromPicoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Nanocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromNanocoulombs(T value) => new(value.FromNanoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Microcoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromMicrocoulombs(T value) => new(value.FromMicroUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Millicoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromMillicoulombs(T value) => new(value.FromMilliUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Centicoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromCenticoulombs(T value) => new(value.FromCentiUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Decicoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromDecicoulombs(T value) => new(value.FromDeciUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Coulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromCoulombs(T value) => new(value.FromBaseUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Decacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromDecacoulombs(T value) => new(value.FromDecaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Hectocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromHectocoulombs(T value) => new(value.FromHectoUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Kilocoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromKilocoulombs(T value) => new(value.FromKiloUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Megacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromMegacoulombs(T value) => new(value.FromMegaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Gigacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromGigacoulombs(T value) => new(value.FromGigaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Teracoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromTeracoulombs(T value) => new(value.FromTeraUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Petacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromPetacoulombs(T value) => new(value.FromPetaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Exacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromExacoulombs(T value) => new(value.FromExaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Zettacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromZettacoulombs(T value) => new(value.FromZettaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Yottacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromYottacoulombs(T value) => new(value.FromYottaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Ronnacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromRonnacoulombs(T value) => new(value.FromRonnaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Quettacoulombs value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromQuettacoulombs(T value) => new(value.FromQuettaUnits());

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Ampere-hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromAmpereHours(T value) => new(value * T.CreateChecked(3.6e33));

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified Milliampere-hours value.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromMilliampereHours(T value) => new(value * T.CreateChecked(3.6e30));

    /// <summary>
    /// Creates a new <see cref="Charge{T}"/> instance from the specified number of elementary charges (e), where e = 1.602176634×10⁻¹⁹ C.
    /// </summary>
    /// <param name="value">The value from which to construct the new <see cref="Charge{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="Charge{T}"/> instance from the specified value.</returns>
    public static Charge<T> FromElementaryCharges(T value) => new(value * T.CreateChecked(1.602176634e11));
}
