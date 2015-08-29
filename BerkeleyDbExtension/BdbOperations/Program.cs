using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeleyDbExtension.Core;
using BerkeleyDbExtension.Translators;
using BerkeleyDbExtension.Intefaces;

namespace BdbOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\test.bdb";

            var connection = new BerkeleyDbExtension.Core.Connection<string, string>(path, new GenericTranslator<string, string>());
            connection.Open();
            connection.Add("Hari", "Prasad");
            connection.Add("Thenshi", "Paidipati");

            Console.WriteLine(connection["Thenshi"]);
            Console.ReadKey();
        }
    }
}
