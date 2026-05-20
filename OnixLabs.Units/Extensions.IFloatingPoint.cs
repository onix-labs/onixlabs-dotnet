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

using System.ComponentModel;
using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Provides extension methods for <see cref="IFloatingPoint{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IFloatingPointExtensions
{
    extension<T>(T receiver) where T : IFloatingPoint<T>
    {
        public T FromRontoUnits() => receiver * UnitMath.Pow10<T>(3);
        public T FromYoctoUnits() => receiver * UnitMath.Pow10<T>(6);
        public T FromZeptoUnits() => receiver * UnitMath.Pow10<T>(9);
        public T FromAttoUnits() => receiver * UnitMath.Pow10<T>(12);
        public T FromFemtoUnits() => receiver * UnitMath.Pow10<T>(15);
        public T FromPicoUnits() => receiver * UnitMath.Pow10<T>(18);
        public T FromNanoUnits() => receiver * UnitMath.Pow10<T>(21);
        public T FromMicroUnits() => receiver * UnitMath.Pow10<T>(24);
        public T FromMilliUnits() => receiver * UnitMath.Pow10<T>(27);
        public T FromCentiUnits() => receiver * UnitMath.Pow10<T>(28);
        public T FromDeciUnits() => receiver * UnitMath.Pow10<T>(29);
        public T FromBaseUnits() => receiver * UnitMath.Pow10<T>(30);
        public T FromDecaUnits() => receiver * UnitMath.Pow10<T>(31);
        public T FromHectoUnits() => receiver * UnitMath.Pow10<T>(32);
        public T FromKiloUnits() => receiver * UnitMath.Pow10<T>(33);
        public T FromMegaUnits() => receiver * UnitMath.Pow10<T>(36);
        public T FromGigaUnits() => receiver * UnitMath.Pow10<T>(39);
        public T FromTeraUnits() => receiver * UnitMath.Pow10<T>(42);
        public T FromPetaUnits() => receiver * UnitMath.Pow10<T>(45);
        public T FromExaUnits() => receiver * UnitMath.Pow10<T>(48);
        public T FromZettaUnits() => receiver * UnitMath.Pow10<T>(51);
        public T FromYottaUnits() => receiver * UnitMath.Pow10<T>(54);
        public T FromRonnaUnits() => receiver * UnitMath.Pow10<T>(57);
        public T FromQuettaUnits() => receiver * UnitMath.Pow10<T>(60);

        public T ToRontoUnits() => receiver / UnitMath.Pow10<T>(3);
        public T ToYoctoUnits() => receiver / UnitMath.Pow10<T>(6);
        public T ToZeptoUnits() => receiver / UnitMath.Pow10<T>(9);
        public T ToAttoUnits() => receiver / UnitMath.Pow10<T>(12);
        public T ToFemtoUnits() => receiver / UnitMath.Pow10<T>(15);
        public T ToPicoUnits() => receiver / UnitMath.Pow10<T>(18);
        public T ToNanoUnits() => receiver / UnitMath.Pow10<T>(21);
        public T ToMicroUnits() => receiver / UnitMath.Pow10<T>(24);
        public T ToMilliUnits() => receiver / UnitMath.Pow10<T>(27);
        public T ToCentiUnits() => receiver / UnitMath.Pow10<T>(28);
        public T ToDeciUnits() => receiver / UnitMath.Pow10<T>(29);
        public T ToBaseUnits() => receiver / UnitMath.Pow10<T>(30);
        public T ToDecaUnits() => receiver / UnitMath.Pow10<T>(31);
        public T ToHectoUnits() => receiver / UnitMath.Pow10<T>(32);
        public T ToKiloUnits() => receiver / UnitMath.Pow10<T>(33);
        public T ToMegaUnits() => receiver / UnitMath.Pow10<T>(36);
        public T ToGigaUnits() => receiver / UnitMath.Pow10<T>(39);
        public T ToTeraUnits() => receiver / UnitMath.Pow10<T>(42);
        public T ToPetaUnits() => receiver / UnitMath.Pow10<T>(45);
        public T ToExaUnits() => receiver / UnitMath.Pow10<T>(48);
        public T ToZettaUnits() => receiver / UnitMath.Pow10<T>(51);
        public T ToYottaUnits() => receiver / UnitMath.Pow10<T>(54);
        public T ToRonnaUnits() => receiver / UnitMath.Pow10<T>(57);
        public T ToQuettaUnits() => receiver / UnitMath.Pow10<T>(60);
    }
}
