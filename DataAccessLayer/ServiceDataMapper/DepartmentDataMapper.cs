using EmployeeCRUDWebAPI.DataAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.Utilities;
using System.Data;
using System.Data.Odbc;

namespace EmployeeCRUDWebAPI.DataAccessLayer.ServiceDataMapper
{
    public class DepartmentDataMapper : IDataMapper
    {
        SQLManager sqlManager;
        public static string getAll = @"EXECUTE  [dbo].[GetAllDepartments]";
        public DepartmentDataMapper(IConfiguration configuration)
        {
            sqlManager = new SQLManager(configuration);
        }
        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
        public object Get(long id)
        {
            throw new NotImplementedException();
        }
        public List<object> GetList()
        {
            throw new NotImplementedException();
        }
        public object Save(object obj, SaveType saveType)
        {
            throw new NotImplementedException();
        }
        public DataTable GetDataTable()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(getAll);

                DataSet ds = sqlManager.ExecuteQuery(cmd);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
