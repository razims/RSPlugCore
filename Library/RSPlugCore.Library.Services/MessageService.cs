using System;

namespace RSPlugCore.Library.Services
{
    public interface IMessageService
    {
        string Greeting { get; }
    }

    public class MessageService : IMessageService
    {
        public string Greeting => "Shared Greeting";
    }
}
