using File;
using HttpContextExtention;
using Security.Algorithm;
using System;
using System.Threading.Tasks;

namespace ConsoleCoreTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpContextExtention.HttpContextExtention httpContextExtention = new HttpContextExtention.HttpContextExtention("https://localhost:44323/");
            var data = await httpContextExtention.GetAsync("/api/Home/MethodGet");
            Console.WriteLine(data);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private void AES256Process()
        {
            AES256 aES256 = new AES256();
            string text = System.IO.File.ReadAllText(@"F:\Program\Library.Net\TextFile1.txt");
            var en = aES256.EncryptAES256ToString(text, "c318ed1b7724f1a7caad353c22af9d12");
            Console.WriteLine(en);
            var de = aES256.DecryptAES256ToString(en, "c318ed1b7724f1a7caad353c22af9d12");
            Console.WriteLine(de);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
