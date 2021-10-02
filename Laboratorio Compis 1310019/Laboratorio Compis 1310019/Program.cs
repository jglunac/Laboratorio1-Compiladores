using System;

namespace Laboratorio_Compis_1310019
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            do
            {

                Console.WriteLine("Ingrese la operación a realizar:");
                Parser parser = new Parser();
                string input = Console.ReadLine();
                double result = parser.Parse(input);
                Console.WriteLine(result.ToString());

                Console.WriteLine("¿Desea continuar?");
                Console.WriteLine("Y/N");
                string opt = Console.ReadLine();
                switch (opt.ToLower())
                {
                    case "y":
                        exit = false;
                        break;
                    case "n":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Input no interpretado, saliendo.");
                        exit = true;
                        break;
                }
            } while (!exit);
        }
    }
}
