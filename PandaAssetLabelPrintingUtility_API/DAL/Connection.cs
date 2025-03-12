using System.Data.SqlClient;

namespace PandaAssetLabelPrintingUtility_API.DAL
{
    public class Connection
    {

        string constr = null;
        public Connection()
        {
            constr = ConfigSettings.conStr1;
        }
        public Connection(int i)
        {
            constr = ConfigSettings.ConfigSettings_id(i);
        }
        public SqlConnection GetDataBaseConnection()
        {
            try
            {
                //EncryptDecryptPassword EDP = new EncryptDecryptPassword();
                //Data Source=S1ZHXDm7B4g=;Initial Catalog=QjYdDZ+F9E8=;User ID=SDnVsKemi7s=;Password=QjYdDZ+F9E8=;
                //string EncryptServerName = string.Empty;
                //string EncryptDataBase = string.Empty;
                //string EncryptUserId = string.Empty;
                //string EncryptPassword = string.Empty;

                //string DecryptServerName = string.Empty;
                //string DecryptDataBase = string.Empty;
                //string DecryptUserId = string.Empty;
                //string DecryptPassword = string.Empty;
                ////var b = encryption.Encrypt("DESKTOP-PISB67F\\SQLEXPRESS");
                //EncryptServerName = constr.Between("Data Source=", ";Initial");
                //DecryptServerName = EncryptDecryptPassword.Decrypt(EncryptServerName, "r0b1nr0y");
                //EncryptDataBase = constr.Between("Initial Catalog=", ";User ID");
                ////var a = encryption.Encrypt(EncryptDataBase);
                //DecryptDataBase = EncryptDecryptPassword.Decrypt(EncryptDataBase, "r0b1nr0y");
                //EncryptUserId = constr.Between("User ID=", ";Password");
                //DecryptUserId = EncryptDecryptPassword.Decrypt(EncryptUserId, "r0b1nr0y");
                //EncryptPassword = constr.After("Password=");
                //EncryptPassword = EncryptPassword.Remove(EncryptPassword.Length - 1, 1);
                //DecryptPassword = EncryptDecryptPassword.Decrypt(EncryptPassword, "r0b1nr0y");

                //constr = constr.Replace(EncryptServerName, DecryptServerName);
                //constr = constr.Replace(EncryptDataBase, DecryptDataBase);
                //constr = constr.Replace(EncryptUserId, DecryptUserId);
                //constr = constr.Replace(EncryptPassword, DecryptPassword);

                SqlConnection sqlcon = new SqlConnection();
                sqlcon.ConnectionString = constr;
                sqlcon.Open();
                return sqlcon;
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return null;
            }

        }

    }
}
