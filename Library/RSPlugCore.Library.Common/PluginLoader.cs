using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace RSPlugCore.Library.Common
{
    public interface IPluginLoader
    {
        Assembly Load(PluginManifest plugin);
    }

    public class PluginLoader : IPluginLoader
    {


        public Assembly Load(PluginManifest plugin)
        {
            var assemblyName = AssemblyLoadContext.GetAssemblyName(plugin.Filename);
            var dependencyContext = DependencyContext.Default;
            var ressource = dependencyContext.CompileLibraries.FirstOrDefault(r => r.Name.Contains(assemblyName.Name));

            if (ressource != null)
            {
                return Assembly.Load(new AssemblyName(ressource.Name));
            }

            return AssemblyLoadContext.Default.LoadFromAssemblyPath(plugin.Filename);
        }

    }
}
