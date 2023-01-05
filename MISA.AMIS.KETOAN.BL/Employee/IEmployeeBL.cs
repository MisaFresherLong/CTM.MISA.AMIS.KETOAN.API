using MISA.AMIS.KETOAN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetNewCode();

    }
}
