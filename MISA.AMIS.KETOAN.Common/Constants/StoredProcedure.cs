using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public class StoredProcedure
    {
        /// <summary>
        /// Stored procedure lấy tất cả bản ghi
        /// </summary>
        public static string GetAllRecord = "Proc_{0}_GetAll";

        /// <summary>
        /// Stored procedure lấy bản ghi theo ID
        /// </summary>
        public static string GetRecordByID = "Proc_{0}_GetByID";

        /// <summary>
        /// Stored procedure lấy bản ghi theo mã
        /// </summary>
        public static string GetRecordByCode = "Proc_{0}_GetByCode";

        /// <summary>
        /// Stored procedure lọc bản ghi
        /// </summary>
        public static string GetFilterRecord = "Proc_{0}_GetFilter";

        /// <summary>
        /// Stored procedure lấy mã lớn nhất
        /// </summary>
        public static string GetBiggestCode = "Proc_{0}_GetBiggestCode";

        /// <summary>
        /// Stored procedure thêm bản ghi
        /// </summary>
        public static string InsertRecord = "Proc_{0}_Insert";

        /// <summary>
        /// Stored procedure sửa bản ghi
        /// </summary>
        public static string UpdateRecord = "Proc_{0}_Update";

        /// <summary>
        /// Stored procedure xóa một bản ghi
        /// </summary>
        public static string DeleteRecord = "Proc_{0}_DeleteByID";

        /// <summary>
        /// Stored procedure xóa nhiều bản ghi
        /// </summary>
        public static string DeleteBatch = "Proc_{0}_DeleteBatch";
    }
}
