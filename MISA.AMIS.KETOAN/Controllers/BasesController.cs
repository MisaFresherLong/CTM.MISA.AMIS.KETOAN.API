using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;

namespace MISA.AMIS.KETOAN.API.Controllers
{
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
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet()]
        public IActionResult GetAllRecords()
        {
            try
            {
                var record = _baseBL.GetAllRecords();

                // Trả về kết quả cho client
                return Ok(record);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

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
                        ErrorCode = ErrorCode.GetDataError,
                        DevMsg = Resource.GetDataError_DevMsg,
                        UserMsg = Resource.GetDataError_UserMsg,
                        MoreInfo = GetMoreInfoMsg(ErrorCode.GetDataError),
                        TraceId = HttpContext.TraceIdentifier
                    };

                    return StatusCode(StatusCodes.Status404NotFound, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpPost()]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                var serviceResponse = _baseBL.InsertRecord(record);
                ErrorHandler errorHandler;

                // Kiểm tra các trạng thái của service
                switch (serviceResponse.Status)
                {
                    case ServiceResponseStatus.InputDataInvalid:
                        // Nếu dữ liệu đầu vào không hợp lệ trả về lỗi
                        errorHandler = new ErrorHandler
                        {
                            ErrorCode = ErrorCode.ValidateError,
                            DevMsg = Resource.ValidateError_DevMsg,
                            UserMsg = Resource.ValidateError_UserMsg,
                            MoreInfo = serviceResponse.Data,
                            TraceId = HttpContext.TraceIdentifier
                        };

                        return StatusCode(StatusCodes.Status400BadRequest, errorHandler);
                        break;
                    case ServiceResponseStatus.Done:
                        // Nếu thành công trả về kết quả cho client
                        return StatusCode(StatusCodes.Status201Created, serviceResponse.Data);
                        break;
                    default:
                        // Nếu thất bại trả về lỗi
                        errorHandler = new ErrorHandler
                        {
                            ErrorCode = ErrorCode.InsertError,
                            DevMsg = Resource.InsertError_DevMsg,
                            UserMsg = Resource.InsertError_UserMsg,
                            MoreInfo = GetMoreInfoMsg(ErrorCode.InsertError),
                            TraceId = HttpContext.TraceIdentifier
                        };

                        return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                        break;
                }

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <returns>Số dòng đã được sửa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        {
            try
            {
                var numberOfEffectedRow = _baseBL.UpdateRecord(recordID, record);

                // Xử lý kết quả
                if (numberOfEffectedRow == 0)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler
                    {
                        ErrorCode = ErrorCode.UpdateError,
                        DevMsg = Resource.UpdateError_DevMsg,
                        UserMsg = Resource.UpdateError_UserMsg,
                        MoreInfo = GetMoreInfoMsg(ErrorCode.UpdateError),
                        TraceId = HttpContext.TraceIdentifier
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }

                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, recordID);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần xóa</param>
        /// <returns>Số dòng đã được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                var numberOfEffectedRow = _baseBL.DeleteRecord(recordID);

                // Xử lý kết quả
                if (numberOfEffectedRow == 0)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler
                    {
                        ErrorCode = ErrorCode.DeleteError,
                        DevMsg = Resource.DeleteError_DevMsg,
                        UserMsg = Resource.DeleteError_UserMsg,
                        MoreInfo = GetMoreInfoMsg(ErrorCode.DeleteError),
                        TraceId = HttpContext.TraceIdentifier
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }

                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, numberOfEffectedRow);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Ngoại lệ đã được xử lý</returns>
        /// Created by: PVLONG (26/12/2022)
        protected IActionResult HandleException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorHandler()
            {
                ErrorCode = ErrorCode.Exception,
                DevMsg = Resource.Exception_DevMsg,
                UserMsg = Resource.Exception_UserMsg,
                // MoreInfo = GetMoreInfoMsg(ErrorCode.Exception),
                MoreInfo = exception.Message,
                TraceId = HttpContext.TraceIdentifier
            });
        }

        /// <summary>
        /// Hàm lấy tin nhắn MoreInfo
        /// </summary>
        /// <param name="errorCode">mã lỗi</param>
        /// <returns>Tin nhắn MoreInfo</returns>
        /// Created by: PVLONG (26/12/2022)
        protected String GetMoreInfoMsg(ErrorCode errorCode)
        {
            return String.Concat(Resource.MoreInfoMsg, (int)errorCode);
        }

        #endregion
    }
}
