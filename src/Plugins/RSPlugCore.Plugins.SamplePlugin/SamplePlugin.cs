using System;
using Autofac;
using RSPlugCore.Library.Common;
using RSPlugCore.Library.Services;
using MessageService = RSPlugCore.Plugins.SamplePlugin.Services.MessageService;

namespace RSPlugCore.Plugins.SamplePlugin
{
    public class SamplePlugin : IPlugin
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<MessageService>().As<IMessageService>();
        }
    }
}
