namespace PandaAssetLabelPrintingUtility_API.BAL
{
    public class RequestParameters
    {

        #region Loginparam
        public class Loginparam
        {
            public string Email { get; set; }
            public string Password { get; set; }

        }
        #endregion

        #region Assets Params

        public class AssetsReqParams
        {
            public string LocID { get; set; } = "";
            public string AstCatID { get; set; } = "";
            public string Barcode { get; set; } = "";
        }

        #endregion

        #region ChangePassword
        public class ChangePassword
        {
            public string? EmailAddress { get; set; }
            public string? OldPassword { get; set; }
            public string? NewPassword { get; set; }
        }

        #endregion

    }
}
