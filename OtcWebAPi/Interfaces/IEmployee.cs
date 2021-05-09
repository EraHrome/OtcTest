namespace OtcWebAPi.Interfaces
{
    public interface IEmployee
    {

        string Name { get; set; }

        decimal Salary { get; set; }

        int DepartmentId { get; set; }

    }
}
