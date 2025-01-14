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

namespace OnixLabs.Core.UnitTests.Data;

public sealed record Location(string City, string Country)
{
    public static readonly Location London = new("London", "England");
    public static readonly Location Paris = new("Paris", "France");
    public static readonly Location Lisbon = new("Lisbon", "Postugal");
    public static readonly Location Berlin = new("Berlin", "Germany");
    public static readonly Location Brussels = new("Brussels", "Belgium");
}
