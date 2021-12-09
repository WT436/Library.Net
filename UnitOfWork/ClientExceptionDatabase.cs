using System;

namespace UnitOfWork
{
    /// <summary>
    /// Chỉ dùng cho các loại lỗi 4XX  từ phía client
    /// <para>400 Bad Request – Request không hợp lệ.</para>
    /// <para>401 Unauthorized – Request cần có Authentication.</para>
    /// <para>403 Forbidden – Máy chủ từ chối do quá tải.</para>
    /// <para>404 Not Found – Không tìm thấy trang được yêu cầu.</para>
    /// <para>405 Method Not Allowed – Phương thức không cho phép với user hiện tại.</para>
    /// <para>410 Gone – Resource không còn tồn tại, Version cũ đã không còn hỗ trợ.</para>
    /// <para>415 Unsupported Media Type – Không hỗ trợ kiểu Resource này.</para>
    /// <para>422 Unprocessable Entity – Dữ liệu không được xác thực do thiếu thông tin</para>
    /// <para>429 Too Many Requests – Request bị từ chối do bị giới hạn.</para>
    /// </summary>
    [Serializable]
    public class ClientExceptionDatabase : Exception
    {
        public int errorCode;
        /// <summary>
        /// Chỉ dùng cho các loại lỗi 4XX  từ phía client
        /// <para>400 Bad Request – Request không hợp lệ.</para>
        /// <para>401 Unauthorized – Request cần có Authentication.</para>
        /// <para>403 Forbidden – Máy chủ từ chối do quá tải.</para>
        /// <para>404 Not Found – Không tìm thấy trang được yêu cầu.</para>
        /// <para>405 Method Not Allowed – Phương thức không cho phép với user hiện tại.</para>
        /// <para>410 Gone – Resource không còn tồn tại, Version cũ đã không còn hỗ trợ.</para>
        /// <para>415 Unsupported Media Type – Không hỗ trợ kiểu Resource này.</para>
        /// <para>422 Unprocessable Entity – Dữ liệu không được xác thực do thiếu thông tin</para>
        /// <para>429 Too Many Requests – Request bị từ chối do bị giới hạn.</para>
        /// </summary>
        public ClientExceptionDatabase(int StatusCode, string message) : base(message)
        {
            errorCode = StatusCode;
        }
    }
}
