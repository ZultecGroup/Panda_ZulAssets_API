using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PandaAssetLabelPrintingUtility_API.DAL;
using PandaAssetLabelPrintingUtility_API.Services;
using static PandaAssetLabelPrintingUtility_API.BAL.RequestParameters;
using System.Data;
using static PandaAssetLabelPrintingUtility_API.BAL.ResponseParameters;

namespace PandaAssetLabelPrintingUtility_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region Declarations

        public readonly static string SP_UserLogin = "[dbo].[SP_UserLogin]";
        public readonly static string SP_VerifyOldPassword = "[dbo].[SP_NewPasswordwithVerification]";
        public readonly static string UpdateRefreshToken_SP = "[dbo].[UpdateRefreshToken]";
        public readonly static string SP_GetAllUsers = "[dbo].[SP_GetAllUsers]";
        public readonly static string SP_UpdatePasswordsToDefault = "[dbo].[SP_UpdatePasswordsToDefault]";
        public readonly static string SP_CheckEmailExists = "[dbo].[SP_CheckEmailExists]";

        #endregion

        #region User Login

        /// <summary>
        /// Login API
        /// </summary>
        /// <param name="loginParam"></param>
        /// <returns></returns>
        /// 

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Loginparam loginParam)
        {
            Message msg = new Message();
            LoginRes loginRes;
            try
            {
                string dec = EncryptDecryptPassword.DecryptQueryString(loginParam.Password);
                loginParam.Password = EncryptDecryptPassword.EncryptQueryString(loginParam.Email + "|" + loginParam.Password);

                DataTable dt = DataLogic.LoginDetails(loginParam, SP_UserLogin);

                dt.Columns.Add("Message", typeof(string));
                dt.Columns.Add("RefreshTokenNew", typeof(string));
                dt.Columns.Add("Token", typeof(string));
                dt.Columns.Add("ValidTill", typeof(string));


                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ErrorMessage"))
                    {
                        msg.message = dt.Rows[0]["ErrorMessage"].ToString();
                        msg.status = "401";
                        return Ok(msg);
                    }
                    else
                    {
                        var emailfromDB = EncryptDecryptPassword.DecryptQueryString(dt.Rows[0]["Password"].ToString()).Split("|");
                        var userID = dt.Rows[0]["User_ID"].ToString();
                        var email = dt.Rows[0]["Email"].ToString();
                        var roleID = dt.Rows[0]["phone"].ToString();
                        var status = dt.Rows[0]["Status"].ToString();

                        dt.Columns.Remove("Password");

                        #region Return Parameters

                        if (emailfromDB[0] == loginParam.Email && status == "200")
                        {

                            #region JWT Authentication

                            var tokenString = JWTBuilder.Generation(userID, email, roleID, status);

                            var refreshToken = JWTBuilder.GenerateRefreshToken();
                            DataTable dt2 = DataLogic.UpdateRefreshToken(loginParam.Email, refreshToken, UpdateRefreshToken_SP);

                            #endregion

                            //loginRes = new LoginRes();

                            foreach (DataRow row in dt.Rows)
                            {
                                // You can set the value for the new column here
                                row["Message"] = "User authenticated";
                                row["RefreshTokenNew"] = refreshToken;
                                row["Token"] = tokenString.Item1.ToString();
                                row["ValidTill"] = tokenString.Item2.ToString();
                            }

                            return Ok(dt);
                        }
                        else if (status == "401")
                        {

                            foreach (DataRow row in dt.Rows)
                            {
                                // You can set the value for the new column here
                                row["Message"] = "User not authenticated";
                                row["RefreshTokenNew"] = "";
                                row["Token"] = "";
                                row["ValidTill"] = "";
                            }

                            return Ok(dt);
                        }
                        else
                        {
                            
                            foreach (DataRow row in dt.Rows)
                            {
                                // You can set the value for the new column here
                                row["Message"] = "User not found";
                                row["RefreshTokenNew"] = "";
                                row["Token"] = "";
                                row["ValidTill"] = "";
                            }

                            return Ok(dt);
                        }

                        #endregion

                    }
                }
                else
                {
                    return Ok(dt);
                }

            }
            catch (Exception ex)
            {
                msg.message = ex.Message;
                return Ok(msg);
            }

        }

        #endregion

        #region Change Password

        /// <summary>
        /// This API will verify the old password first and if it is correct it will update it with New Password.
        /// </summary>
        /// <param name="changePassword"></param>
        [HttpPost("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            Message msg = new Message();

            try
            {
                changePassword.OldPassword = EncryptDecryptPassword.EncryptQueryString(changePassword.EmailAddress + "|" + changePassword.OldPassword);
                changePassword.NewPassword = EncryptDecryptPassword.EncryptQueryString(changePassword.EmailAddress + "|" + changePassword.NewPassword);
                DataTable dt = DataLogic.VerifyOldPassword(changePassword, SP_VerifyOldPassword);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ErrorMessage"))
                    {
                        msg.message = dt.Rows[0]["ErrorMessage"].ToString();
                        msg.status = "401";
                        return Ok(msg);
                    }
                    else
                    {
                        msg.message = dt.Rows[0]["Message"].ToString();
                        msg.status = dt.Rows[0]["Status"].ToString();
                        return Ok(msg);
                    }

                }
                else
                {
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                msg.message = ex.Message;
                return Ok(msg);
            }

        }

        #endregion

        #region Get All Users

        /// <summary>
        /// This API will return the email address for all users who are active
        /// </summary>
        [HttpPost("GetAllUsers")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            Message msg = new Message();

            try
            {
                
                DataTable dt = DataLogic.GetAllUsers(SP_GetAllUsers);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("ErrorMessage"))
                    {
                        msg.message = dt.Rows[0]["ErrorMessage"].ToString();
                        msg.status = "401";
                        return Ok(msg);
                    }
                    else
                    {
                        string res = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string email = dt.Rows[i]["Email"].ToString();
                            string pass = EncryptDecryptPassword.EncryptQueryString(email+ "|Abc@123");
                            DataTable dt2 = DataLogic.UpdatePasswordToDefault(email, pass, SP_UpdatePasswordsToDefault);
                        }
                        msg.message = "Users Updated";
                        msg.status = "200";
                        return Ok(msg);
                    }

                }
                else
                {
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                msg.message = ex.Message;
                return Ok(msg);
            }

        }

        #endregion

        #region Update Password to Default

        /// <summary>
        /// This API will verify the old password first and if it is correct it will update it with New Password.
        /// </summary>
        /// <param name="changePassword"></param>
        [HttpPost("ForgotPassword")]
        //[Authorize]
        public IActionResult ForgotPassword([FromBody] ChangePassword changePassword)
        {
            Message msg = new Message();

            try
            {
                DataTable checkEmailDT = DataLogic.CheckEmailExists(changePassword.EmailAddress, SP_CheckEmailExists);
                if (Convert.ToInt32(checkEmailDT.Rows[0][0]) == 0)
                {
                    msg.message = "Email Address does not exists";
                    msg.status = "401";
                    return Ok(msg);
                }
                changePassword.NewPassword = EncryptDecryptPassword.EncryptQueryString(changePassword.EmailAddress + "|" + changePassword.NewPassword);
                DataTable dt = DataLogic.UpdatePasswordToDefault(changePassword.EmailAddress, changePassword.NewPassword, SP_UpdatePasswordsToDefault);
                msg.message = "Update successfully.";
                msg.status = "200";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                msg.message = ex.Message;
                return Ok(msg);
            }

        }

        #endregion

    }
}
