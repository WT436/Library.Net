using File;
using Security.Algorithm;
using System;

namespace ConsoleCoreTest
{
    class Program
    {
        static void Main(string[] args)
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
