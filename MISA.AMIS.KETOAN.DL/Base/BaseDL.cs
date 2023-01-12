using Dapper;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field

        protected IConnectionLayer _connectionLayer;

        #endregion

        #region Constructor

        public BaseDL(IConnectionLayer connectionLayer)
        {
            _connectionLayer = connectionLayer;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: PVLONG (26/12/2022)
        public IEnumerable<T> GetAllRecords()
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetAllRecord, className);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var record = _connectionLayer.Query<T>(connection, storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }
        }

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public T GetRecordByID(Guid recordID)
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetRecordByID, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{className}ID", recordID);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var record = _connectionLayer.QueryFirstOrDefault<T>(connection, storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return record;
            }
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created by: PVLONG (26/12/2022)
        public Guid InsertRecord(T record)
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.InsertRecord, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = PrepareInsertParameter(record);
            Guid recordID = Guid.NewGuid();
            parameters.Add($"@{className}ID", recordID);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var effectedRow = _connectionLayer.Execute(connection, storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return recordID;
            }
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <returns>Số dòng đã được sửa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int UpdateRecord(Guid recordID, T record)
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.UpdateRecord, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = PrepareInsertParameter(record);
            parameters.Add($"@{className}ID", recordID);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var numberOfEffectedRow = _connectionLayer.Execute(connection, storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return numberOfEffectedRow;
            }
        }

        /// <summary>
        /// Xóa một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần xóa</param>
        /// <returns>Số dòng đã được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteRecord(Guid recordID)
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.DeleteRecord, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{className}ID", recordID);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var numberOfEffectedRow = _connectionLayer.Execute(connection, storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return numberOfEffectedRow;
            }
        }

        /// <summary>
        /// Hàm chuẩn bị tham số đầu vào để insert
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Tham số đầu vào</returns>
        /// Created by: PVLONG (26/12/2022)
        internal DynamicParameters PrepareInsertParameter(T record)
        {
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            // Duyệt qua từng thuộc tính và thêm giá trị vào parameter
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                parameters.Add($"@{property.Name}", propertyValue);
            }
            return parameters;
        }

        #endregion
    }
}
