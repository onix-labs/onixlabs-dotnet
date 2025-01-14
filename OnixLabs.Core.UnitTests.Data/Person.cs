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

using System.Collections.Generic;

namespace OnixLabs.Core.UnitTests.Data;

public sealed record Person(string Name, int Age, IEnumerable<Location> Locations)
{
    public static readonly Person Alice = new("Alice", 12, [Location.London, Location.Paris]);
    public static readonly Person Bob = new("Bob", 23, [Location.Lisbon, Location.London]);
    public static readonly Person Charlie = new("Charlie", 34, [Location.Berlin, Location.Brussels]);

    public static readonly IEnumerable<Person> People = [Alice, Bob, Charlie];
}
