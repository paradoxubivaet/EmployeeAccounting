namespace EmployeeAccounting.Messages
{
    public class UpdateMessage
    {
        public bool Flag { get; }
        public UpdateMessage(bool flag)
        {
            Flag = flag;
        }
    }
}
