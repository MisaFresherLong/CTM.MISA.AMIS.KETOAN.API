using Dapper;
using MISA.AMIS.KETOAN.Common;
using MISA.AMIS.KETOAN.DL;
using MySqlConnector;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetNewCode()
        {
            return _employeeDL.GetNewEmployeeCode();
        }

        /// <summary>
        /// Lọc nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="keyword">Từ khóa cần lọc</param>
        /// <param name="limit">Số bản ghi cần lấy</param>
        /// <param name="offset">Nơi bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên đã được phân trang</returns
        public Pagingnation<Employee> GetFilterEmployees(
            string? keyword,
            int limit,
            int offset
        )
        {
            return _employeeDL.GetFilterEmployees(keyword, limit, offset); 
        }

        /// <summary>
        /// Xóa nhiều nhân viên dựa theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID của các nhân viên cần xóa/param>
        /// <returns>Danh sách GUID của các nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteBatchEmployees(ListEmployeeIDs listEmployeeIDs)
        {
            return _employeeDL.DeleteBatchEmployees(listEmployeeIDs);
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
                // Truy vấn data layer 
                Employee employee = _employeeDL.GetEmployeeByCode(employeeCode);

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
                // Truy vấn data layer 
                Employee employee = _employeeDL.GetEmployeeByCode(employeeCode);

                // Xử lý kết quả
                if (employee == null)
                {
                    // Nếu mã không tồn tại trả về true
                    return true;
                }
                else if (employee.EmployeeID == employeeID)
                {
                    // Nếu mã tồn tại và id trùng khớp trả về true
                    return true;
                }
                // Nếu mã tồn tại và id không trùng khớp trả về false
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
