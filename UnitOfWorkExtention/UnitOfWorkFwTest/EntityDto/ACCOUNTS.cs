using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkFwTest.EntityDto
{
    public class ACCOUNTS
    {
        public decimal ACCOUNT_ID { get; set; }
        public decimal ROLE_ID { get; set; }
        public string CONTACT_PERSON { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public DateTime DATETIME_CREATED { get; set; }
        public DateTime DATETIME_LAST_MODIFIED { get; set; }
        public decimal DEL_FLAG { get; set; }
        public decimal BLOCK_FLAG { get; set; }
        public string USER_NAME { get; set; }
        public string PASS_WORD { get; set; }
        public decimal PARTNER_ID { get; set; }
        public decimal PARENT_ID { get; set; }
        public decimal BALANCE_QC { get; set; }
        public decimal BALANCE_CSKH { get; set; }
        public string SMSTYPE_ALLOW { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime DATETIME_LASTLOGIN { get; set; }
    }                                                     

}
