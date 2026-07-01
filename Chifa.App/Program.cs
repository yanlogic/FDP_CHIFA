using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chifa.Logica;
using Chifa.Entidades;

namespace Chifa.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Sistema Chifa Fa";
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
                        case 0: resultado = gestor.RegistrarNuevoPedido(); break;  // RF01
                        case 1: resultado = gestor.ConsultarCarta(); break;  // RF02
                        case 2: resultado = gestor.CobrarMesa(); break;  // RF03
                        case 3: resultado = gestor.ModificarPedido(); break;  // RF04
                        case 4: resultado = gestor.MostrarReporteDiario(); break;  // RF05
                        case 5: resultado = GestorArchivos.LeerHistorialTXT(); break;
                        case 6: resultado = gestor.ListarPedidos(); break;  // RF07
                        case 7: salir = true; break;
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
                    Console.Clear();
                    ClaseInterfaz.DibujarMarcoBase();
                    gestor.Simular50Registros();
                    Console.ReadKey(true);
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n  Gracias por usar Sistema Chifa Fa. ¡Hasta pronto!");
            Console.ResetColor();
            System.Threading.Thread.Sleep(1500);
        }
    }
}