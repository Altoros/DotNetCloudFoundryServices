using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;

namespace CloudFoundryServiceBroker
{
    /// <summary>
    /// Centralized module objects holder. Modules include all objects that contains business rules, equipment control managers and etc.
    /// </summary>
    public static class ModuleManager
    {
        #region Variables

        private static volatile object _syncRoot = new object();
        private static readonly Dictionary<Type, object> ModulesCollection = new Dictionary<Type, object>();

        #endregion

        /// <summary>
        /// Event that will be raised when module registration become updated
        /// </summary>
        public static event EventHandler<ModuleChangedEventArgs> ModuleRegistrationUpdated;

        /// <summary>
        /// Register new module within ModuleManager
        /// </summary>
        /// <typeparam name="T">Module type to register</typeparam>
        /// <param name="serviceInstance">Module instance</param>
        public static void Register<T>(T serviceInstance) where T : class
        {
            object old = null;
            lock (_syncRoot)
            {
                if (ModulesCollection.ContainsKey(typeof(T)))
                {
                    old = ModulesCollection[typeof(T)];
                    ModulesCollection[typeof(T)] = serviceInstance;
                }
                else
                {
                    ModulesCollection.Add(typeof(T), serviceInstance);
                }
            }
            var evt = ModuleRegistrationUpdated;
            if (evt != null)
            {
                evt(old, new ModuleChangedEventArgs(typeof(T), serviceInstance));
            }
        }

        /// <summary>
        /// Remove module registration
        /// </summary>
        /// <typeparam name="T">Module type to unregister</typeparam>
        /// <returns>True if registration was removed; False if module was not registered</returns>
        public static bool Unregister<T>() where T : class
        {
            lock (_syncRoot)
            {
                if (ModulesCollection.ContainsKey(typeof(T)))
                {
                    var module = ModulesCollection[typeof(T)];
                    ModulesCollection.Remove(typeof(T));
                    if (module != null && !RemotingServices.IsTransparentProxy(module) && module is IDisposable) ((IDisposable)module).Dispose();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Remove registration of all modules
        /// </summary>
        public static void UnregisterAll()
        {
            lock (_syncRoot)
            {
                foreach (var module in ModulesCollection.Where(module => module.Value != null))
                {
                    if (!RemotingServices.IsTransparentProxy(module.Value) && module.Value is IDisposable)
                        ((IDisposable)module.Value).Dispose();
                }
                ModulesCollection.Clear();
            }
        }

        /// <summary>
        /// Returns registered module by its type if any registered
        /// </summary>
        /// <typeparam name="T">Module type</typeparam>
        /// <returns>Return module instance</returns>
        public static T GetModule<T>() where T : class
        {
            lock (_syncRoot)
            {
                if (ModulesCollection.ContainsKey(typeof(T)))
                {
                    return ModulesCollection[typeof(T)] as T;
                }
                if (!IsShutdown)
                {
                    //tracer.FatalFormat("Requested module does not yet exists: {0}", typeof(T));
                    //Debug.Assert(false, "Requested module does not yet exists: "+typeof(T));
                }
                return default(T);
            }
        }

        /// <summary>
        /// Check is module already registered or not
        /// </summary>
        /// <typeparam name="T">Module type</typeparam>
        /// <returns></returns>
        public static bool GetIsSupported<T>() where T : class
        {
            lock (_syncRoot)
            {
                if (ModulesCollection.ContainsKey(typeof(T)))
                {
                    return ModulesCollection[typeof(T)] != null;
                }
                return false;
            }
        }

        /// <summary>
        /// Is shutdown initiated
        /// </summary>
        public static bool IsShutdown { get; set; }

        /// <summary>
        /// Return collection of all registered modules
        /// </summary>
        /// <returns></returns>
        internal static IList<object> GetModules()
        {
            lock (_syncRoot)
            {
                return ModulesCollection.Values.ToList();
            }
        }
    }
    /// <summary>
    /// Event argument for module registration updates
    /// </summary>
    public class ModuleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Type of module that was changed
        /// </summary>
        public Type ModuleType { get; private set; }
        /// <summary>
        /// New module instance
        /// </summary>
        public object Module { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        public ModuleChangedEventArgs(Type type, object instance)
        {
            ModuleType = type;
            Module = instance;
        }
    }
}
