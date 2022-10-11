using System.Data;
using System.Data.SqlClient;

namespace AD_Manager.Layers.DAL
{
    public interface IDatabaseHelper
    {
        SqlConnection GetConnection();
        DataTable? ExecuteQuery(string cmd, out string message);
        DataTable? ExecuteQuery(string cmd, out string message, params SqlParameter[]? parameters);
        int? ExequteNonQuery(string cmd, out string message);
        int? ExequteNonQuery(string cmd, out string message, params SqlParameter[]? parameters);
    }
}
