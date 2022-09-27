namespace EmployeeCRUDWebAPI.Models
{
    public class Employee
    {
        public Employee()
        {
        }

        long _ID;
        string _Name;
        DateTime _HiredOn;
        DateTime _DOB;
        string _Email;
        string _Address;
        string _Position;
        long _ContactNo;
        Department _Department;
        long _DepartmentID;
        public long ID { get => _ID; set => _ID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public DateTime HiredOn
        {
            get => _HiredOn.Date;
            set => _HiredOn = value;
        }
        public DateTime DOB
        {
            get => _DOB.Date;
            set => _DOB = value;
        }
        public string Email { get => _Email; set => _Email = value; }
        public string Address { get => _Address; set => _Address = value; }
        public string Position { get => _Position; set => _Position = value; }
        public long ContactNo { get => _ContactNo; set => _ContactNo = value; }
        public long DepartmentID
        {
            get
            {
                _DepartmentID = (_Department != null) ? _Department.ID : 0;
                return _DepartmentID;
            }
            set
            {
                _DepartmentID = value;
                _Department = new Department();
                _Department.ID = _DepartmentID;
            }
        }
        public Department Department { get => _Department; set => _Department = value; }
    }
}
