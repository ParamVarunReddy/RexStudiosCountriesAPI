using Microsoft.Extensions.Logging;
using RexStudios.DynamicsCRM.Actions.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RexStudios.DynamicsCRM.Actions.Common
{
    /// <summary>
    /// Class to instantiate to service objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ServiceFactory<T>
    {
        /// <summary>
        /// Method to create the instance of a service 
        /// </summary>
        /// <param name="apiOptions"></param>
        /// <param name="httpService"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static T Instance(ApiOptions apiOptions, IHttpService httpService, ILogger logger)
        {
            object[] arr = { apiOptions, httpService, logger };

            return SingleTon.Instance.GetT<T>(arr);
        }
    }

    /// <summary>
    /// Singleton class to hold the instances
    /// </summary>
    sealed class SingleTon
    {
        static SingleTon _singleTon = null;
        static readonly object _lockObject = new object();
        private readonly IDictionary<string, object> _implementations = new Dictionary<string, object>();
        private SingleTon()
        {

        }

        private static T GetInstance<T>(object[] ctorParams)
        {

            foreach (var type in typeof(T).Assembly.GetTypes())
            {
                if (!type.IsAbstract && typeof(T).IsAssignableFrom(type))
                {
                    return (T)Activator.CreateInstance(type, ctorParams);
                }
            }

            return default;
        }

        /// <summary>
        /// Method to get instance if not exists create it and get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prms"></param>
        /// <returns></returns>
        internal T GetT<T>(object[] prms)
        {
            if (_implementations.ContainsKey(typeof(T).Name))
            {
                return (T)_implementations[typeof(T).Name];
            }

            T instance = GetInstance<T>(prms);
            if (instance != null && !_implementations.ContainsKey(typeof(T).Name))
            {
                _implementations.Add(typeof(T).Name, instance);
                return instance;
            }
            else
            {
                throw new NotImplementedException($"Could not find the implemetation type for {typeof(T).Name}");
            }
        }

        internal static SingleTon Instance
        {
            get
            {
                if (_singleTon == null)
                {
                    lock (_lockObject)
                    {
                        if (_singleTon == null)
                        {
                            _singleTon = new SingleTon();
                        }
                    }
                }

                return _singleTon;
            }
        }
    }
}
