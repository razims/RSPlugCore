using System;
using Autofac;

namespace RSPlugCore.Library.Common
{
    /// <summary>
    /// Basic Plugin Interface
    /// </summary>
    public interface IPlugin
    {
        void Register(ContainerBuilder builder);
    }
}
