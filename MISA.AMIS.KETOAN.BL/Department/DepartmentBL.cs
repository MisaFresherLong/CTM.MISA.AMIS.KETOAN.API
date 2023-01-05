using Dapper;
using MISA.AMIS.KETOAN.Common;
using MISA.AMIS.KETOAN.DL;
using MySqlConnector;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field

        private IDepartmentDL _departmentDL;

        #endregion

        #region Constructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }

        #endregion
    }
}
