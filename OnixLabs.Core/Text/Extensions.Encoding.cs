// Copyright 2020 ONIXLabs
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
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides extension methods for text encodings.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class EncodingExtensions
{
    /// <summary>
    /// The default encoding, which is <see cref="Encoding.UTF8"/>.
    /// </summary>
    private static readonly Encoding Default = Encoding.UTF8;

    /// <summary>
    /// Gets the current <see cref="Encoding"/>, or the default encoding if the current <see cref="Encoding"/> is <see langword="null"/>.
    /// </summary>
    /// <param name="encoding">The current <see cref="Encoding"/>.</param>
    /// <returns>Returns the current <see cref="Encoding"/>, or the default encoding if the current <see cref="Encoding"/> is <see langword="null"/>.</returns>
    public static Encoding GetOrDefault(this Encoding? encoding) => encoding ?? Default;
}
