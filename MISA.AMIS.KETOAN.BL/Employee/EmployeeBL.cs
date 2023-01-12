using Dapper;
using MISA.AMIS.KETOAN.Common;
using MISA.AMIS.KETOAN.DL;
using MySqlConnector;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// Validate dữ liệu đầu vào tùy chỉnh cho các lớp kế thừa
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mô tả thành công hay thất bại</returns>
        /// Created by: PVLONG (26/12/2022)
        public override ServiceResponse ValidateCustom(Employee employee)
        {
            var errorMessages = new List<string>();

            // Lặp qua từng property và kiểm tra attribute
            var properties = typeof(Employee).GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(employee);
                var propertyName = property.Name;

                // Kiểm tra EmailAddressAttribute
                var emailAddressAttribute = (EmailAddressAttribute?)Attribute.GetCustomAttribute(property, typeof(EmailAddressAttribute));
                if (emailAddressAttribute != null &&
                    !string.IsNullOrEmpty(propertyValue?.ToString()) &&
                    !emailAddressAttribute.IsValid(propertyValue?.ToString())
                    )
                {
                    // Nếu tồn tại addtribute email và giá trị property không hợp lệ thì thêm lỗi vào danh sách
                    errorMessages.Add(emailAddressAttribute.ErrorMessage);
                }

                // Kiểm tra RegularExpressionAttribute
                var regexAttribute = (RegularExpressionAttribute?)Attribute.GetCustomAttribute(property, typeof(RegularExpressionAttribute));
                if (regexAttribute != null &&
                    !string.IsNullOrEmpty(propertyValue?.ToString()) &&
                    !regexAttribute.IsValid(propertyValue?.ToString())
                    )
                {
                    // Nếu tồn tại attribute regex và giá trị property không hợp lệ thì thêm lỗi vào danh sách
                    errorMessages.Add(regexAttribute.ErrorMessage);
                }

            }
            // Nếu có lỗi trả về service response với status invalid
            if (errorMessages.Count > 0)
            {
                return new ServiceResponse
                {
                    Status = ServiceResponseStatus.InputDataInvalid,
                    Data = errorMessages.ToArray()
                };
            }

            // Nếu không có lỗi, trả về service response với status thành công
            return new ServiceResponse { Status = ServiceResponseStatus.Done };
        }
        #endregion
    }
}
