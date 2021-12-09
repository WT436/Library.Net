using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Exceptions
{
    /// <summary>
    /// Chỉ dùng cho các loại lỗi 5XX từ phía server 
    /// <para>500 Internal Server Error: Một thông báo chung,khi máy chủ gặp phải một trường hợp bất ngờ.</para>
    /// <para>501 Not Implemented: Máy chủ không công nhận các phương thức yêu cầu hoặc không có khả năng xử lý nó.</para>
    /// <para>502 Bad Gateway: Máy chủ đã hoạt động như một gateway hoặc proxy và nhận được một phản hồi không hợp lệ từ máy chủ nguồn.</para>
    /// <para>503 Service Unavailable: Máy chủ hiện tại không có sẵn (hiện đang quá tải hoặc bị down để bảo trì). Đây chỉ là trạng thái tạm thời.</para>
    /// <para>504 Gateway Timeout: Máy chủ đã hoạt động như một gateway hoặc proxy và không nhận được một phản hồi từ máy chủ nguồn.</para>
    /// <para>505 HTTP Version Not Supported: Máy chủ không hỗ trợ phiên bản “giao thức HTTP”.</para>
    /// </summary>
    public class ServerException : Exception
    {
        public int errorCode;
        /// <summary>
        /// Chỉ dùng cho các loại lỗi 5XX từ phía server 
        /// <para>500 Internal Server Error: Một thông báo chung,khi máy chủ gặp phải một trường hợp bất ngờ.</para>
        /// <para>501 Not Implemented: Máy chủ không công nhận các phương thức yêu cầu hoặc không có khả năng xử lý nó.</para>
        /// <para>502 Bad Gateway: Máy chủ đã hoạt động như một gateway hoặc proxy và nhận được một phản hồi không hợp lệ từ máy chủ nguồn.</para>
        /// <para>503 Service Unavailable: Máy chủ hiện tại không có sẵn (hiện đang quá tải hoặc bị down để bảo trì). Đây chỉ là trạng thái tạm thời.</para>
        /// <para>504 Gateway Timeout: Máy chủ đã hoạt động như một gateway hoặc proxy và không nhận được một phản hồi từ máy chủ nguồn.</para>
        /// <para>505 HTTP Version Not Supported: Máy chủ không hỗ trợ phiên bản “giao thức HTTP”.</para>
        /// </summary>
        public ServerException(int StatusCode, string message) : base(message)
        {
            errorCode = StatusCode;
        }
    }
}
