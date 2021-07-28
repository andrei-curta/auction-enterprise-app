using System;
using ServiceLayer.Implementations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serv = new ApplicationSettingImplementation();


            var settings = serv.List();

            Console.WriteLine(settings[0].Value + " " +  settings[0].Value);
        }
    }
}
