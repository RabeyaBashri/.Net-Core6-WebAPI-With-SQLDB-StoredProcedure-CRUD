using EmployeeCRUDWebAPI.DataAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.Models;
using EmployeeCRUDWebAPI.Utilities;
using System.Data;
using System.Data.Odbc;

namespace EmployeeCRUDWebAPI.DataAccessLayer.ServiceDataMapper
{
    public class EmployeeDataMapper : IDataMapper, IParentChildDataMapper
    {
        SQLManager sqlManager;
        public EmployeeDataMapper(IConfiguration _configuration)
        {
            sqlManager = new SQLManager(_configuration);
        }
        public static string getAllByDepartmentID = @"EXECUTE [dbo].[GetAllEmployeeByDepartment] @DepartmentID = ?";
        public static string save = @"EXECUTE [dbo].[SaveEmployee] 
                                       @ID = ?
                                      ,@Name = ?
                                      ,@HiredOn = ?
                                      ,@DOB = ?
                                      ,@Email = ?
                                      ,@ContactNo = ?
                                      ,@Address = ?
                                      ,@Position = ?
                                      ,@DepartmentID = ?
                                      ,@SaveType = ?";
        public static string delete = @"EXECUTE [dbo].[DeleteEmployee] @ID = ?";
        public static string get = @"EXECUTE [dbo].[GetEmployee] @ID = ?";
        public bool Delete(long id)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(delete);

                cmd.Parameters.AddWithValue("@ID", id);

                sqlManager.ExecuteNonQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;   
        }
        public object Get(long id)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(get);
                cmd.Parameters.AddWithValue("@ID", id);
                DataSet ds = sqlManager.ExecuteQuery(cmd);

                if(ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt != null && dt.Rows != null && dt.Rows.Count == 1)
                    {
                        return dt.Rows[0].ToObject<Employee>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        public List<object> GetList()
        {
            throw new NotImplementedException();
        }
        public object Save(object obj, SaveType saveType)
        {
            Employee emp;
            
            try
            {
                if (obj != null)
                {
                    emp = (Employee)obj;
                    
                    OdbcCommand cmd = new OdbcCommand(save);
                    cmd.Parameters.AddWithValue("@ID", emp.ID);
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@HiredOn", emp.HiredOn);
                    cmd.Parameters.AddWithValue("@DOB", emp.DOB);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@ContactNo", emp.ContactNo);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@Position", emp.Position);
                    cmd.Parameters.AddWithValue("@DepartmentID", emp.DepartmentID);
                    cmd.Parameters.AddWithValue("@SaveType", saveType.ToString());

                    if (saveType == SaveType.Insert)
                    {
                        emp.ID = Convert.ToInt64(sqlManager.ExecuteNonQueryAndScalar(cmd));
                    }
                    else if (saveType == SaveType.Update)
                    {
                        sqlManager.ExecuteNonQuery(cmd);
                    }

                    return emp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        public DataTable GetDataTable()
        {
            throw new NotImplementedException();
        }
        public List<object> GetListByParentID(long departmentID)
        {
            throw new NotImplementedException();
        }
        public DataTable GetDataTableByParentID(long departmentID)
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(getAllByDepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

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
