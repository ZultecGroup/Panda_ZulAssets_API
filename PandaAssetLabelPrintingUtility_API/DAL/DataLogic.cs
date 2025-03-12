using System.Data.SqlClient;
using System.Data;
using static PandaAssetLabelPrintingUtility_API.BAL.RequestParameters;
using System.Reflection.Emit;

namespace PandaAssetLabelPrintingUtility_API.DAL
{
    public class DataLogic
    {

        #region Login & Password Related Logics

        #region LoginDetails

        public static DataTable LoginDetails(Loginparam loginParam, string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter ("@Email", loginParam.Email),
                new SqlParameter ("@Pass", loginParam.Password)
            };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region UpdateRefreshToken

        public static DataTable UpdateRefreshToken(string email, string refreshToken, string StoredProcedure)
        {
            try
            {
                DbReports CGD = new DbReports();
                SqlParameter[] sqlParameters = {
                    new SqlParameter ("@Email", email),
                    new SqlParameter ("@RefreshToken",refreshToken)
                };

                DataTable dt = CGD.DTWithParam(StoredProcedure, sqlParameters, 1);
                return dt;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                return new DataTable(errorMessage);
            }
        }

        #endregion

        #endregion

        #region Users

        #region Get All Users

        public static DataTable GetAllUsers(string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            { };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Check Email Exists

        public static DataTable CheckEmailExists(string email, string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter ("@Email", email)
            };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Update Passwords to Default

        public static DataTable UpdatePasswordToDefault(string email, string Password, string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter ("@Password", Password),
                new SqlParameter ("@Email", email)
            };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #endregion

        #region Get Assets

        public static DataTable GetAssets(AssetsReqParams assetsReqParams, string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter ("@LocID", assetsReqParams.LocID),
                new SqlParameter ("@AstCatID", assetsReqParams.AstCatID),
                new SqlParameter ("@Barcode", assetsReqParams.Barcode)
            };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Get Locations

        public static DataTable GetLocations(string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            { };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Get Categories

        public static DataTable GetCategories(string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            { };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Get Label Info

        public static DataTable GetLabelInfo(string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            { };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

        #region Verify Old Password

        public static DataTable VerifyOldPassword(ChangePassword changePassword, string StoreProcedure)
        {
            DbReports CGD = new DbReports();
            SqlParameter[] sqlParameters =
            {
                new SqlParameter ("@Email", changePassword.EmailAddress),
                new SqlParameter ("@OldPassword", changePassword.OldPassword),
                new SqlParameter ("@NewPassword", changePassword.NewPassword)
            };
            return CGD.DTWithParam(StoreProcedure, sqlParameters, 1);
        }

        #endregion

    }
}
