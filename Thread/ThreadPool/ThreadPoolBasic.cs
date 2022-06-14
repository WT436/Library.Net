using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thread.ThreadPool.Dto;
using System.Security.Cryptography;

namespace Thread.ThreadPool
{
    public class ThreadPoolBasic
    {
        private static readonly IDictionary<int, AProcessData> keyValuePairs = new Dictionary<int, AProcessData>();

        public static int fali { get; set; } = 0;

        public static int suss { get; set; } = 0;

        public double countFile = 0;
        public DateTime dateTimeStart = DateTime.Now;
        public int indexProcessData = 0;
        public void Process()
        {
            Console.OutputEncoding = Encoding.UTF8;

            for (var itemThreadPool = 0; itemThreadPool < 10; itemThreadPool++)
            {
                // cài đặt luồng
                System.Threading.ThreadPool.SetMinThreads(40, 10);
                System.Threading.ThreadPool.SetMaxThreads(64, 10);
                // khởi tạo class lưu trữu
                AProcessData aProcessData = new AProcessData();
                System.Threading.ThreadPool.QueueUserWorkItem(WorkItem, indexProcessData);
                indexProcessData++;
                while (System.Threading.ThreadPool.PendingWorkItemCount != 0)
                {
                    Console.WriteLine($"Đang sử lý : {System.Threading.ThreadPool.ThreadCount} | "
                    + $"Đã gửi : {System.Threading.ThreadPool.CompletedWorkItemCount} | "
                    + $"Hàng đợi : {System.Threading.ThreadPool.PendingWorkItemCount} | "
                    + $"Thất bại : {fali} | "
                    + $"Thành công : {suss} | "
                    + $"Hoàn thành : { Convert.ToDouble((suss + fali) * 100) / Convert.ToDouble(0):0.00}% | "
                    + $"Thời gian : {DateTime.UtcNow.Subtract(dateTimeStart)} ms" );
                }
            }
        }

        static void WorkItem(object o)
        {
            int index = (int)o;
            var dataReturn = keyValuePairs[index].ProcessSendSms(index);
            HttpResponseMessage httpResponseMessage;
            Task.Run(async () =>
            {
                //httpResponseMessage = await configConnextHttp.Post<SendSms>("SendTest", dataReturn);
                //if ((int)httpResponseMessage.StatusCode >= 400)
                //{
                //    fali++;
                //}
                //else
                //{
                //    suss++;
                //}
            }).Wait();
        }

        public static string EncryptDataInAES256ToKey(string textData, string Encryptionkey)
        {

            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            //Final transform the test string.
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        public static string DecryptAES256ToData(string EncryptedText, string Encryptionkey)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            objrij.Mode = CipherMode.CBC;
            objrij.Padding = PaddingMode.PKCS7;
            objrij.KeySize = 0x80;
            objrij.BlockSize = 0x80;
            byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            byte[] EncryptionkeyBytes = new byte[0x10];
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(TextByte);  //it will return readable string
        }
    }
}
