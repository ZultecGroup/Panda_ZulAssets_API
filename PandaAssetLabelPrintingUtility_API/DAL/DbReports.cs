using System.Data.SqlClient;
using System.Data;

namespace PandaAssetLabelPrintingUtility_API.DAL
{
    public class DbReports
    {

        #region Declaration

        Connection con;
        DataTable dt;
        DataSet ds;
        SqlDataAdapter da;
        SqlCommand cmd;
        string query = string.Empty;

        #endregion

        #region With Parameters

        public DataTable DTWithParam(string storedProcedure, SqlParameter[] parameters, int connect)
        {
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                cmd = new SqlCommand();

                con = new Connection();
                using (sqlcon = con.GetDataBaseConnection())
                {
                    query = storedProcedure;
                    cmd = new SqlCommand(query, sqlcon);
                    int cmdTimeout = Convert.ToInt32(ConfigSettings.ConfigSettings_id(4));
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                DataTable dt2 = new DataTable();
                DataRow dtRow = dt2.NewRow();
                dt2.Columns.Add("ErrorMessage");
                dtRow["ErrorMessage"] = errorMessage;
                dt2.Rows.Add(dtRow);
                return dt2;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
        }

        public DataTable DTWithParamSecondDB(string storedProcedure, SqlParameter[] parameters, int connect)
        {
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                cmd = new SqlCommand();

                con = new Connection(connect);

                using (sqlcon = con.GetDataBaseConnection())
                {
                    query = storedProcedure;
                    cmd = new SqlCommand(query, sqlcon);
                    int cmdTimeout = Convert.ToInt32(ConfigSettings.ConfigSettings_id(4));
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                DataTable dt2 = new DataTable();
                DataRow dtRow = dt2.NewRow();
                dt2.Columns.Add("ErrorMessage");
                dtRow["ErrorMessage"] = errorMessage;
                dt2.Rows.Add(dtRow);
                return dt2;
            }
            finally
            {
                if (sqlcon != null)
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
            }
        }

        #endregion

    }
}
