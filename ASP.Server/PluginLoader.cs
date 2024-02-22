using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ASP.Server
{
    public class PluginLoader
    {
        private static List<IOutputApiPluginFormat> _plugins;

        public static IEnumerable<IOutputApiPluginFormat> Plugins => _plugins;

        public static void LoadPlugins(string path)
        {
            if (_plugins == null)
            {
                _plugins = new List<IOutputApiPluginFormat>();
                foreach (var dll in Directory.GetFiles(path, "*.dll"))
                {
                    var assembly = Assembly.LoadFrom(dll);
                    var types = assembly.GetTypes()
                        .Where(t => typeof(IOutputApiPluginFormat).IsAssignableFrom(t) && !t.IsInterface &&
                                    !t.IsAbstract);

                    foreach (var type in types)
                    {
                        if (Activator.CreateInstance(type) is IOutputApiPluginFormat plugin)
                        {
                            _plugins.Add(plugin);
                        }
                    }
                }
            }
        }
    }
}