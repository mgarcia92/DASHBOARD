using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace B2B_DASHBOARD.Models.Connection
{
    public class DBContextMSSQL
    {
        private SqlConnectionStringBuilder cnString { get; set; }
        private SqlConnection cn { get; set; }
        private SqlCommand cmd { get; set; }
        private SqlDataAdapter adapter { get; set; }
        private SqlDataReader reader { get; set; }
        private DataTable table { get; set; }

        public DBContextMSSQL()
        {
            cnString = new SqlConnectionStringBuilder();
            cnString.DataSource = ConfigurationManager.AppSettings["hostXsales"].ToString();
            cnString.UserID = ConfigurationManager.AppSettings["userXsales"].ToString();
            cnString.Password = ConfigurationManager.AppSettings["passwordXsales"].ToString();
            cnString.InitialCatalog = ConfigurationManager.AppSettings["dbXsales"].ToString();
            cnString.ConnectTimeout = 10000;
        }

        private SqlConnection Connect()
        {
            try
            {
                cn = new SqlConnection(cnString.ConnectionString);
                cn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cn;
        }

        private SqlConnection ConnectAsync()
        {
            try
            {
                cn = new SqlConnection(cnString.ConnectionString);
                cn.OpenAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cn;
        }


        public DataTable ReadSql(string query)
        {
            try
            {
                using (Connect())
                {
                    table = new DataTable();
                    cmd = new SqlCommand(query, cn);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (SqlException sq)
            {

                throw;
            }
           
        }

        public async Task<DataTable> ReadSqlAsync(string query)
        {
            try
            {
                using (Connect())
                {
                    using (cmd = new SqlCommand(query, cn))
                    {
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void ExecuteStatements(string query)
        {
            using (Connect())
            {
                cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ExecProcedure(string procedure, SqlParameter[] parameters = null)
        {
            using (Connect())
            {
                cmd = new SqlCommand(procedure, cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters.Length > 0)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                }

                adapter = new SqlDataAdapter(cmd);
                table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public async Task<DataTable> ExecProcedureAsync(string procedure, SqlParameter[] parameters = null)
        {
            try
            {
                using (ConnectAsync())
                {
                    cmd = new SqlCommand(procedure, cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        if (parameters.Length > 0)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                            }
                        }
                    }

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}