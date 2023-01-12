using Dapper;
using MISA.AMIS.KETOAN.Common;
using MISA.AMIS.KETOAN.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: PVLONG (26/12/2022)
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created by: PVLONG (26/12/2022)
        public ServiceResponse InsertRecord(T record)
        {
            // Validate dữ liệu
            ServiceResponse validateResult = ValidateData(record);

            // Nếu validate thất bại, trả về response cho controller
            if (validateResult.Status != ServiceResponseStatus.Done)
            {
                return validateResult;
            }

            // Nếu thành công trả về kết quả cho controller
            return new ServiceResponse()
            {
                Status = ServiceResponseStatus.Done,
                Data = _baseDL.InsertRecord(record)
            };
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <returns>Số dòng đã được sửa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int UpdateRecord(Guid recordID, T record)
        {
            return _baseDL.UpdateRecord(recordID, record);
        }

        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần xóa</param>
        /// <returns>Số dòng đã được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteRecord(Guid recordID)
        {
            return _baseDL.DeleteRecord(recordID);
        }

        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mô tả thành công hay thất bại</returns>
        /// Created by: PVLONG (26/12/2022)
        public ServiceResponse ValidateData(T record)
        {
            var errorMessages = new List<string>();

            // Lặp qua từng property và kiểm tra attribute
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var propertyName = property.Name;

                // Kiểm tra RequiredAttribute
                var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    // Nếu tồn tại addtribute required và giá trị property rỗng thì thêm lỗi vào danh sách
                    errorMessages.Add(requiredAttribute.ErrorMessage);
                }

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
