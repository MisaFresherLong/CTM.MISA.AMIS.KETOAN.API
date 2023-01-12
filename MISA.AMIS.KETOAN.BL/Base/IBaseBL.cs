using MISA.AMIS.KETOAN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: PVLONG (26/12/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created by: PVLONG (26/12/2022)
        public ServiceResponse InsertRecord(T record);

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <returns>Số dòng đã được sửa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần xóa</param>
        /// <returns>Số dòng đã được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteRecord(Guid recordID);
    }
}
