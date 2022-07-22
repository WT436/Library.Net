using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Dtos
{
    public class JwtCustomClaims
    {
        /// <summary>
        /// Id của tài khoản
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Tên tài khoản
        /// </summary>
        public string UserName { get; set; } 
        /// <summary>
        /// Email của tài khoản
        /// </summary>
        public string Email { get; set; }
    }
}
