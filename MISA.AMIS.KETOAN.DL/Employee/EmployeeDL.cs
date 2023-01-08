﻿using Dapper;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Lấy nhân viên theo mã
        /// </summary>
        /// <returns>Nhân viên cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public Employee GetEmployeeByCode(String employeeCode)
        {
            string className = "employee";

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetRecordByCode, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", employeeCode);

            // Khởi tạo kết nối đến database
            string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Truy vấn database
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return employee;
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetNewEmployeeCode()
        {
            string className = "employee";

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetBiggestCode, className);

            // Khởi tạo kết nối đến database
            string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Truy vấn database
                var biggestCode = mySqlConnection.QueryFirstOrDefault<string>(storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                return biggestCode;
            }
        }

        /// <summary>
        /// Lọc nhân viên theo các tiêu chí
        /// </summary>
        /// <param name="keyword">Từ khóa cần lọc</param>
        /// <param name="limit">Số bản ghi cần lấy</param>
        /// <param name="offset">Nơi bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên đã được phân trang</returns
        public Pagingnation<Employee> GetFilterEmployees(
            string? keyword,
            int limit,
            int offset
        )
        {
            string className = "employee";

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetFilterRecord, className);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);

            // Khởi tạo kết nối đến database
            string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Truy vấn database
                var reader = mySqlConnection.QueryMultiple(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return new Pagingnation<Employee>()
                {
                    TotalPage = 1,
                    TotalRecord = 2,
                    Data = reader.Read<Employee>().ToList()
                };
            }
        }

        /// <summary>
        /// Xóa nhiều nhân viên dựa theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID của các nhân viên cần xóa/param>
        /// <returns>Danh sách GUID của các nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteBatchEmployees(ListEmployeeIDs listEmployeeIDs)
        {
            return 0;
        }
    }
}
