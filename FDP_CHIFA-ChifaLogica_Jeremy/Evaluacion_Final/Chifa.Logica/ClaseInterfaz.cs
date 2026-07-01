using System;

namespace Chifa.Logica
{
    public class ClaseInterfaz
    {
        public static string[] OpcionesMenu =
        {
            " PEDIDO ", " CARTA ", " COBRAR ", " EDITAR ", " REPORTE ", " HISTORIAL ", " LISTAR ", " SALIR "
        };

        // ══════════════════════════════════════════
        //  CABECERA PRINCIPAL 
        // ══════════════════════════════════════════
        public static void DibujarCabecera()
        {
            // Fondo rojo oscuro para la cabecera
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', 90));

            // Línea de subtítulo con ícono
            Console.SetCursorPosition(0, 1);
            string subtitulo = "  \u2605  RESTAURANTE CHIFA FA  \u2605";
            int esp = (90 - subtitulo.Length) / 2;
            Console.Write(new string(' ', esp) + subtitulo + new string(' ', 90 - esp - subtitulo.Length));

            Console.SetCursorPosition(0, 2);
            Console.Write(new string(' ', 90));

            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  MARCO BASE
        // ══════════════════════════════════════════
        public static void DibujarMarcoBase()
        {
            DibujarCabecera();

            // Bordes laterales en rojo
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int i = 3; i < 40; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write(" ");
                Console.SetCursorPosition(89, i); Console.Write(" ");
            }

            // Línea separadora bajo la cabecera
            Console.SetCursorPosition(1, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(new string('\u2550', 88));
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  PESTAÑOS DEL MENÚ
        // ══════════════════════════════════════════
        public static void DibujarPestanas(int seleccionado)
        {
            Console.SetCursorPosition(2, 4);
            for (int i = 0; i < OpcionesMenu.Length; i++)
            {
                if (i == seleccionado)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write(OpcionesMenu[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
            }
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  MARCO DINÁMICO CONTENIDO
        // ══════════════════════════════════════════
        public static void DibujarMarcoDinamico(int lineaFinal)
        {
            int limite = lineaFinal + 1;
            if (limite < 38) limite = 38;

            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int i = 5; i < limite; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write(" ");
                Console.SetCursorPosition(89, i); Console.Write(" ");
            }
            Console.SetCursorPosition(0, limite);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(new string('\u2550', 90));
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  TÍTULO DE FORMULARIO
        // ══════════════════════════════════════════
        public static void DibujarTituloFormulario(string titulo)
        {
            // Barra de título con fondo rojo
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            int espacios = (86 - titulo.Length) / 2;
            string centrado = new string(' ', espacios) + titulo + new string(' ', 86 - espacios - titulo.Length);
            Console.SetCursorPosition(2, 7);
            Console.Write(centrado);
            Console.ResetColor();

            // Línea decorativa bajo el título
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(2, 8);
            Console.Write(new string('\u2500', 86));
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  SEPARADOR HORIZONTAL
        // ══════════════════════════════════════════
        public static void DibujarSeparador(int fila)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(5, fila);
            Console.Write(new string('\u2508', 80));
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  MENSAJE DE ÉXITO
        // ══════════════════════════════════════════
        public static void MostrarExito(int fila, string msg)
        {
            Console.SetCursorPosition(5, fila);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  \u2714  " + msg + "  ");
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  MENSAJE DE ALERTA
        // ══════════════════════════════════════════
        public static void MostrarAlerta(int fila, string msg)
        {
            Console.SetCursorPosition(5, fila);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("  \u26A0  " + msg + "  ");
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  ETIQUETA DE CAMPO
        // ══════════════════════════════════════════
        public static void EscribirEtiqueta(int x, int y, string texto)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(texto);
            Console.ResetColor();
        }

        // ══════════════════════════════════════════
        //  MENSAJE DE NAVEGACIÓN
        // ══════════════════════════════════════════
        public static void MostrarNavegacion()
        {
            Console.SetCursorPosition(2, 5);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  \u25C4 \u25BA  Navegar   [ENTER] Seleccionar   [ESC] Cancelar");
            Console.ResetColor();
        }
    }
}

