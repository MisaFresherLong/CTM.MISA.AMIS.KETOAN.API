using MISA.AMIS.KETOAN.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.DL
{
    public interface IConnectionLayer : IDisposable
    {
        /// <summary>
        /// Hàm lấy connection hiện tại
        /// <returns>Connection hiện tại</returns>
        /// Created by: PVLONG (10/01/2023)
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// Hàm thực thi câu lệnh sql
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: PVLONG (10/01/2023)
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm truy vấn câu lệnh sql
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Đối tượng được truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm truy vấn câu lệnh sql hoặc trả về mặc định
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Đối tượng được truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm truy vấn câu lệnh sql trả về nhiều kết quả
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>GridReader của kết quả truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
