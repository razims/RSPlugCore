using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Newtonsoft.Json;

namespace RSPlugCore.Library.Common
{
    public interface IPluginManager
    {
        List<PluginManifest> AvailablePlugins { get; }
        void RegisterPlugin(PluginManifest asm, ContainerBuilder builder);
    }

    public class PluginManager : IPluginManager
    {
        protected PluginLoader PluginLoader = new PluginLoader();
        public List<PluginManifest> AvailablePlugins => PluginsManifests.Select(LoadFromManifest).ToList();

        public PluginManager()
        {
            if (!Directory.Exists(PluginsDirectory))
                Directory.CreateDirectory(PluginsDirectory);
        }


        /// <summary>
        /// Register Plugin Entry Point
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="builder"></param>
        public void RegisterPlugin(PluginManifest plugin, ContainerBuilder builder)
        {
            var asm = PluginLoader.Load(plugin);

            var entries = asm.GetTypes().Where(t => t.IsAssignableTo<IPlugin>()).ToList();

            if (entries.Count > 1)
                throw new Exception("More than 1 entry points were found");

            if (!entries.Any())
                throw new Exception("No entry point was found");

            var type = entries[0];
            var plugInstance = Activator.CreateInstance(type) as IPlugin;

            plugInstance?.Register(builder);

        }

        #region Helpers 
        protected PluginManifest LoadFromManifest(string pluginsManifest)
        {
            var directory = Path.GetDirectoryName(pluginsManifest) + Path.DirectorySeparatorChar;
            var options = ParseOptions(pluginsManifest);

            options.Filename = directory + options.Filename;
            return options;
        }

        protected PluginManifest ParseOptions(string manifestFileName)
        {
            var raw = File.ReadAllText(manifestFileName);
            return JsonConvert.DeserializeObject<PluginManifest>(raw);
        }

        protected List<string> PluginsManifests => Directory.GetFiles(PluginsDirectory, "manifest.json", SearchOption.AllDirectories).ToList();
        protected string PluginsDirectory => AssemblyDirectory + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar;

        protected string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


        #endregion
    }
}
