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
        public T FromRontoUnits() => receiver * T.CreateChecked(1e3);
        public T FromYoctoUnits() => receiver * T.CreateChecked(1e6);
        public T FromZeptoUnits() => receiver * T.CreateChecked(1e9);
        public T FromAttoUnits() => receiver * T.CreateChecked(1e12);
        public T FromFemtoUnits() => receiver * T.CreateChecked(1e15);
        public T FromPicoUnits() => receiver * T.CreateChecked(1e18);
        public T FromNanoUnits() => receiver * T.CreateChecked(1e21);
        public T FromMicroUnits() => receiver * T.CreateChecked(1e24);
        public T FromMilliUnits() => receiver * T.CreateChecked(1e27);
        public T FromCentiUnits() => receiver * T.CreateChecked(1e28);
        public T FromDeciUnits() => receiver * T.CreateChecked(1e29);
        public T FromBaseUnits() => receiver * T.CreateChecked(1e30);
        public T FromDecaUnits() => receiver * T.CreateChecked(1e31);
        public T FromHectoUnits() => receiver * T.CreateChecked(1e32);
        public T FromKiloUnits() => receiver * T.CreateChecked(1e33);
        public T FromMegaUnits() => receiver * T.CreateChecked(1e36);
        public T FromGigaUnits() => receiver * T.CreateChecked(1e39);
        public T FromTeraUnits() => receiver * T.CreateChecked(1e42);
        public T FromPetaUnits() => receiver * T.CreateChecked(1e45);
        public T FromExaUnits() => receiver * T.CreateChecked(1e48);
        public T FromZettaUnits() => receiver * T.CreateChecked(1e51);
        public T FromYottaUnits() => receiver * T.CreateChecked(1e54);
        public T FromRonnaUnits() => receiver * T.CreateChecked(1e57);
        public T FromQuettaUnits() => receiver * T.CreateChecked(1e60);

        public T ToRontoUnits() => receiver / T.CreateChecked(1e3);
        public T ToYoctoUnits() => receiver / T.CreateChecked(1e6);
        public T ToZeptoUnits() => receiver / T.CreateChecked(1e9);
        public T ToAttoUnits() => receiver / T.CreateChecked(1e12);
        public T ToFemtoUnits() => receiver / T.CreateChecked(1e15);
        public T ToPicoUnits() => receiver / T.CreateChecked(1e18);
        public T ToNanoUnits() => receiver / T.CreateChecked(1e21);
        public T ToMicroUnits() => receiver / T.CreateChecked(1e24);
        public T ToMilliUnits() => receiver / T.CreateChecked(1e27);
        public T ToCentiUnits() => receiver / T.CreateChecked(1e28);
        public T ToDeciUnits() => receiver / T.CreateChecked(1e29);
        public T ToBaseUnits() => receiver / T.CreateChecked(1e30);
        public T ToDecaUnits() => receiver / T.CreateChecked(1e31);
        public T ToHectoUnits() => receiver / T.CreateChecked(1e32);
        public T ToKiloUnits() => receiver / T.CreateChecked(1e33);
        public T ToMegaUnits() => receiver / T.CreateChecked(1e36);
        public T ToGigaUnits() => receiver / T.CreateChecked(1e39);
        public T ToTeraUnits() => receiver / T.CreateChecked(1e42);
        public T ToPetaUnits() => receiver / T.CreateChecked(1e45);
        public T ToExaUnits() => receiver / T.CreateChecked(1e48);
        public T ToZettaUnits() => receiver / T.CreateChecked(1e51);
        public T ToYottaUnits() => receiver / T.CreateChecked(1e54);
        public T ToRonnaUnits() => receiver / T.CreateChecked(1e57);
        public T ToQuettaUnits() => receiver / T.CreateChecked(1e60);
    }
}
