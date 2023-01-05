using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System.Diagnostics;

namespace MISA.AMIS.KETOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả nhân viên 
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;ConvertZeroDateTime=True;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_GetAll";

                // Chuẩn bị tham số đầu vào

                // Truy vấn database
                var employees = mySqlConnection.Query<Employee>(storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employees == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Get data from database failed.";
                    errorHandler.UserMsg = "Lấy danh sách nhân viên thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employees);
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

        /// <summary>
        /// Lấy một nhân viên theo ID  
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần lấy</param>
        /// <returns>Nhân viên cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetAEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_GetOneByID";

                // Chuẩn bị dữ liệu đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);

                // Truy vấn database
                var employee = mySqlConnection.Query<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employee == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler
                    {
                        ErrorCode = 1,
                        DevMsg = "Get data from database failed.",
                        UserMsg = "Lấy nhân viên thất bại.",
                        MoreInfo = "/errorCode/1",
                        TraceId = HttpContext.TraceIdentifier
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employee);
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

        /// <summary>
        /// Lọc nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="keyword">Từ khóa cần lọc</param>
        /// <param name="departmentID">Phòng ban cần lọc</param>
        /// <param name="limit">Số bản ghi cần lấy</param>
        /// <param name="offset">Nơi bắt đầu lấy</param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult FilterEmployees(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int offset = 0
            )
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);
                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_GetFilter";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword);
                parameters.Add("@Limit", limit);
                parameters.Add("@Offset", offset);

                // Truy vấn database
                var employees = mySqlConnection.Query<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employees == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Get data from database failed.";
                    errorHandler.UserMsg = "Lọc nhân viên thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employees);
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

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpGet("newCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeBL.GetNewCode();

                // Xử lý kết quả
                if (newEmployeeCode == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Get data from database failed.";
                    errorHandler.UserMsg = "Lấy mã nhân viên mới thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
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

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm</param>
        /// <returns>GUID của nhân viên vừa được thêm</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpPost]
        public IActionResult InsertAEmployee([FromBody] Employee employee)
        {
            try
            {
                // Kiểm tra mã tồn tại
                if (IsEmployeeCodeExist(employee.EmployeeCode))
                {
                    // Nếu mã đã tồn tại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Employee code is existed.";
                    errorHandler.UserMsg = "Mã nhân viên đã tồn tại trong hệ thống.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }

                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_Insert";

                // Chuẩn bị dữ liệu đầu vào
                Guid employeeID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Address", employee.Address);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@JobPosition", employee.JobPosition);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssueDate", employee.IdentityIssueDate);
                parameters.Add("@IdentityIssuePlace", employee.IdentityIssuePlace);
                parameters.Add("@BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("@BankName", employee.BankName);
                parameters.Add("@BankBranch", employee.BankBranch);
                parameters.Add("@CreatedBy", employee.CreatedBy);

                // Truy vấn database
                var newEmployee = mySqlConnection.Query<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (newEmployee == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Insert employee failed.";
                    errorHandler.UserMsg = "Thêm mới nhân viên thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employeeID);
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

        /// <summary>
        /// Chỉnh sửa một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Các thông tin nhân viên cần sửa</param>
        /// <returns>GUID của nhân viên vừa được sửa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployeeByID([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            try
            {
                // Kiểm tra mã nhân viên
                if (!IsEmployeeCodeValid(employee.EmployeeCode, employeeID))
                {
                    // Nếu mã không hợp lệ trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Employee code is existed.";
                    errorHandler.UserMsg = "Mã nhân viên đã tồn tại trong hệ thống.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }

                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_Update";

                // Chuẩn bị dữ liệu đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Address", employee.Address);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@JobPosition", employee.JobPosition);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssueDate", employee.IdentityIssueDate);
                parameters.Add("@IdentityIssuePlace", employee.IdentityIssuePlace);
                parameters.Add("@BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("@BankName", employee.BankName);
                parameters.Add("@BankBranch", employee.BankBranch);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);

                // Truy vấn database
                var newEmployee = mySqlConnection.Query<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (newEmployee == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Update failed.";
                    errorHandler.UserMsg = "Sửa nhân viên thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employeeID);
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

        /// <summary>
        /// Xóa một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>GUID của nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_DeleteOneByID";

                // Chuẩn bị dữ liệu đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);

                // Truy vấn database
                var employee = mySqlConnection.Query<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employee == null)
                {
                    // Nếu thất bại trả về lỗi
                    var errorHandler = new ErrorHandler();
                    errorHandler.ErrorCode = 1;
                    errorHandler.DevMsg = "Delete employee failed.";
                    errorHandler.UserMsg = "Lấy nhân viên thất bại.";
                    errorHandler.MoreInfo = "/errorCode/1";
                    errorHandler.TraceId = HttpContext.TraceIdentifier;
                    return StatusCode(StatusCodes.Status500InternalServerError, errorHandler);
                }
                // Nếu thành công trả về kết quả cho client
                return StatusCode(StatusCodes.Status200OK, employeeID);
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

        /// <summary>
        /// Xóa nhiều nhân viên dựa theo danh sách ID
        /// </summary>
        /// <param name="employeeIDs">Danh sách ID của các nhân viên cần xóa/param>
        /// <returns>Danh sách GUID của các nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        [HttpDelete("deleteBatch")]
        public IActionResult DeleteMultiEmployeeByID([FromBody] String employeeIDs)
        {
            return StatusCode(StatusCodes.Status200OK, employeeIDs);
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// CreatedBy: PVLONG (26/12/2022)
        private Boolean IsEmployeeCodeExist(string employeeCode)
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_GetOneByCode";

                // Chuẩn bị dữ liệu đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);

                // Truy vấn database
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employee == null)
                {
                    // Nếu không tồn tại trả về false
                    return false;
                }
                // Nếu tồn tại trả về true
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Kiểm tra mã nhân viên lỗi.", e);
            }
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có hợp lệ để sửa hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// CreatedBy: PVLONG (26/12/2022)
        private Boolean IsEmployeeCodeValid(string employeeCode, Guid employeeID)
        {
            try
            {
                // Khởi tạo kết nối đến database
                string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh sql
                string storedProcedure = "Proc_employee_GetOneByCode";

                // Chuẩn bị dữ liệu đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);

                // Truy vấn database
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả
                if (employee == null)
                {
                    // Nếu mã không tồn tại trả về true
                    return true;
                }
                else if (employee.EmployeeID == employeeID)
                {
                    // Nếu mã trùng khớp trả về true
                    return true;
                }
                // Nếu mã không trùng khớp trả về false
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("Kiểm tra mã nhân viên lỗi.", e);
            }
        }

        #endregion
    }
}
