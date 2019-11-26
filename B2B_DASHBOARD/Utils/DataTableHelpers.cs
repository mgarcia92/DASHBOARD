using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace B2B_DASHBOARD.Utils
{
    public static class DataTableHelpers
    {
        //METODO DE EXTENCION PARA CONVERTIR UN UN DATAROW A UN OBJETO.
        public static T toObject<T>(this DataTable table) where T:class, new()
        {
            T obj = new T();
            if (table.Rows.Count > 0)
            {
                DataRow data = table.Rows[0];

                //OBTENER LAS PROPIEDADES CON REFLECTION...
                PropertyInfo[] propertys = obj.GetType().GetProperties();

                foreach (var prop in propertys)
                {
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.Name.Contains("Nulleable"))
                    {
                        if (!string.IsNullOrEmpty(data[prop.Name].ToString()))
                        {
                            prop.SetValue(obj, Convert.ChangeType(data[prop.Name], Nullable.GetUnderlyingType(prop.PropertyType), null));
                        }
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(data[prop.Name], prop.PropertyType), null);
                    }
                }
                return obj;
            }
            else
                return null;
        }
        //METODO PARA CONVERTIR DE DATATABLE A UNA LISTA DE OBJETOS.
        public static List<T> ToList<T>(this DataTable data) where T : class, new()
        {
            List<T> list = new List<T>();
            var d = data.AsEnumerable().ToList<object>();
            foreach (var item in data.AsEnumerable())
            {
                //var obj = data.toObject<T>();

                T obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propInfo = obj.GetType().GetProperty(prop.Name);
                        propInfo.SetValue(obj, Convert.ChangeType(item[prop.Name], propInfo.PropertyType), null);
                    }
                    catch 
                    {
                        continue;
                    }
                }

                list.Add(obj);
            }

            return list;
        }
    }
}