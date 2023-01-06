using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KETOAN.API.Controllers;
using MISA.AMIS.KETOAN.BL;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;

namespace MISA.AMIS.KETOAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        }
    }
}
