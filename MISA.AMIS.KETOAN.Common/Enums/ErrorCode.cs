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
        Exception = 1,

        /// <summary>
        /// Kết nối database lỗi
        /// </summary>
        DatabaseConnectError = 2,

        /// <summary>
        /// Lấy dữ liệu lỗi
        /// </summary>
        GetDataError = 3,

        /// <summary>
        /// Validate dữ liệu lỗi
        /// </summary>
        ValidateError = 4,

        /// <summary>
        /// Thêm bản ghi lỗi
        /// </summary>
        InsertError = 5,

        /// <summary>
        /// Sửa bản ghi lỗi
        /// </summary>
        UpdateError = 6,

        /// <summary>
        /// Xóa bản ghi lỗi
        /// </summary>
        DeleteError = 7
    }
}
