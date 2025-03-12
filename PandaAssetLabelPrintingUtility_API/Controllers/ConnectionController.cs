using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PandaAssetLabelPrintingUtility_API.BAL.ResponseParameters;

namespace PandaAssetLabelPrintingUtility_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {

        #region Constructor

        private readonly IConfiguration _configuration;
        public ConnectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Get Connection

        /// <summary>
        /// The API will return all active Impacts in response
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("GetConnection")]
        [Authorize]
        public IActionResult GetConnection()
        {
            Message msg = new Message();
            try
            {
                //DataTable dt = DataLogic.GetImpactsDD(SP_GetImpactsDD);
                var configurationBuilder = new ConfigurationBuilder();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

                string con = configurationBuilder.Build().GetSection("ConnectionStrings:DefaultConnection").Value;

                return Ok(con);
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
