using System;
using DomainModel.Models;
using ServiceLayer.Implementations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serv = new ApplicationSettingImplementation();

            serv.Add(new ApplicationSetting());

            var settings = serv.List();

            Console.WriteLine(settings[0].Value + " " +  settings[0].Value);
        }
    }
}
