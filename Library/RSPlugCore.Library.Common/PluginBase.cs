using System;
using Autofac;

namespace RSPlugCore.Library.Common
{
    abstract class PluginBase : IPlugin
    {
        public PluginManifest Manifest { get; }

        protected PluginBase(PluginManifest manifest)
        {
            Manifest = manifest;
        }

        public virtual void Register(ContainerBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}