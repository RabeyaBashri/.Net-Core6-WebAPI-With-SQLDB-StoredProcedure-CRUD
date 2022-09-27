using EmployeeCRUDWebAPI.BusinessAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.DataAccessLayer;
using EmployeeCRUDWebAPI.DataAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.DataAccessLayer.ServiceDataMapper;
using EmployeeCRUDWebAPI.Utilities;
using System.Data;

namespace EmployeeCRUDWebAPI.BusinessAccessLayer.ServiceManager
{
    public class DepartmentManager : IManager
    {
        IDataMapper dataMapper;
        public DepartmentManager(IConfiguration configuration)
        {
            dataMapper = new DepartmentDataMapper(configuration);
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
            try
            {
                return dataMapper.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetDataTable()
        {
            try
            {
                return dataMapper.GetDataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object Save(object obj, SaveType saveType)
        {
            throw new NotImplementedException();
        }
    }
}
