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
using System.Linq;
using System.Linq.Expressions;

namespace OnixLabs.Core.UnitTests.Data;

public class PersonSpecification(Expression<Func<Person, bool>> expression) :
    CriteriaSpecification<Person>(expression);

public sealed class PersonNameEqualsSpecification(string name) :
    PersonSpecification(person => person.Name == name);

public sealed class PersonNameStartsWithSpecification(string name) :
    PersonSpecification(person => person.Name.StartsWith(name));

public sealed class PersonOlderThanSpecification(int age) :
    PersonSpecification(person => person.Age > age);

public sealed class PersonHasLocationSpecification(Location location) :
    PersonSpecification(person => person.Locations.Contains(location));

public sealed class PersonHasLocationCitySpecification(string city) :
    PersonSpecification(person => person.Locations.Any(location => location.City == city));
