using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.API.Controllers;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System.Diagnostics;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lọc nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="keyword">Từ khóa cần lọc</param>
        /// <param name="departmentID">Phòng ban cần lọc</param>
        /// <param name="limit">Số bản ghi cần lấy</param>
        /// <param name="offset">Nơi bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên đã được phân trang</returns>
        [HttpGet("filter")]
        public IActionResult GetFilterEmployees(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0
            )
        {
            try
            {
                // Truy vấn bussiness layer
                var pagingnationEmployees = _employeeBL.GetFilterEmployees(keyword, limit, offset);

                // Trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, pagingnationEmployees);

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                // Truy vấn bussiness layer
                var newEmployeeCode = _employeeBL.GetNewCode();

                // Xử lý kết quả
                if (newEmployeeCode == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = ErrorCode.GetDataError;
                    errorHandler.DevMsg = Resource.GetNewEmployeeCodeError_DevMsg;
                    errorHandler.UserMsg = Resource.GetNewEmployeeCodeError_UserMsg;
                    errorHandler.MoreInfo = GetMoreInfoMsg(ErrorCode.GetDataError);
                    errorHandler.TraceId = HttpContext.TraceIdentifier;

                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xóa nhiều nhân viên dựa theo danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các nhân viên cần xóa/param>
        /// <returns>Danh sách GUID của các nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpDelete("deleteBatch")]
        public IActionResult DeleteBatchEmployees([FromBody] ListEmployeeIDs listEmployeeIDs)
        {
            try
            {
                var numberOfEffectedRow = _employeeBL.DeleteBatchEmployees(listEmployeeIDs);

                // Xử lý kết quả
                if (numberOfEffectedRow == 0)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler
                    {
                        ErrorCode = ErrorCode.DeleteError,
                        DevMsg = Resource.DeleteBatchEmployeeError_DevMsg,
                        UserMsg = Resource.DeleteBatchEmployeeError_UserMsg,
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

        #endregion
    }
}
