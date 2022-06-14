using HttpContextExtention;
using ServerAutoRuntimesBasic.Domain.Shared.DataTransfer;
using ServerAutoRuntimesBasic.Domain.Shared.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerAutoRuntimesBasic.Service
{
    public class ServiceSpamRoot
    {
        private static IDictionary<int, FuncCallback> keyValuePairs = new Dictionary<int, FuncCallback>();

        public static int fali { get; set; } = 0;
        public static int suss { get; set; } = 0;

        public static async Task Processor()
        {
            try
            {
                var dataInput = (new List<SendSms>
                {
                    new SendSms(){Destination = "Hainam",Sender = "9x",Keyword = "1999",ShortMessage="Hello"}
                }).ToArray();
                int indexProcessData = 0;
                var dateTimeStart = DateTime.UtcNow;
                int countFile = dataInput.Length;

                if (countFile != 0)
                {
                    for (var itemThreadPool = 0; itemThreadPool < dataInput.Length; itemThreadPool++)
                    {
                        // cài đặt luồng                  
                        ThreadPool.SetMinThreads(40, 10);
                        ThreadPool.SetMaxThreads(64, 10);
                        // khởi tạo class lưu trữ
                        FuncCallback aProcessData = new FuncCallback();
                        aProcessData.AProcessDataSetUp(dataInput[itemThreadPool], indexProcessData);
                        keyValuePairs.Add(indexProcessData, aProcessData);
                        ThreadPool.QueueUserWorkItem(WorkItem, indexProcessData);
                        indexProcessData++;
                        while (ThreadPool.PendingWorkItemCount != 0)
                        {
                            Console.WriteLine($"Đang sử lý : {ThreadPool.ThreadCount} | "
                                + $"Đã gửi : {ThreadPool.CompletedWorkItemCount} | "
                                + $"Hàng đợi : {ThreadPool.PendingWorkItemCount} | "
                                + $"Thất bại : {fali} | "
                                + $"Thành công : {suss} | "
                                + $"Hoàn thành : { Convert.ToDouble((suss + fali) * 100) / Convert.ToDouble(countFile):0.00}% | "
                                + $"Thời gian : {DateTime.UtcNow.Subtract(dateTimeStart)} ms"
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }

        static void WorkItem(object o)
        {
            int index = (int)o;
            var dataReturn = keyValuePairs[index].ProcessSendSms(index);
            HttpResponseMessage httpResponseMessage;
            Task.Run(async () =>
            {
                // httpResponseMessage = await configConnextHttp.Post<SendSms>("SendTest", dataReturn);

            }).Wait();
        }

    }

    public class FuncCallback
    {
        private int _index { get; set; }
        private SendSms _sendSms { get; set; }

        public void AProcessDataSetUp(SendSms sendSms, int index)
        {
            _sendSms = sendSms;
            _index = index;
        }

        public SendSms ProcessSendSms(int index)
        {
            SendSms sendSmsInline = new SendSms();

            if (_sendSms != null)
            {
                sendSmsInline = _sendSms;
            }
            else
            {
                // map data
            }

            return sendSmsInline;
        }
    }
}
