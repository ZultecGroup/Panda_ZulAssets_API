namespace PandaAssetLabelPrintingUtility_API.BAL
{
    public class ResponseParameters
    {

        #region ResponseMsg

        public class Message
        {
            public string message { get; set; }
            public string status { get; set; }
        }

        #endregion

        #region LoginResponse

        public class LoginRes
        {
            public string UserID { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string RoleID { get; set; }
            public string Status { get; set; }
            public string Message { get; set; }
            public string ValidTill { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string RefreshTokenNew { get; set; }

        }

        #endregion

        #region Return Params for Tree

        public class Return
        {
            public string rescode { get; set; }
            public string message { get; set; }
            public object Detail { get; set; }
            public object Menujson { get; set; }

        }

        #endregion

    }
}
