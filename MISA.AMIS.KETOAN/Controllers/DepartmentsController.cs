using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.API.Controllers;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;

namespace MISA.AMIS.KETOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        }


        /// <summary>
        /// Lấy danh sách tất cả đơn vị 
        /// </summary>
        /// <returns>Danh sách tất cả đơn vị</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;ConvertZeroDateTime=True;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_department_GetAll";

                // Chuẩn bị tham số đầu vào

                // Truy vấn database
                var departments = mySqlConnection.Query<Department>(storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (departments == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Get data from database failed.";
                    errorHandler.UserMsg = "Lấy danh sách đơn vị thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, departments);
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
    }
}
