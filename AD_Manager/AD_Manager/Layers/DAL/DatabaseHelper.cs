using System.Data;
using System.Data.SqlClient;

namespace AD_Manager.Layers.DAL
{
    public class DatabaseHelper: IDatabaseHelper
    {
        private readonly string ConnectionString;
        //private readonly string UserName;
        //private readonly string Password;
        private readonly ILogger _logger;
        public DatabaseHelper(IConfiguration config, ILogger<DatabaseHelper> logger)
        {
            ConnectionString = config.GetConnectionString("DBConnectionString");
            //UserName = config.GetConnectionString("DBUserName");
            //Password = config.GetConnectionString("DBPassword");
            _logger = logger;
        }

        private string GetConnectionString()
        {
            return ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            SqlConnection sqlConnection;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                //sqlConnection.Open();
                return sqlConnection;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "GetConnection");
                throw new Exception(ex.Message);
            }
        }

        public DataTable? ExecuteQuery(string cmd, out string message)
        {
            return ExecuteQuery(cmd, out message, null);
        }

        public DataTable? ExecuteQuery(string cmd, out string message, params SqlParameter[]? parameters)
        {
            try
            {
                message = string.Empty;
                using var sql = GetConnection();
                using SqlCommand sqlCommand = new(cmd, sql);
                if (parameters != null)
                    sqlCommand.Parameters.AddRange(parameters);
                using SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                using DataTable dataTable = new();
                sql.Open();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }catch(Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, "ExecuteQuery");
                return null;
            }
        }
        public int? ExequteNonQuery(string cmd, out string message)
        {
            return ExequteNonQuery(cmd, out message, null);
        }

        public int? ExequteNonQuery(string cmd, out string message, params SqlParameter[]? parameters)
        {
            try
            {
                message = string.Empty;
                using var sql = GetConnection();
                using SqlCommand sqlCommand = new(cmd, sql);
                if (parameters != null)
                    sqlCommand.Parameters.AddRange(parameters);
                sql.Open();
                return sqlCommand.ExecuteNonQuery();
            }catch(Exception ex)
            {
                message = ex.Message;
                _logger.LogError(ex, "ExequteNonQuery");
                return null;
            }
        }

    }
}
