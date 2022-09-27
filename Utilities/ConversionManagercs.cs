using System.Reflection;
using System.Data;
namespace EmployeeCRUDWebAPI.Utilities
{
    public static class ConversionManagercs
    {
        public static T ToObject<T>(this DataRow dr) where T : new()
        {
            T obj = new T();

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (dr[col] != DBNull.Value)
                {
                    PropertyInfo objproperty = obj.GetType().GetProperty(col.ColumnName);

                    if (objproperty != null)
                    {
                        object propertyValue = Convert.ChangeType(dr[col], objproperty.PropertyType);
                        objproperty.SetValue(obj, propertyValue, null);
                        continue;
                    }
                    else
                    {
                        FieldInfo objpropertyField = obj.GetType().GetField(col.ColumnName);
                       
                        if (objpropertyField != null)
                        {
                            object propertyValue = Convert.ChangeType(dr[col], objpropertyField.FieldType);
                            objpropertyField.SetValue(obj, propertyValue);
                        }
                    }
                }
            }
            return obj;
        }
    }
}
