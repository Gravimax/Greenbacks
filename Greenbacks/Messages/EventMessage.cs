namespace Greenbacks.Messages
{
    public class EventMessage
    {
        public EventMessage(string message, bool isError)
        {
            Message = message;
            IsError = isError;
        }

        public string Message;
        public bool IsError;
    }
}
