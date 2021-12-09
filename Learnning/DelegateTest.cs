using System;
using System.Collections.Generic;
using System.Text;

namespace Learnning
{
    public class DelegateTest
    {
        public delegate int Mydelegete(string a);
        public void ShowAll()
        {
            Mydelegete convertInt = new Mydelegete(ConvertStringToInt);

            string num = "250";
            // delegate
            convertInt(num);
            //multicast
            Mydelegete showInt = new Mydelegete(ShowInt);
            Mydelegete multicast = convertInt + showInt;

            multicast(num);

            // callback function

            CallbackName(showInt);
        }
        public int ConvertStringToInt(string a)
        {
            int trave = Convert.ToInt32(a);
            Console.WriteLine("convertStringToInt : " + trave);
            return trave;
        }

        public int ShowInt(string a)
        {
            int trave = Convert.ToInt32(a);
            Console.WriteLine("Show : " + trave);
            return trave;
        }

        public void CallbackName(Mydelegete Dg)
        {
            string num = "250";
            Dg(num);
        }
    }
}
