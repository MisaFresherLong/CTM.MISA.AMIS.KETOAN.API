using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    public class ServiceResponse
    {
        /// <summary>
        /// Trạng thái của phản hồi
        /// </summary>
        public ServiceResponseStatus Status { get; set; }

        /// <summary>
        /// Data response khi thành công hoặc thất bại
        /// </summary>
        public object? Data { get; set; }
    }
}
