using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;

namespace B2B_DASHBOARD.Models.Connection
{
    public class DBContextMysql
    {
        private MySqlConnectionStringBuilder cnString { get; set; }
        private MySqlConnection cn { get; set; }
        private MySqlCommand cmd { get; set; }
        private MySqlDataAdapter adapter { get; set; }
        private MySqlDataReader reader { get; set; }
        private DataTable table { get; set; }


        public DBContextMysql()
        {
            cnString = new MySqlConnectionStringBuilder();
            cnString.Server = ConfigurationManager.AppSettings["host"].ToString();
            cnString.UserID = ConfigurationManager.AppSettings["user"].ToString();
            cnString.Password = ConfigurationManager.AppSettings["password"].ToString();
            cnString.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["port"].ToString());
            cnString.Database = ConfigurationManager.AppSettings["db_b2b"].ToString();
            cnString.ConnectionTimeout = 1000;
        }




        public DBContextMysql(string DB)
        {
            cnString = new MySqlConnectionStringBuilder();
            cnString.Server = ConfigurationManager.AppSettings["host"].ToString();
            cnString.UserID = ConfigurationManager.AppSettings["user"].ToString();
            cnString.Password = ConfigurationManager.AppSettings["password"].ToString();
            cnString.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["port"].ToString());
            cnString.Database = ConfigurationManager.AppSettings[DB].ToString();
            cnString.ConnectionTimeout = 1000;
        }

        private MySqlConnection Connect()
        {
            try
            {
                cn = new MySqlConnection(cnString.ConnectionString);
                cn.Open();           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cn;
        }

        private async Task<MySqlConnection> ConnectAsync()
        {
            try
            {
                cn = new MySqlConnection(cnString.ConnectionString);
                await cn.OpenAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cn;
        }
        public DataTable ReadSql(string query)
        {
            using (Connect())
            {
                cmd = new MySqlCommand(query, cn);
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);
                return table;
            }
        }

        public async Task<DataTable> ReadSqlAsync(string query)
        {
            try
            {
                using (Connect())
                {
                    using (cmd = new MySqlCommand(query, cn))
                    {
                        var reader = await cmd.ExecuteReaderAsync();
                        table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public void ExecuteStatements(string query)
        {
            using (Connect())
            {
                cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ExecProcedure(string procedure,MySqlParameter[] parameters = null)
        {
            try
            {
                using (Connect())
                {
                    cmd = new MySqlCommand(procedure, cn);
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
                    adapter = new MySqlDataAdapter(cmd);
                    table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int ExecuteProcedure(string procedure, MySqlParameter[] parameters = null)
        {
            try
            {
                using (Connect())
                {
                    cmd = new MySqlCommand(procedure, cn);
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

                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<DataTable> ExecProcedureAsync(string procedure, MySqlParameter[] parameters = null)
        {
            try
            {
                using (ConnectAsync())
                {
                    cmd = new MySqlCommand(procedure, cn);
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
                    var reader = await cmd.ExecuteReaderAsync();
                    // adapter = new MySqlDataAdapter(cmd);
                    table = new DataTable();
                    table.Load(reader);
                   // adapter.Fill(table);
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