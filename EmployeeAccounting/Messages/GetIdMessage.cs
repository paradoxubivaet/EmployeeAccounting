namespace EmployeeAccounting.Messages
{
    public class GetIdMessage
    {
        public int Id { get; }
        public GetIdMessage(int id)
        {
            Id = id;
        }
    }
}
