// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Data;

/// <summary>
/// Represents the base contract for implementing a repository.
/// </summary>
/// <typeparam name="TKey">The underlying key type.</typeparam>
/// <typeparam name="TModel">The underlying model type.</typeparam>
public interface IRepository<in TKey, TModel> where TModel : class
{
    /// <summary>
    /// Finds an item in the repository using the provided key.
    /// </summary>
    /// <param name="id">The key for which to get an item from the repository.</param>
    /// <returns>Returns an item whose key matches the given key.</returns>
    TModel? FindById(TKey id);

    /// <summary>
    /// Gets all items from the repository.
    /// </summary>
    /// <returns>Returns all items from the repository.</returns>
    IEnumerable<TModel> GetAll();

    /// <summary>
    /// Adds an item to the repository.
    /// </summary>
    /// <param name="item">The item to add to the repository.</param>
    void Add(TModel item);

    /// <summary>
    /// Adds a collection of items to the repository.
    /// </summary>
    /// <param name="items">The collection of items to add to the repository.</param>
    void AddRange(IEnumerable<TModel> items);

    /// <summary>
    /// Removes an item from the repository.
    /// </summary>
    /// <param name="item">The item to remove from the repository.</param>
    void Remove(TModel item);

    /// <summary>
    /// Removes a collection of items from the repository.
    /// </summary>
    /// <param name="items">The collection of items to remove from the repository.</param>
    void RemoveRange(IEnumerable<TModel> items);
}
