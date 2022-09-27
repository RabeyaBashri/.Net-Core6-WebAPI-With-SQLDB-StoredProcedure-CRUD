using System.Data;

namespace EmployeeCRUDWebAPI.Utilities
{
    public interface ICRUD
    {
        List<object> GetList();
        DataTable GetDataTable();
        object Get(long id);
        object Save(object obj, SaveType saveType);
        bool Delete(long id);
    }
}
