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
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public T GetRecordByID(Guid recordID);
    }
}
