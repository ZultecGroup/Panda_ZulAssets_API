using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaAssetLabelPrintingUtility_API.DAL;
using static PandaAssetLabelPrintingUtility_API.BAL.RequestParameters;
using static PandaAssetLabelPrintingUtility_API.BAL.ResponseParameters;
using System.Data;
using PandaAssetLabelPrintingUtility_API.BAL;
using Microsoft.AspNetCore.Authorization;

namespace PandaAssetLabelPrintingUtility_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeViewController : ControllerBase
    {

        #region Declarations & Contruction

        public readonly static string SP_GetLocations = "[dbo].[SP_GetLocations]";
        public readonly static string SP_GetCategories = "[dbo].[SP_GetCategories]";

        #endregion

        #region Get Locations

        /// <summary>
        /// The API will return all locations
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("GetLocations")]
        [Authorize]
        public IActionResult GetLocations()
        {
            Message msg = new Message();
            try
            {

                DataTable dt = DataLogic.GetLocations(SP_GetLocations);

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
                        List<FlatObject_ddl> flatObjects = new List<FlatObject_ddl>();

                        foreach (DataRow type in dt.Rows)
                        {
                            flatObjects.Add(new FlatObject_ddl(type["text"].ToString(), type["id"].ToString(), type["parentId"].ToString()));
                        }


                        var recursiveObjects = FlatObject_ddl.FillRecursive_ddl(flatObjects, "0");

                        Return rtn;
                        if (dt.Rows.Count > 0)
                        {
                            rtn = new Return()
                            {
                                rescode = "1",
                                message = "success",
                                Detail = dt,
                                Menujson = recursiveObjects
                            };
                        }
                        else
                        {
                            rtn = new Return()
                            {
                                rescode = "0",
                                message = "failure"

                            };
                        }

                        return Ok(rtn);
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

        #region Get Categories

        /// <summary>
        /// The API will return all locations
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("GetCategories")]
        [Authorize]
        public IActionResult GetCategories()
        {
            Message msg = new Message();
            try
            {

                DataTable dt = DataLogic.GetCategories(SP_GetCategories);

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
                        List<FlatObject_ddl> flatObjects = new List<FlatObject_ddl>();

                        foreach (DataRow type in dt.Rows)
                        {
                            flatObjects.Add(new FlatObject_ddl(type["text"].ToString(), type["id"].ToString(), type["parentId"].ToString()));
                        }


                        var recursiveObjects = FlatObject_ddl.FillRecursive_ddl(flatObjects, "0");

                        Return rtn;
                        if (dt.Rows.Count > 0)
                        {
                            rtn = new Return()
                            {
                                rescode = "1",
                                message = "success",
                                Detail = dt,
                                Menujson = recursiveObjects
                            };
                        }
                        else
                        {
                            rtn = new Return()
                            {
                                rescode = "0",
                                message = "failure"

                            };
                        }

                        return Ok(rtn);
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
