using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaAssetLabelPrintingUtility_API.DAL;
using static PandaAssetLabelPrintingUtility_API.BAL.ResponseParameters;
using System.Data;
using static PandaAssetLabelPrintingUtility_API.BAL.RequestParameters;

namespace PandaAssetLabelPrintingUtility_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        #region Declarations & Contruction

        public readonly static string SP_GetAssetInformation = "[dbo].[SP_GetAssetInformation]";

        #endregion

        #region Get Assets

        /// <summary>
        /// The API will return all the assets against LocID or AstCatID
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost("GetAssets")]
        [Authorize]
        public IActionResult GetAssets([FromBody] AssetsReqParams assetsReqParams)
        {
            Message msg = new Message();
            try
            {
                
                DataTable dt = DataLogic.GetAssets(assetsReqParams, SP_GetAssetInformation);

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

        #region Import Master Data

        /// <summary>
        /// The API will return all the assets against LocID or AstCatID
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost("ImportMasterData")]
        [Authorize]
        public IActionResult ImportMasterData()
        {
            return Ok();
        }

        #endregion

    }
}
