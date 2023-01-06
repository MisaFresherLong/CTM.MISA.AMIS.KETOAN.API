namespace MISA.AMIS.KETOAN.Common
{
    public class ErrorHandler
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message dành cho lập trình viên
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message dành cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm về lỗi
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Mã truy tìm
        /// </summary>
        public string TraceId { get; set; }

        public ErrorHandler()
        {
            ErrorCode = 0;
            DevMsg = "";
            UserMsg = "";
            MoreInfo = "";
            TraceId = "";
        }
    }
}
