using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace B2B_DASHBOARD.Models.Repository
{
    public class RepositoryMysql<T> : IRepository<T>
    {
        private DBContextMysql DB;
        public string SQL;
        private MySqlParameter[] Parameters;

        public RepositoryMysql(string SQL)
        {
           this.DB = new DBContextMysql("db_dashboard");
           this.SQL = SQL;
        }

        private MySqlParameter[] GetParamaters<T>(T item)
        {   //SE OBTIENE CON REFLECTION TODOS LAS PROPIEDADES PARA
            //CREAR LOS PARAMETROS
            PropertyInfo[] properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            MySqlParameter[] parametros = new MySqlParameter[properties.Length];
            int count = 0;
            foreach (PropertyInfo p in properties)
            {
                //PropertyInfo propInfo = properties.GetType().GetProperty(p.Name);
                string value = p.GetValue(item).ToString();
                parametros[count] =  new MySqlParameter(p.Name, value);
                count++;
            }
            return parametros;
        }

        public int Add(T Object)
        {
            int result = 0;
            MySqlParameter[] parameters = this.GetParamaters(Object);
            if (parameters.Length > 0)
            {
                result = this.DB.ExecuteProcedure(this.SQL, parameters);
            }
            return result;
        }   

        public int Delete(string id, string IdName)
        {
            MySqlParameter[] parameters = { new MySqlParameter(IdName, id) };
            int result = this.DB.ExecuteProcedure(this.SQL, parameters);
            return result;
        }

        public T Find<T>(string id, string IdName) where T: class,new()
        {
            MySqlParameter[] parameters = { new MySqlParameter(IdName, id) };
            DataTable table = this.DB.ExecProcedure(this.SQL,parameters);
            return table.toObject<T>();
        }

        public List<T> List<T>() where T:class,new()
        { 
            DataTable table = DB.ExecProcedure(this.SQL);
            return table.ToList<T>();
        }

        public List<T> ListByFilter<T>(string Filter, string FilterName) where T : class, new()
        {
            MySqlParameter[] parameters = { new MySqlParameter(Filter, FilterName) };
            DataTable table = DB.ExecProcedure(this.SQL);
            return table.ToList<T>();
        }

        public int Update(T Object)
        {
            if (Object != null)
            {
                MySqlParameter[] parameters = this.GetParamaters(Object);
                int result = this.DB.ExecuteProcedure(this.SQL,parameters);
                return result;
            }
            else
            {
                return 0;
            }
           
        }

    }
}