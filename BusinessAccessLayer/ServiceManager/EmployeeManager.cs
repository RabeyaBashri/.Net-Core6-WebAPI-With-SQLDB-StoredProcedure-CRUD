using EmployeeCRUDWebAPI.BusinessAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.DataAccessLayer;
using EmployeeCRUDWebAPI.DataAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.DataAccessLayer.ServiceDataMapper;
using EmployeeCRUDWebAPI.Utilities;
using System.Data;

namespace EmployeeCRUDWebAPI.BusinessAccessLayer.ServiceManager
{
    public class EmployeeManager : IManager, IParentChildManager
    {
        IDataMapper dataMapper;
        IParentChildDataMapper iDeptEmpDataMapper;
        public EmployeeManager(IConfiguration configuration)
        {
            dataMapper = new EmployeeDataMapper(configuration);
            iDeptEmpDataMapper = new EmployeeDataMapper(configuration);
        }
        public bool Delete(long id)
        {
            return dataMapper.Delete(id);
        }
        public object Get(long id)
        {
            return dataMapper.Get(id);
        }
        public List<object> GetList()
        {
            return dataMapper.GetList();
        }
        public List<object> GetListByParentID(long departmentID)
        {
            throw new NotImplementedException();
        }
        public DataTable GetDataTable()
        {
            throw new NotImplementedException();
        }
        public DataTable GetDataTableByParentID(long departmentID)
        {
            return iDeptEmpDataMapper.GetDataTableByParentID(departmentID);
        }
        public object Save(object obj, SaveType saveType)
        {
            return dataMapper.Save(obj, saveType);
        }
    }
}
