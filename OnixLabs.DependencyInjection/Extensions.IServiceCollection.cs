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
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace OnixLabs.DependencyInjection;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds a service of the type specified in <typeparamref name="TService"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The underlying type of the service to add.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddService<TService>(
        this IServiceCollection services,
        ServiceLifetime lifetime = default
    ) where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddSingleton<TService>(),
        ServiceLifetime.Scoped => services.AddScoped<TService>(),
        ServiceLifetime.Transient => services.AddTransient<TService>(),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a keyed service of the type specified in <typeparamref name="TService"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the service.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The underlying type of the service to add.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddKeyedService<TService>(
        this IServiceCollection services,
        object? serviceKey,
        ServiceLifetime lifetime = default
    ) where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddKeyedSingleton<TService>(serviceKey),
        ServiceLifetime.Scoped => services.AddKeyedScoped<TService>(serviceKey),
        ServiceLifetime.Transient => services.AddKeyedTransient<TService>(serviceKey),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a service of the type specified in <typeparamref name="TService"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The underlying type of the service to add.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddService<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory,
        ServiceLifetime lifetime = default
    ) where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddSingleton(implementationFactory),
        ServiceLifetime.Scoped => services.AddScoped(implementationFactory),
        ServiceLifetime.Transient => services.AddTransient(implementationFactory),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds keyed a service of the type specified in <typeparamref name="TService"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="implementationFactory">The factory that creates the service.</param>
    /// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the service.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The underlying type of the service to add.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddKeyedService<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, object?, TService> implementationFactory,
        object? serviceKey,
        ServiceLifetime lifetime = default
    ) where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddKeyedSingleton(serviceKey, implementationFactory),
        ServiceLifetime.Scoped => services.AddKeyedScoped(serviceKey, implementationFactory),
        ServiceLifetime.Transient => services.AddKeyedTransient(serviceKey, implementationFactory),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a service of the type specified in <typeparamref name="TService"/> with an implementation type
    /// specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddService<TService, TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime = default
    ) where TImplementation : class, TService where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddSingleton<TService, TImplementation>(),
        ServiceLifetime.Scoped => services.AddScoped<TService, TImplementation>(),
        ServiceLifetime.Transient => services.AddTransient<TService, TImplementation>(),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a keyed service of the type specified in <typeparamref name="TService"/> with an implementation type
    /// specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the service.</param>
    /// <param name="lifetime">The lifetime of the specified <typeparamref name="TService"/> service. The default lifetime is <see cref="ServiceLifetime.Singleton"/>.</param>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddKeyedService<TService, TImplementation>(
        this IServiceCollection services,
        object? serviceKey,
        ServiceLifetime lifetime = default
    ) where TImplementation : class, TService where TService : class => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddKeyedSingleton<TService, TImplementation>(serviceKey),
        ServiceLifetime.Scoped => services.AddKeyedScoped<TService, TImplementation>(serviceKey),
        ServiceLifetime.Transient => services.AddKeyedTransient<TService, TImplementation>(serviceKey),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a service of the type specified in <paramref name="serviceType"/> with an implementation of the type
    /// specified in <paramref name="implementationType"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <param name="lifetime">The lifetime of the specified service.</param>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddService(
        this IServiceCollection services,
        Type serviceType,
        Type implementationType, ServiceLifetime lifetime = default
    ) => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
        ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
        ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };

    /// <summary>
    /// Adds a keyed service of the type specified in <paramref name="serviceType"/> with an implementation of the type
    /// specified in <paramref name="implementationType"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="serviceType">The type of the service to register.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <param name="serviceKey">The <see cref="ServiceDescriptor.ServiceKey"/> of the service.</param>
    /// <param name="lifetime">The lifetime of the specified service.</param>
    /// <returns>Returns a reference to the current <see cref="IServiceCollection"/> instance after the operation has completed.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified <paramref name="lifetime"/> is not defined.</exception>
    public static IServiceCollection AddKeyedService(
        this IServiceCollection services,
        Type serviceType,
        Type implementationType,
        object? serviceKey,
        ServiceLifetime lifetime = default
    ) => RequireIsDefined(lifetime, nameof(lifetime)) switch
    {
        ServiceLifetime.Singleton => services.AddKeyedSingleton(serviceType, serviceKey, implementationType),
        ServiceLifetime.Scoped => services.AddKeyedScoped(serviceType, serviceKey, implementationType),
        ServiceLifetime.Transient => services.AddKeyedTransient(serviceType, serviceKey, implementationType),
        _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
    };
}
