using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chifa.Logica;

namespace Chifa.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // -- Configuración visual del sistema
            Console.Title = "Sistema Chifa Fa v2.0";
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try { Console.SetWindowSize(90, 42); } catch { }

            ChifaGestor gestor = new ChifaGestor();
            int opcionActual = 0;
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                ClaseInterfaz.DibujarMarcoBase();
                ClaseInterfaz.DibujarPestanas(opcionActual);
                ClaseInterfaz.MostrarNavegacion();

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.RightArrow)
                {
                    opcionActual = (opcionActual + 1) % ClaseInterfaz.OpcionesMenu.Length;
                }
                else if (tecla.Key == ConsoleKey.LeftArrow)
                {
                    opcionActual = (opcionActual - 1 + ClaseInterfaz.OpcionesMenu.Length)
                                   % ClaseInterfaz.OpcionesMenu.Length;
                }
                else if (tecla.Key == ConsoleKey.Enter || tecla.Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    ClaseInterfaz.DibujarMarcoBase();
                    ClaseInterfaz.DibujarPestanas(opcionActual);
                    ClaseInterfaz.MostrarNavegacion();

                    bool resultado = false;
                    switch (opcionActual)
                    {
                        //Aquí irán los resultados de las opciones seleccionadas
                    }

                    if (!salir && resultado)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("\n  Presiona cualquier tecla para continuar...");
                        Console.ResetColor();
                        Console.ReadKey(true);
                    }
                }
                else if (tecla.Key == ConsoleKey.Escape)
                {
                    salir = true;
                }
                else if (tecla.KeyChar == '0')
                {
                    // Tecla oculta: simular 50 registros
                }
            }
        }
    }
}