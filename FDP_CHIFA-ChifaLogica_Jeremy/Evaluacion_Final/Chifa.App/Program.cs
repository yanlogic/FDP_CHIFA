using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chifa.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aquí irá el programa principal
            // -- Configuración visual del sistema
            Console.Title = "Sistema Chifa Fa v2.0";
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try { Console.SetWindowSize(90, 42); } catch { }
        }
    }
}