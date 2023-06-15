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
using System.Threading.Tasks;
using OnixLabs.Core.Specifications;

namespace OnixLabs.Core.Data;

/// <summary>
/// Represents the base contract for asynchronous repositories that are able to query a data source by specification.
/// </summary>
/// <typeparam name="TModel">The underlying model type.</typeparam>
public interface IAsyncSpecificationRepository<TModel> where TModel : class
{
    /// <summary>
    /// Finds a single item in the data source by specification.
    /// </summary>
    /// <param name="specification">The specification for the item to find in the data source.</param>
    /// <returns>Returns a single item from the data source that matches the specification.</returns>
    Task<TModel?> FindBySpecificationAsync(Specification<TModel> specification);

    /// <summary>
    /// Finds all items in the data source by specification.
    /// </summary>
    /// <param name="specification">The specification for the items to find in the data source.</param>
    /// <returns>Returns all items from the data source that match the specification.</returns>
    Task<IEnumerable<TModel>> FindAllBySpecificationAsync(Specification<TModel> specification);
}
