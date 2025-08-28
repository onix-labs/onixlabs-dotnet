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
using System.Linq;
using OnixLabs.Core.Linq;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests;

public sealed class SpecificationTests
{
    [Fact(DisplayName = "PersonNameEqualsSpecification should return the expected result")]
    public void PersonNameEqualsSpecificationShouldReturnExpectedResult()
    {
        // Given
        PersonNameEqualsSpecification specification = new("Alice");

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Single(result);
        Assert.Contains(Person.Alice, result);
    }

    [Fact(DisplayName = "PersonNameStartsWithSpecification should return the expected result")]
    public void PersonNameStartsWithSpecificationShouldReturnExpectedResult()
    {
        // Given
        PersonNameStartsWithSpecification specification = new("A");

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Single(result);
        Assert.Contains(Person.Alice, result);
    }

    [Fact(DisplayName = "PersonOlderThanSpecification should return the expected result")]
    public void PersonOlderThanSpecificationShouldReturnExpectedResult()
    {
        // Given
        PersonOlderThanSpecification specification = new(20);

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Equal(2, IEnumerableExtensions.Count(result));
        Assert.Contains(Person.Bob, result);
        Assert.Contains(Person.Charlie, result);
    }

    [Fact(DisplayName = "PersonHasLocationSpecification should return the expected result")]
    public void PersonHasLocationSpecificationShouldReturnExpectedResult()
    {
        // Given
        PersonHasLocationSpecification specification = new(Location.London);

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Equal(2, IEnumerableExtensions.Count(result));
        Assert.Contains(Person.Alice, result);
        Assert.Contains(Person.Bob, result);
    }

    [Fact(DisplayName = "PersonHasLocationCitySpecification should return the expected result")]
    public void PersonHasLocationCitySpecificationShouldReturnExpectedResult()
    {
        // Given
        PersonHasLocationCitySpecification specification = new(Location.London.City);

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Equal(2, IEnumerableExtensions.Count(result));
        Assert.Contains(Person.Alice, result);
        Assert.Contains(Person.Bob, result);
    }

    [Fact(DisplayName = "PersonSpecification.And should return the expected result")]
    public void PersonSpecificationAndShouldReturnExpectedResult()
    {
        // Given
        Specification<Person> specification = PersonSpecification.And(
            new PersonOlderThanSpecification(20),
            new PersonHasLocationCitySpecification("London")
        );

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Single(result);
        Assert.Contains(Person.Bob, result);
    }

    [Fact(DisplayName = "PersonSpecification.And should return true for an empty collection")]
    public void PersonSpecificationAndShouldReturnTrueForEmptyCollection()
    {
        // Given
        Specification<Person> specification = PersonSpecification.And();

        // When
        bool result = specification.IsSatisfiedBy(Person.Alice);

        // Then
        Assert.True(result);
    }

    [Fact(DisplayName = "PersonSpecification.Or should return the expected result")]
    public void PersonSpecificationOrShouldReturnExpectedResult()
    {
        // Given
        Specification<Person> specification = PersonSpecification.Or(
            new PersonNameStartsWithSpecification("A"),
            new PersonHasLocationCitySpecification("Lisbon")
        );

        // When
        IEnumerable<Person> result = Person.People.Where(specification).ToList();

        // Then
        Assert.Equal(2, result.Count());
        Assert.Contains(Person.Alice, result);
        Assert.Contains(Person.Bob, result);
    }

    [Fact(DisplayName = "PersonSpecification.Or should return false for an empty collection")]
    public void PersonSpecificationOrShouldReturnFalseForEmptyCollection()
    {
        // Given
        Specification<Person> specification = PersonSpecification.Or();

        // When
        bool result = specification.IsSatisfiedBy(Person.Alice);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "PersonSpecification.Not should return the expected result")]
    public void PersonSpecificationNotShouldReturnExpectedResult()
    {
        // Given
        Specification<Person> specification = PersonSpecification.Or(
            new PersonNameStartsWithSpecification("A"),
            new PersonHasLocationCitySpecification("Lisbon")
        );

        // When
        IEnumerable<Person> result = Person.People.WhereNot(specification).ToList();

        // Then
        Assert.Single(result);
        Assert.Contains(Person.Charlie, result);
    }
}
