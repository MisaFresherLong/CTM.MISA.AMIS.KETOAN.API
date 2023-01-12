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
        /// Lọc bản ghi theo các tiêu chí
        /// </summary>
        /// <param name="filterKey">Các từ khóa cần lọc</param>
        /// <returns>Danh bản ghi đã được phân trang</returns
        /// Created by: PVLONG (26/12/2022)
        public PagingnationResponse<T> GetFilterRecords(FilterKey filterKey)
        {
            return _baseDL.GetFilterRecords(filterKey);
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
        public virtual ServiceResponse InsertRecord(T record)
        {
            // Validate dữ liệu
            var validateResult = ValidateData(record);

            // Nếu validate thất bại, trả về response cho controller
            if (validateResult.Status != ServiceResponseStatus.Done)
            {
                return validateResult;
            }

            // Kiểm tra trùng mã
            var checkDuplicateCodeResult = CheckDuplicateCode(record);

            // Nếu validate thất bại, trả về response cho controller
            if (checkDuplicateCodeResult.Status != ServiceResponseStatus.Done)
            {
                return checkDuplicateCodeResult;
            }

            // Nếu validate thành công, truy vấn bussiness layer và trả về kết quả cho controller
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
        public virtual ServiceResponse UpdateRecord(Guid recordID, T record)
        {
            // Validate dữ liệu
            var validateResult = ValidateData(record);

            // Nếu validate thất bại, trả về response cho controller
            if (validateResult.Status != ServiceResponseStatus.Done)
            {
                return validateResult;
            }

            // Kiểm tra trùng mã
            var checkDuplicateCodeResult = CheckDuplicateCode(record, recordID);

            // Nếu validate thất bại, trả về response cho controller
            if (checkDuplicateCodeResult.Status != ServiceResponseStatus.Done)
            {
                return checkDuplicateCodeResult;
            }

            // Nếu validate thành công, truy vấn bussiness layer và trả về kết quả cho controller
            return new ServiceResponse()
            {
                Status = ServiceResponseStatus.Done,
                Data = _baseDL.UpdateRecord(recordID, record)
            };
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
            // Validate dữ liệu (mặc định)
            var validateCommonResult = ValidateCommon(record);

            // Validate dữ liệu (tùy chỉnh)
            var validateCustomResult = ValidateCustom(record);

            // Nếu validate thất bại, trả về response cho controller
            if (validateCommonResult.Status != ServiceResponseStatus.Done ||
                validateCustomResult.Status != ServiceResponseStatus.Done)
            {
                return new ServiceResponse
                {
                    Status = ServiceResponseStatus.InputDataInvalid,
                    Data = validateCommonResult.Data == null ? validateCustomResult.Data : validateCommonResult.Data,
                };
            }

            // Nếu không có lỗi, trả về service response với status thành công
            return new ServiceResponse { Status = ServiceResponseStatus.Done };
        }

        /// <summary>
        /// Validate dữ liệu mặc định, chung cho tất cả entity
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mô tả thành công hay thất bại</returns>
        /// Created by: PVLONG (26/12/2022)
        public virtual ServiceResponse ValidateCommon(T record)
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

        /// <summary>
        /// Validate dữ liệu đầu vào tùy chỉnh cho các lớp kế thừa
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mô tả thành công hay thất bại</returns>
        /// Created by: PVLONG (26/12/2022)
        public virtual ServiceResponse ValidateCustom(T record)
        {
            // Mặc định validate thành công
            return new ServiceResponse { Status = ServiceResponseStatus.Done };
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Đối tượng ServiceResponse mô tả thành công hay thất bại</returns>
        /// Created by: PVLONG (26/12/2022)
        public virtual ServiceResponse CheckDuplicateCode(T record, Guid? recordID = null)
        {
            string className = typeof(T).Name;

            var recordCode = (string)record.GetType().GetProperty($"{className}Code").GetValue(record, null);

            if (IsRecordCodeValid(recordCode, recordID))
            {
                // Nếu recordCode hợp lệ, trả về thành công
                return new ServiceResponse { Status = ServiceResponseStatus.Done };
            }
            else
            {
                // Nếu recordCode hợp lệ, trả về lỗi mã trùng
                return new ServiceResponse
                {
                    Status = ServiceResponseStatus.DuplicateCode,
                    Data = recordCode
                };
            }

        }

        /// <summary>
        /// Kiểm tra mã bản ghi có hợp lệ hay không
        /// </summary>
        /// <param name="recordCode">Mã bản ghi cần kiểm tra</param>
        /// <param name="recordID">ID bản ghi cần kiểm tra</param>
        /// <returns></returns>
        /// CreatedBy: PVLONG (26/12/2022)
        private Boolean IsRecordCodeValid(string recordCode, Guid? recordID = null)
        {

            string className = typeof(T).Name;

            // Truy vấn data layer 
            T record = _baseDL.GetRecordByCode(recordCode);

            // Xử lý kết quả
            if (record == null)
            {
                // Nếu mã không tồn tại trả về true
                return true;
            }
            else if (recordID != null)
            {
                var dbRecordId = (Guid)record.GetType().GetProperty($"{className}ID").GetValue(record, null);
                // Nếu mã tồn tại và id trùng khớp trả về true
                if (dbRecordId == recordID) return true;
            }
            // Nếu mã tồn tại và id không trùng khớp trả về false
            return false;

        }
        #endregion
    }
}
