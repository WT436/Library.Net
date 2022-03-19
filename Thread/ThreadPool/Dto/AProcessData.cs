using ObjectExtention;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Thread.ThreadPool.Dto
{
    public class AProcessData
    {
        private int _index { get; set; }
        private int _retryNumber { get; set; } = 0;
        private DataRow _dataRow { get; set; }
        private SendSms _sendSms { get; set; }
        private SendSms _sendSmsReturn { get; set; }
        private int _isEncrypt { get; set; } = 0;

        public void AProcessDataSetUp(DataRow dataRow, SendSms sendSms, int retry, int isEncrypt, int index)
        {
            _dataRow = dataRow;
            _sendSms = sendSms;
            _retryNumber = retry;
            _isEncrypt = isEncrypt;
            _index = index;
        }

        public SendSms ProcessSendSms(int index)
        {
            SendSms sendSmsInline;
            // map data
            if (_sendSms != null)
            {
                sendSmsInline = _sendSms;
            }
            else
            {
                sendSmsInline = new SendSms
                {
                    Keyword = _dataRow["Keyword"].ToString(),
                    Sender = _dataRow["Sender"].ToString(),
                    Destination = _dataRow["Destination"].ToString(),
                    ShortMessage = "",
                    EncryptMessage = ThreadPoolBasic.EncryptDataInAES256ToKey(LanguageConvert.ConvertVietNamese(_dataRow["shortMessage"].ToString()), "5c6b322e-5d49-4ac3-9fab-1f2d9f3322d1"),
                    IsEncrypt = 1
                };
            }

            //if (_retryNumber <= 3)
            //{
            //    _retryNumber++;
            //}
            //if (_retryNumber >= 4)
            //{
            //    return null;
            //}

            return sendSmsInline;
        }
    }
}
