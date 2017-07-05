using System;
using System.Collections.Generic;
using System.Text;
using RSPlugCore.Library.Services;

namespace RSPlugCore.Plugins.SamplePlugin.Services
{
    public class MessageService : IMessageService
    {
        public string Greeting => "Sample Plugin Greeting";
    }
}
