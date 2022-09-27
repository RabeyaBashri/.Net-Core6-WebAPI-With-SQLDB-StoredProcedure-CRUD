namespace EmployeeCRUDWebAPI.Models
{
    public class Department
    {
        long _ID;
        string _Name;
        public long ID { get => _ID; set => _ID = value; }
        public string Name { get => _Name; set => _Name = value; }
    }
}
