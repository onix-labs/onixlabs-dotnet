using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OnixLabs.DependencyInjection.UnitTests.Data;

namespace OnixLabs.DependencyInjection.UnitTests;

// ReSharper disable once InconsistentNaming
public sealed class IServiceCollectionExtensionTests
{
    [Theory(DisplayName = "ServiceCollection.AddService<TService> should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddServiceTServiceShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        ServiceCollection services = [];

        // When
        services.AddService<Implementation>(lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(Implementation));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.False(serviceDescriptor.IsKeyedService);
    }

    [Theory(DisplayName = "ServiceCollection.AddKeyedService<TService> should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddKeyedServiceTServiceShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        const string serviceKey = "my-service-key";
        ServiceCollection services = [];

        // When
        services.AddKeyedService<Implementation>(serviceKey, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(Implementation));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.True(serviceDescriptor.IsKeyedService);
        Assert.Equal(serviceKey, serviceDescriptor.ServiceKey);
    }

    [Theory(DisplayName = "ServiceCollection.AddService<TService, TImplementation> should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddServiceTServiceTImplementationShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        ServiceCollection services = [];

        // When
        services.AddService<IAbstraction, Implementation>(lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAbstraction));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.False(serviceDescriptor.IsKeyedService);
        Assert.Equal(typeof(Implementation), serviceDescriptor.ImplementationType);
    }

    [Theory(DisplayName = "ServiceCollection.AddKeyedService<TService, TImplementation> should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddKeyedServiceTServiceTImplementationShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        const string serviceKey = "my-service-key";
        ServiceCollection services = [];

        // When
        services.AddKeyedService<IAbstraction, Implementation>(serviceKey, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAbstraction));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.True(serviceDescriptor.IsKeyedService);
        Assert.Equal(serviceKey, serviceDescriptor.ServiceKey);
        Assert.Equal(typeof(Implementation), serviceDescriptor.KeyedImplementationType);
    }

    [Theory(DisplayName = "ServiceCollection.AddService<TService> with factory should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddServiceWithFactoryShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        ServiceCollection services = [];

        // When
        services.AddService(Factory, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAbstraction));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.False(serviceDescriptor.IsKeyedService);
        Assert.Null(serviceDescriptor.ImplementationType);
        Assert.NotNull(serviceDescriptor.ImplementationFactory);
        return;

        IAbstraction Factory(IServiceProvider sp) => new Implementation();
    }

    [Theory(DisplayName = "ServiceCollection.AddKeyedService<TService> with factory should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddKeyedServiceWithFactoryShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        const string serviceKey = "my-service-key";
        ServiceCollection services = [];

        // When
        services.AddKeyedService(Factory, serviceKey, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAbstraction));

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.True(serviceDescriptor.IsKeyedService);
        Assert.Equal(serviceKey, serviceDescriptor.ServiceKey);
        Assert.Null(serviceDescriptor.KeyedImplementationType);
        Assert.NotNull(serviceDescriptor.KeyedImplementationFactory);
        return;

        IAbstraction Factory(IServiceProvider sp, object? key) => new Implementation();
    }

    [Theory(DisplayName = "ServiceCollection.AddService with Type parameters should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddServiceWithTypeParametersShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        ServiceCollection services = [];
        Type serviceType = typeof(IAbstraction);
        Type implementationType = typeof(Implementation);

        // When
        services.AddService(serviceType, implementationType, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == serviceType);

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.False(serviceDescriptor.IsKeyedService);
        Assert.Equal(implementationType, serviceDescriptor.ImplementationType);
    }

    [Theory(DisplayName = "ServiceCollection.AddKeyedService with Type parameters should produce the expected result")]
    [InlineData(ServiceLifetime.Singleton)]
    [InlineData(ServiceLifetime.Scoped)]
    [InlineData(ServiceLifetime.Transient)]
    public void ServiceCollectionAddKeyedServiceWithTypeParametersShouldProduceExpectedResult(ServiceLifetime lifetime)
    {
        // Given
        const string serviceKey = "my-service-key";
        ServiceCollection services = [];
        Type serviceType = typeof(IAbstraction);
        Type implementationType = typeof(Implementation);

        // When
        services.AddKeyedService(serviceType, implementationType, serviceKey, lifetime);

        // Then
        ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == serviceType);

        Assert.NotNull(serviceDescriptor);
        Assert.Equal(lifetime, serviceDescriptor.Lifetime);
        Assert.True(serviceDescriptor.IsKeyedService);
        Assert.Equal(serviceKey, serviceDescriptor.ServiceKey);
        Assert.Equal(implementationType, serviceDescriptor.KeyedImplementationType);
    }
}
