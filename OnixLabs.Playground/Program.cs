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

using System;
using System.Collections.Generic;
using OnixLabs.Core;
using OnixLabs.Core.Linq;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        Person[] people =
        [
            new("John Smith", 32),
            new("Abu Akbar", 10),
            new("Brenda Furlong", 41),
            new("Charlie Chester", 77),
            new("David Dickinson", 19)
        ];

        Specification<Person> spec1 = !(Specification<Person>.Empty
                                        & new PersonNameEquals("John Smith")
                                        | new PersonAgeGreaterThan(40));

        IEnumerable<Person> filtered = people.Where(spec1);

        foreach (Person person in filtered)
        {
            Console.WriteLine($"{person.Name} {person.Age}");
        }
    }
}

internal sealed record Person(string Name, int Age);

internal sealed class PersonNameEquals(string name) : CriteriaSpecification<Person>(person => person.Name == name);

internal sealed class PersonAgeGreaterThan(int age) : CriteriaSpecification<Person>(person => person.Age >= age);

internal sealed class PersonAgeLessThan(int age) : CriteriaSpecification<Person>(person => person.Age <= age);
