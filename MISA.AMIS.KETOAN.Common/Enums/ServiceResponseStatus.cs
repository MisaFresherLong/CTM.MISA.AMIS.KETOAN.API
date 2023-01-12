using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public enum ServiceResponseStatus
    {
        /// <summary>
        /// Hoàn thành
        /// </summary>
        Done = 0,

        /// <summary>
        /// Lỗi đầu vào không hợp lệ
        /// </summary>
        InputDataInvalid = 1,

        /// <summary>
        /// Mã trùng
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Gọi procedure trong database thất bại
        /// </summary>
        ExecuteDatabaseFailed = 3,
    }
}
