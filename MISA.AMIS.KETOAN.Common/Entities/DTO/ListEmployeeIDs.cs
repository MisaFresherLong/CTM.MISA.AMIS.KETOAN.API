using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.Common
{
    class SpEmployeeID
    {
        public Guid EmployeeID { get; set; }
    }

    public class ListEmployeeIDs
    {
        /// <summary>
        /// Danh sách ID nhân viên
        /// </summary>
        public List<Guid> EmployeeIDs { get; set; }

        /// <summary>
        /// Lấy JSON của employeeIDs cho stored procedure
        /// </summary>
        /// <returns>JSON của employeeIDs</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetFormatedEmployeeIDs()
        {
            List<SpEmployeeID> formatedEmployeeIDs = new List<SpEmployeeID>();

            // Lặp qua từng giá trị -> format -> thêm vào list
            foreach (Guid employeeID in EmployeeIDs)
            {
                formatedEmployeeIDs.Add(new SpEmployeeID
                {
                    EmployeeID = employeeID
                });
            }

            return JsonConvert.SerializeObject(formatedEmployeeIDs.ToArray());
        }
    }
}
