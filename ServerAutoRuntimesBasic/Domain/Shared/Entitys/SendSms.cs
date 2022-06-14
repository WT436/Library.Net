using System;
using System.ComponentModel.DataAnnotations;

namespace ServerAutoRuntimesBasic.Domain.Shared.Entitys
{
    public class SendSms
    {
        [Key]
        [MaxLength(32)]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        [RegularExpression(@"(^(84|0))([0-9]{4,10}$)", ErrorMessage = "Destination must be in the form 84xxxx, 0xxx")]
        public string Destination { get; set; }
        public string Sender { get; set; }
        public string Keyword { get; set; }
        [MaxLength(480, ErrorMessage = "Short Message should not be greater than 480")]
        public string ShortMessage { get; set; }
        public string EncryptMessage { get; set; }
        [Range(0, 1, ErrorMessage = "IsEncrypt only 0 or 1")]
        public int IsEncrypt { get; set; }
        [Range(0, 1, ErrorMessage = "Type only 0 or 1")]
        public int Type { get; set; }
        public long RequestTime { get; set; } = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        [Required]
        public string PartnerCode { get; set; } = "950003";
        [Required]
        public string SercretKey { get; set; } = "5c6b322e-5d49-4ac3-9fab-1f2d9f3322d1";
    }
}
