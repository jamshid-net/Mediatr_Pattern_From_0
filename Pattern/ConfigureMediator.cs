using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pattern.Implements;
using Pattern.Interfaces;
using System.Collections.Concurrent;
using System.Reflection;

namespace Pattern;
public static class ConfigureMediator
{
    public static IServiceCollection AddCustomMediatr(this IServiceCollection services, ServiceLifetime serviceLifetime, params Assembly[] assemblies)
    {
        var handlerInfo = new ConcurrentDictionary<Type, Type>();
        var interfaceName = typeof(IRequestHandler<,>).Name;

        foreach (var assembly in assemblies)
        {

            var irequests = GetClassesImplement(assembly, typeof(IRequest<>));
            var irequestHandlers = GetClassesImplement(assembly, typeof(IRequestHandler<,>));


            irequests.ForEach(request =>
            {
                irequestHandlers.ForEach(handler =>
                {
                    if (handler.GetInterface(interfaceName)!.GetGenericArguments().First() == request)
                    {
                        handlerInfo[request] = handler;
                    }
                });
            });

            var serviceDescriptor = irequestHandlers.Select(x => new ServiceDescriptor(x, x, serviceLifetime));

            services.TryAdd(serviceDescriptor);

        }

        services.AddSingleton<IMediatr>(x => new Mediatr(x.GetRequiredService, handlerInfo));

        return services;
    }

    private static List<Type> GetClassesImplement(Assembly assembly, Type typetoMatch)
    {
        return assembly.ExportedTypes.Where(type =>
        {
            var implementRequestType = type.GetInterfaces()
                                           .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typetoMatch)
                                           .ToArray();

            return !type.IsInterface && !type.IsAbstract && implementRequestType.Length != 0;
        }).ToList();
    }

    #region Singleton_Scoped_Transient
    public static IServiceCollection AddCustomMediatrSingleton(this IServiceCollection services, params Assembly[] assemblies)
       => services.AddCustomMediatr(ServiceLifetime.Singleton, assemblies);

    public static IServiceCollection AddCustomMediatrScoped(this IServiceCollection services, params Assembly[] assemblies)
       => services.AddCustomMediatr(ServiceLifetime.Scoped, assemblies);

    public static IServiceCollection AddCustomMediatrTransient(this IServiceCollection services, params Assembly[] assemblies)
       => services.AddCustomMediatr(ServiceLifetime.Transient, assemblies);
    #endregion
}
