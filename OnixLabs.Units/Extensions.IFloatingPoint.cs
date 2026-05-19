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
using OnixLabs.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Provides extension methods for <see cref="IFloatingPoint{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IFloatingPointExtensions
{
    extension<T>(T receiver) where T : IFloatingPoint<T>
    {
        public T FromRontoUnits() => receiver * GenericMath.Pow10<T>(3);
        public T FromYoctoUnits() => receiver * GenericMath.Pow10<T>(6);
        public T FromZeptoUnits() => receiver * GenericMath.Pow10<T>(9);
        public T FromAttoUnits() => receiver * GenericMath.Pow10<T>(12);
        public T FromFemtoUnits() => receiver * GenericMath.Pow10<T>(15);
        public T FromPicoUnits() => receiver * GenericMath.Pow10<T>(18);
        public T FromNanoUnits() => receiver * GenericMath.Pow10<T>(21);
        public T FromMicroUnits() => receiver * GenericMath.Pow10<T>(24);
        public T FromMilliUnits() => receiver * GenericMath.Pow10<T>(27);
        public T FromCentiUnits() => receiver * GenericMath.Pow10<T>(28);
        public T FromDeciUnits() => receiver * GenericMath.Pow10<T>(29);
        public T FromBaseUnits() => receiver * GenericMath.Pow10<T>(30);
        public T FromDecaUnits() => receiver * GenericMath.Pow10<T>(31);
        public T FromHectoUnits() => receiver * GenericMath.Pow10<T>(32);
        public T FromKiloUnits() => receiver * GenericMath.Pow10<T>(33);
        public T FromMegaUnits() => receiver * GenericMath.Pow10<T>(36);
        public T FromGigaUnits() => receiver * GenericMath.Pow10<T>(39);
        public T FromTeraUnits() => receiver * GenericMath.Pow10<T>(42);
        public T FromPetaUnits() => receiver * GenericMath.Pow10<T>(45);
        public T FromExaUnits() => receiver * GenericMath.Pow10<T>(48);
        public T FromZettaUnits() => receiver * GenericMath.Pow10<T>(51);
        public T FromYottaUnits() => receiver * GenericMath.Pow10<T>(54);
        public T FromRonnaUnits() => receiver * GenericMath.Pow10<T>(57);
        public T FromQuettaUnits() => receiver * GenericMath.Pow10<T>(60);

        public T ToRontoUnits() => receiver / GenericMath.Pow10<T>(3);
        public T ToYoctoUnits() => receiver / GenericMath.Pow10<T>(6);
        public T ToZeptoUnits() => receiver / GenericMath.Pow10<T>(9);
        public T ToAttoUnits() => receiver / GenericMath.Pow10<T>(12);
        public T ToFemtoUnits() => receiver / GenericMath.Pow10<T>(15);
        public T ToPicoUnits() => receiver / GenericMath.Pow10<T>(18);
        public T ToNanoUnits() => receiver / GenericMath.Pow10<T>(21);
        public T ToMicroUnits() => receiver / GenericMath.Pow10<T>(24);
        public T ToMilliUnits() => receiver / GenericMath.Pow10<T>(27);
        public T ToCentiUnits() => receiver / GenericMath.Pow10<T>(28);
        public T ToDeciUnits() => receiver / GenericMath.Pow10<T>(29);
        public T ToBaseUnits() => receiver / GenericMath.Pow10<T>(30);
        public T ToDecaUnits() => receiver / GenericMath.Pow10<T>(31);
        public T ToHectoUnits() => receiver / GenericMath.Pow10<T>(32);
        public T ToKiloUnits() => receiver / GenericMath.Pow10<T>(33);
        public T ToMegaUnits() => receiver / GenericMath.Pow10<T>(36);
        public T ToGigaUnits() => receiver / GenericMath.Pow10<T>(39);
        public T ToTeraUnits() => receiver / GenericMath.Pow10<T>(42);
        public T ToPetaUnits() => receiver / GenericMath.Pow10<T>(45);
        public T ToExaUnits() => receiver / GenericMath.Pow10<T>(48);
        public T ToZettaUnits() => receiver / GenericMath.Pow10<T>(51);
        public T ToYottaUnits() => receiver / GenericMath.Pow10<T>(54);
        public T ToRonnaUnits() => receiver / GenericMath.Pow10<T>(57);
        public T ToQuettaUnits() => receiver / GenericMath.Pow10<T>(60);
    }
}
