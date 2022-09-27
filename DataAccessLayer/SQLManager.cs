using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Odbc;

namespace EmployeeCRUDWebAPI.DataAccessLayer
{
    public class SQLManager
    {
        private IConfiguration _Configuration;
        public SQLManager(IConfiguration _configuration)
        {
            _Configuration = _configuration;
        }
        public  OdbcConnection GetConnection()
        {
            try
            {
                return new OdbcConnection(_Configuration.GetConnectionString("EmployeeDB"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ExecuteQuery(OdbcCommand cmd)
        {
            using (OdbcConnection conn = GetConnection())
            {
                try
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandTimeout = 120;

                    DataSet dsExecute = new DataSet();
                    
                    OdbcDataAdapter daExecute = new OdbcDataAdapter();
                    daExecute.SelectCommand = cmd;
                    daExecute.AcceptChangesDuringFill = false;
                    daExecute.Fill(dsExecute, "Data");

                    DataTable dt = dsExecute.Tables[0];

                    conn.Close();

                    return dsExecute;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public long ExecuteNonQueryAndScalar(OdbcCommand cmd)
        {
            try
            {
                using (OdbcConnection conn = GetConnection())
                {
                    OdbcTransaction tx = null;
                    cmd.Connection = conn;

                    OdbcCommand cmd1 = new OdbcCommand("SELECT @@IDENTITY; ");
                    cmd1.Connection = conn;
                    conn.Open();

                    tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    cmd.ExecuteNonQuery();
                    cmd1.Transaction = tx;

                    long id = Convert.ToInt64(cmd1.ExecuteScalar());

                    tx.Commit();

                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecuteNonQuery(OdbcCommand cmd)
        {
            using (OdbcConnection conn = GetConnection())
            {
                OdbcTransaction tx = null;
                cmd.Connection = conn;

                try
                {
                    conn.Open();
                    tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        tx.Rollback();
                        throw ex;
                    }
                    catch (Exception ex1)
                    {
                        throw ex1;
                    }
                }
            }
        }
    }
}
