// Copyright © 2020 ONIXLabs
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

using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography.UnitTests.Data.Objects;

public sealed class MerkleNode : IEquatable<MerkleNode>, IHashable
{
    public MerkleNode(string text, int number, DateTime moment, Guid identifier)
    {
        Text = text;
        Number = number;
        Moment = moment;
        Identifier = identifier;
    }

    public string Text { get; }
    public int Number { get; }
    public DateTime Moment { get; }
    public Guid Identifier { get; }

    public Hash ComputeHash()
    {
        return Hash.ComputeSha2Hash256(ToString());
    }

    public bool Equals(MerkleNode? other)
    {
        return ReferenceEquals(this, other)
               || other is not null
               && other.Text == Text
               && other.Number == Number
               && other.Moment == Moment
               && other.Identifier == Identifier;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MerkleNode);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Text, Number, Moment, Identifier);
    }

    public override string ToString()
    {
        return this.ToRecordString();
    }
}
