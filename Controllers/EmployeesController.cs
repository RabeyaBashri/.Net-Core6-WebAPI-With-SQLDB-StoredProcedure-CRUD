using EmployeeCRUDWebAPI.BusinessAccessLayer;
using EmployeeCRUDWebAPI.BusinessAccessLayer.Interfaces;
using EmployeeCRUDWebAPI.BusinessAccessLayer.ServiceManager;
using EmployeeCRUDWebAPI.Models;
using EmployeeCRUDWebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeCRUDWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IManager iDeptManager;
        IManager iEmpManager;
        IParentChildManager iDeptEmpManager;
        private readonly IConfiguration _configuration;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            iDeptManager = new DepartmentManager(_configuration);
            iEmpManager = new EmployeeManager(_configuration);
            iDeptEmpManager = new EmployeeManager(_configuration);
        }

        [HttpGet("GetDepartments")]
        public string GetDepartments()
        {
            string JSONString = String.Empty;

            try
            {
                DataTable dt = iDeptManager.GetDataTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    JSONString = JsonConvert.SerializeObject(
                     new
                     {
                         returnStatus = "Success",
                         returnMsg = string.Empty,
                         data = JsonConvert.SerializeObject(dt),
                     });
                }
                else
                {
                    JSONString = JsonConvert.SerializeObject(
                    new
                    {
                        returnStatus = "Fail",
                        returnMsg = "No data found. Please try again later.",
                        data = string.Empty,
                    });
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                new
                {
                    returnStatus = "Fail",
                    returnMsg = ex.Message,
                    data = string.Empty,
                });
            }
            return JSONString;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{departmentID}/GetEmployeesByDepartment")]
        public string GetEmployeesByDepartment(long departmentID)
        {
            string JSONString = String.Empty;

            try
            {
                DataTable dt = iDeptEmpManager.GetDataTableByParentID(departmentID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    JSONString = JsonConvert.SerializeObject(
                     new
                     {
                         returnStatus = "Success",
                         returnMsg = string.Empty,
                         data = JsonConvert.SerializeObject(dt),
                     });
                }
                else
                {
                    JSONString = JsonConvert.SerializeObject(
                    new
                    {
                        returnStatus = "Fail",
                        returnMsg = "No data found. Please try again later.",
                        data = string.Empty,
                    });
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                new
                {
                    returnStatus = "Fail",
                    returnMsg = ex.Message,
                    data = string.Empty,
                });
            }

            return JSONString;
        }

        [HttpGet("{id}/GetEmployee")]
        public string GetEmployee(long id)
        {
            string JSONString = String.Empty;

            try
            {
                object obj = iEmpManager.Get(id);

                if (obj != null)
                {
                    Employee emp = (Employee)obj;
                    JSONString = JsonConvert.SerializeObject(
                     new
                     {
                         returnStatus = "Success",
                         returnMsg = string.Empty,
                         data = JsonConvert.SerializeObject(emp),
                     });
                }
                else
                {
                    JSONString = JsonConvert.SerializeObject(
                    new
                    {
                        returnStatus = "Fail",
                        returnMsg = "No data found. Please try again later.",
                        data = string.Empty,
                    });
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                new
                {
                    returnStatus = "Fail",
                    returnMsg = ex.Message,
                    data = string.Empty,
                });
            }

            return JSONString;
        }

        // POST api/<EmployeesController>
        [HttpPost("Create")]
        public string Create([FromBody] string jsonStringEmployeeObj)
        {
            string JSONString = String.Empty;
            try
            {
                if (!string.IsNullOrEmpty(jsonStringEmployeeObj))
                {
                    Employee? emp = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(jsonStringEmployeeObj);
                    if (emp != null)
                    {
                        iEmpManager.Save(emp, SaveType.Insert);
                        JSONString = JsonConvert.SerializeObject(
                         new
                         {
                             returnStatus = "Success",
                             returnMsg = string.Empty,
                             data = JsonConvert.SerializeObject(emp),
                         });
                    }
                    else
                    {
                        JSONString = JsonConvert.SerializeObject(
                        new
                        {
                            returnStatus = "Fail",
                            returnMsg = "Data not saved. Please try again later.",
                            data = string.Empty,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                   new
                   {
                       returnStatus = "Fail",
                       returnMsg = ex.Message,
                       data = string.Empty,
                   });
            }
            return JSONString;
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{emdID}/Update")]
        public string Update(int emdID, [FromBody] string jsonStringEmployeeObj)
        {
            string JSONString = String.Empty;
            try
            {
                if (!string.IsNullOrEmpty(jsonStringEmployeeObj) && emdID != 0)
                {
                    Employee? emp = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(jsonStringEmployeeObj);
                    if (emp != null && emp.ID == emdID)
                    {
                        iEmpManager.Save(emp, SaveType.Update);
                        JSONString = JsonConvert.SerializeObject(
                         new
                         {
                             returnStatus = "Success",
                             returnMsg = string.Empty,
                             data = JsonConvert.SerializeObject(emp),
                         });
                    }
                    else
                    {
                        JSONString = JsonConvert.SerializeObject(
                        new
                        {
                            returnStatus = "Fail",
                            returnMsg = "Data not saved. Please try again later.",
                            data = string.Empty,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                   new
                   {
                       returnStatus = "Fail",
                       returnMsg = ex.Message,
                       data = string.Empty,
                   });
            }
            return JSONString;
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{emdID}/Delete")]
        public string Delete(int emdID)
        {
            string JSONString = String.Empty;
            try
            {
                if (iEmpManager.Delete(emdID))
                {
                    JSONString = JsonConvert.SerializeObject(
                             new
                             {
                                 returnStatus = "Success",
                                 returnMsg = string.Empty
                             });
                }
                else
                {
                    JSONString = JsonConvert.SerializeObject(
                    new
                    {
                        returnStatus = "Fail",
                        returnMsg = "Data not deleted. Please try again later.",
                        data = string.Empty,
                    });
                }
            }
            catch (Exception ex)
            {
                JSONString = JsonConvert.SerializeObject(
                   new
                   {
                       returnStatus = "Fail",
                       returnMsg = ex.Message,
                       data = string.Empty,
                   });
            }
            return JSONString;
        }
    }
}
