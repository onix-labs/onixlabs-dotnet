// Copyright 2020-2021 ONIXLabs
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

using System.Text;

namespace OnixLabs.Core.Text
{
    /// <summary>
    /// Represents a Base-32 value.
    /// </summary>
    public readonly partial struct Base32
    {
        /// <summary>
        /// Returns a <see cref="byte"/> array that represents the current object.
        /// </summary>
        /// <returns>Returns a <see cref="byte"/> array that represents the current object.</returns>
        public byte[] ToByteArray()
        {
            return value.Copy();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object in plain text.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> that represents the current object in plain text.</returns>
        public string ToPlainTextString()
        {
            return ToPlainTextString(Encoding.Default);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object in plain text.
        /// </summary>
        /// <param name="encoding">The encoding to use to obtain the underlying value.</param>
        /// <returns>Returns a <see cref="string"/> that represents the current object in plain text.</returns>
        public string ToPlainTextString(Encoding encoding)
        {
            return encoding.GetString(value);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current object.</returns>
        public override string ToString()
        {
            return Encode(value, alphabet.Alphabet, padding);
        }
    }
}
