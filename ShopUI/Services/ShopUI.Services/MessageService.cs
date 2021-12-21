using ShopUI.Services.Interfaces;

namespace ShopUI.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
