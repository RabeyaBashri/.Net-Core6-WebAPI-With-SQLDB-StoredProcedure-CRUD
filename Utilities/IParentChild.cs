using System.Data;

namespace EmployeeCRUDWebAPI.Utilities
{
    public interface IParentChild
    {
        List<object> GetListByParentID(long parentID);
        DataTable GetDataTableByParentID(long parentID);
    }
}
