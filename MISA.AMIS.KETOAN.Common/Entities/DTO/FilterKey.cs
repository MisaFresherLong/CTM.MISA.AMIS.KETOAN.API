using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public class FilterKey
    {
        /// <summary>
        /// Từ khóa cần tìm kiếm
        /// </summary>
        public string? keyword { get; set; }

        /// <summary>
        /// Số bản ghi cần lấy
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// Vị trí bắt đầu lấy
        /// </summary>
        public int offset { get; set; }
    }
}
