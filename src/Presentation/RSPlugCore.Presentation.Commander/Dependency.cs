using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RSPlugCore.Library.Common;
using RSPlugCore.Library.Services;

namespace RSPlugCore.Presentation.Commander
{
    public static class Dependency
    {
        public const string Version = "1.0.0";
        public static IServiceProvider Provider { get; private set; }
        public static IContainer Container { get; private set; }

        private static readonly ContainerBuilder ContainerBuilder = new ContainerBuilder();
        private static readonly PluginManager PluginManager = new PluginManager();
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized)
                return;

            var serviceCollection = new ServiceCollection();

            // add 
            serviceCollection.AddLogging();

            // populate
            ContainerBuilder.Populate(serviceCollection);

            // services 
            ContainerBuilder.RegisterType<MessageService>().As<IMessageService>();

            //  plugins
            foreach (var plugin in PluginManager.AvailablePlugins.Where(p => p.HostVersion.Equals(Version)).OrderBy(p => p.Order).ThenBy(p => p.Name))
            {
                PluginManager.RegisterPlugin(plugin, ContainerBuilder);
            }

            Container = ContainerBuilder.Build();
            Provider = new AutofacServiceProvider(Container);

            _isInitialized = true;
        }
    }
}
