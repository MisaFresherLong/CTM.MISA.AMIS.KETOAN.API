using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;

namespace MISA.AMIS.KETOAN.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {

        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy một bản ghi theo ID  
        /// </summary>
        /// <param name="recordID">ID của bản ghi</param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);

                // Xử lý kết quả
                if (record == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler
                    {
                        ErrorCode = 1,
                        DevMsg = "Get data from database failed.",
                        UserMsg = "Lấy bản ghi thất bại.",
                        MoreInfo = "/errorCode/1",
                        TraceId = HttpContext.TraceIdentifier
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var errorHandler = new ErrorHandler();
                errorHandler.ErrorCode = 1;
                errorHandler.DevMsg = "Catch an exception.";
                errorHandler.UserMsg = "Có lỗi xảy ra, vui lòng liên hệ Misa để được hỗ trợ.";
                errorHandler.MoreInfo = "/errorCode/1";
                errorHandler.TraceId = HttpContext.TraceIdentifier;
                return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
            }
        }

        #endregion
    }
}
