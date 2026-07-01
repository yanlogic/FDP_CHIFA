using System;
using System.IO;

namespace Chifa.Logica
{
    public static class GestorArchivos
    {
        private static string rutaArchivo = "Registro_Ventas_Chifa.txt";

        public static void GuardarComprobanteTXT(string contenido)
        {
            string encabezado = "\n========================================\n" +
                                "COMPROBANTE - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n" +
                                "========================================\n";
            File.AppendAllText(rutaArchivo, encabezado + contenido + "\n");
        }

        public static bool LeerHistorialTXT()
        {
            ClaseInterfaz.DibujarTituloFormulario("HISTORIAL DE COMPROBANTES");

            int fila = 11;
            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllText(rutaArchivo).Split('\n');
                foreach (string linea in lineas)
                {
                    if (fila >= 36) break;
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        Console.SetCursorPosition(5, fila);
                        if (linea.StartsWith("==="))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (linea.StartsWith("COMPROBANTE"))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(linea.Length > 79 ? linea.Substring(0, 79) : linea);
                        Console.ResetColor();
                        fila++;
                    }
                }
            }
            else
            {
                ClaseInterfaz.MostrarAlerta(fila, "Aún no hay registros guardados en el archivo TXT.");
                fila++;
            }

            Console.SetCursorPosition(0, fila + 2);
            return true;
        }
    }
}
