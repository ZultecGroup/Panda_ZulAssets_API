using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaAssetLabelPrintingUtility_API.DAL;
using static PandaAssetLabelPrintingUtility_API.BAL.RequestParameters;
using static PandaAssetLabelPrintingUtility_API.BAL.ResponseParameters;
using System.Data;

namespace PandaAssetLabelPrintingUtility_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {

        #region Declarations

        public readonly static string SP_GetLabelInfo = "[dbo].[SP_GetLabelInfo]";

        #endregion

        #region Get Label Info

        /// <summary>
        /// The API will return all the assets against LocID or AstCatID
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("GetLabelInfo")]
        //[Authorize]
        public IActionResult GetLabelInfo()
        {
            Message msg = new Message();
            try
            {

                DataTable dt = DataLogic.GetLabelInfo(SP_GetLabelInfo);

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
                        return Ok(dt);
                    }
                }
                else
                {
                    msg.message = "No data found!";
                    msg.status = "401";
                    return Ok(msg);
                }

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
