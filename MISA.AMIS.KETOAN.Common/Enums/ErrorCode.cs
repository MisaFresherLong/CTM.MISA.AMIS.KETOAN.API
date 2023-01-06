using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public enum ErrorCode
    {
        /// <summary>
        /// Ngoại lệ
        /// </summary>
        Exception = 10,

        /// <summary>
        /// Kết nối database lỗi
        /// </summary>
        DatabaseConnectError = 20,

        /// <summary>
        /// Lấy dữ liệu lỗi
        /// </summary>
        GetDataError = 30,

        /// <summary>
        /// Validate dữ liệu lỗi
        /// </summary>
        ValidateError = 40,

        /// <summary>
        /// Thêm bản ghi lỗi
        /// </summary>
        InsertError = 50,

        /// <summary>
        /// Sửa bản ghi lỗi
        /// </summary>
        UpdateError = 60,

        /// <summary>
        /// Xóa bản ghi lỗi
        /// </summary>
        DeleteError = 70
    }
}
