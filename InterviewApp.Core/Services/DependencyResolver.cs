using System;
using System.Collections.Generic;
using System.IO;

namespace InterviewApp.Core.Services;

public class DependencyResolver
{
    public static DependencyResolver Instance { get; } = new();

    private readonly Dictionary<Type, object> _serviceDictionary;

    private DependencyResolver()
    {
        _serviceDictionary = new Dictionary<Type, object>();
    }

    public T Resolve<T>() where T : class
    {
        var isExits = _serviceDictionary.TryGetValue(typeof(T), out var service);
        if (isExits is false)
            throw new ArgumentNullException();
        return (service as T)!;
    }

    public void Register<T>(T type) where T : class
    {
        var isExits = _serviceDictionary.TryGetValue(typeof(T), out _);
        if (isExits)
            throw new Exception("Service already exits");

        _serviceDictionary.Add(typeof(T), type);
    }
}