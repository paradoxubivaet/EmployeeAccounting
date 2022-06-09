namespace EmployeeAccounting.Messages
{
    public class NotificationClosingMessage
    {
        public bool IsClosing { get; }
        public NotificationClosingMessage()
        {
            IsClosing = true;
        }
    }
}
