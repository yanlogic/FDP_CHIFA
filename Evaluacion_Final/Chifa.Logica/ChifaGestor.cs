using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chifa.Logica
{
    public class ChifaGestor
    {
        public class ChifaGestor
        {
            private List<Plato> cartaPlatos = new List<Plato>();
            private List<Pedido> listaPedidos = new List<Pedido>();
            private double totalVentasDia = 0;
            private int totalBoletas = 0;
            private int totalFacturas = 0;

            public ChifaGestor()
            {
                InicializarCarta();
            }

            // ───────────────────────────────────────── 
            private void InicializarCarta()
            {
                cartaPlatos.Add(new Plato("CH01", "Arroz chaufa personal", 12.50, "Arroz"));
                cartaPlatos.Add(new Plato("CH02", "Arroz chaufa familiar", 25.00, "Arroz"));
                cartaPlatos.Add(new Plato("CH03", "Chaufa de mariscos", 18.00, "Arroz"));
                cartaPlatos.Add(new Plato("LO01", "Lomo saltado", 21.00, "Saltados"));
                cartaPlatos.Add(new Plato("PO01", "Pollo a la plancha", 16.00, "Pollos"));
                cartaPlatos.Add(new Plato("BE01", "Gaseosa personal", 4.00, "Bebidas"));
            }

            // ══════════════════════════════════════════
            //  HELPERS DE ENTRADA
            // ══════════════════════════════════════════
            public static string LeerTextoCajaGris(int x, int y, int longitud)
            {
                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(new string(' ', longitud));
                Console.SetCursorPosition(x, y);
                Console.CursorVisible = true;

                string texto = "";
                while (true)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);
                    if (tecla.Key == ConsoleKey.Escape)
                    {
                        Console.ResetColor();
                        Console.CursorVisible = false;
                        return null;
                    }
                    if (tecla.Key == ConsoleKey.Enter && texto.Trim().Length > 0) break;
                    if (tecla.Key == ConsoleKey.Backspace && texto.Length > 0)
                    {
                        texto = texto.Substring(0, texto.Length - 1);
                        Console.SetCursorPosition(x + texto.Length, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x + texto.Length, y);
                    }
                    else if (texto.Length < longitud && !char.IsControl(tecla.KeyChar))
                    {
                        texto += tecla.KeyChar;
                        Console.Write(tecla.KeyChar);
                    }
                }
                Console.ResetColor();
                Console.CursorVisible = false;
                return texto.Trim();
            }

            public static int LeerEnteroCajaGris(int x, int y, int longitud, int min, int max)
            {
                while (true)
                {
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Black;
                    string input = LeerTextoCajaGris(x, y, longitud);
                    if (input == null) return -1;
                    if (int.TryParse(input, out int valor) && valor >= min && valor <= max)
                        return valor;
                    MostrarErrorTemporal(x, y + 1, $"  Error: ingrese un número entre {min} y {max}  ");
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(new string(' ', longitud));
                    Console.ResetColor();
                }
            }

            public static void MostrarErrorTemporal(int x, int y, string mensaje)
            {
                Console.SetCursorPosition(x, y);

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(mensaje);
                System.Threading.Thread.Sleep(1500);
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', mensaje.Length));
                Console.ResetColor();
            }

            // ══════════════════════════════════════════
            //  RF01 – registrarPedido() 
            // ══════════════════════════════════════════
            public bool RegistrarNuevoPedido()
            {
                ClaseInterfaz.DibujarTituloFormulario("REGISTRO DE NUEVO PEDIDO");

                ClaseInterfaz.EscribirEtiqueta(5, 11, "\u250C" + new string('\u2500', 50) + "\u2510");
                ClaseInterfaz.EscribirEtiqueta(5, 12, "\u2502  INGRESE LOS DATOS DEL PEDIDO                  \u2502");
                ClaseInterfaz.EscribirEtiqueta(5, 13, "\u2514" + new string('\u2500', 50) + "\u2518");

                ClaseInterfaz.EscribirEtiqueta(8, 15, "\u25B6 Número de Mesa   :");
                ClaseInterfaz.EscribirEtiqueta(8, 17, "\u25B6 Código del Plato :");
                ClaseInterfaz.EscribirEtiqueta(8, 19, "\u25B6 Cantidad         :");

                Console.SetCursorPosition(5, 25);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  [ESC] Cancelar y volver al menú principal");
                Console.ResetColor();

                // RF06: validarDatos() - entero positivo
                int mesa = LeerEnteroCajaGris(32, 15, 30, 1, 20);
                if (mesa == -1) return false;

                string cod;
                Plato platoEncontrado = null;
                while (true)
                {
                    cod = LeerTextoCajaGris(32, 17, 30);
                    if (cod == null) return false;
                    cod = cod.ToUpper();
                    foreach (Plato p in cartaPlatos)
                        if (p.Codigo == cod) { platoEncontrado = p; break; }
                    if (platoEncontrado != null) break;
                    MostrarErrorTemporal(32, 18, "  Código no existe en la carta  ");
                    Console.SetCursorPosition(32, 17);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(new string(' ', 30));
                    Console.ResetColor();
                }

                // Mostrar nombre y precio del plato encontrado
                Console.SetCursorPosition(32, 18);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\u2714 " + platoEncontrado.Nombre + " - S/ " + platoEncontrado.Precio.ToString("0.00"));
                Console.ResetColor();

                // RF06: validarDatos() - cantidad positiva
                int cant = LeerEnteroCajaGris(32, 19, 30, 1, 50);
                if (cant == -1) return false;

                double subtotal = cant * platoEncontrado.Precio;

                // Mostrar resumen antes de confirmar
                ClaseInterfaz.DibujarSeparador(21);
                Console.SetCursorPosition(8, 22);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  Mesa {mesa}  |  {platoEncontrado.Nombre} x{cant}  |  Subtotal: S/ {subtotal:0.00}");
                Console.ResetColor();

                ClaseInterfaz.EscribirEtiqueta(8, 24, "\u25B6 Confirmar (1=Sí / 2=No) :");
                int confirmar = LeerEnteroCajaGris(38, 24, 10, 1, 2);
                if (confirmar == -1 || confirmar == 2) return false;

                listaPedidos.Add(new Pedido(mesa, platoEncontrado, cant));
                ClaseInterfaz.MostrarExito(26, $"Pedido guardado: {platoEncontrado.Nombre} x{cant} = S/ {subtotal:0.00}");
                Console.SetCursorPosition(0, 28);
                return true;
            }

            // ══════════════════════════════════════════
            //  RF02 – buscarPlato()  →  ConsultarCarta 
            // ══════════════════════════════════════════
            public bool ConsultarCarta()
            {
                ClaseInterfaz.DibujarTituloFormulario("CARTA DE PLATOS");

                int fila = 11;
                ClaseInterfaz.EscribirEtiqueta(5, fila, $"  {"CÓDIGO",-8} {"PLATO",-30} {"PRECIO",10}  CATEGORÍA");
                fila++;
                ClaseInterfaz.DibujarSeparador(fila); fila++;

                string catActual = "";
                foreach (Plato plato in cartaPlatos)
                {
                    if (plato.Categoria != catActual)
                    {
                        catActual = plato.Categoria;
                        fila++;
                        Console.SetCursorPosition(5, fila);
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"  [ {catActual.ToUpper()} ]  ");
                        Console.ResetColor();
                        fila++;
                    }
                    Console.SetCursorPosition(5, fila);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  {plato.Codigo,-8}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {plato.Nombre,-30}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" S/ {plato.Precio,6:0.00}");
                    Console.ResetColor();
                    fila++;
                }
                Console.SetCursorPosition(0, fila + 1);
                return true;
            }

            // ══════════════════════════════════════════
            //  RF03 – calcularTotalMesa()  →  CobrarMesa
            // ══════════════════════════════════════════
            public bool CobrarMesa()
            {
                ClaseInterfaz.DibujarTituloFormulario("COBRAR MESA");

                ClaseInterfaz.EscribirEtiqueta(8, 11, "\u25B6 Número de Mesa a Cobrar :");
                Console.SetCursorPosition(5, 25);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  [ESC] Cancelar y volver");
                Console.ResetColor();

                int mesaCobrar = LeerEnteroCajaGris(38, 11, 25, 1, 20);
                if (mesaCobrar == -1) return false;

                List<Pedido> pedidosMesa = new List<Pedido>();
                foreach (Pedido p in listaPedidos)
                    if (p.Mesa == mesaCobrar && p.Estado == "pendiente")
                        pedidosMesa.Add(p);

                if (pedidosMesa.Count == 0)
                {
                    ClaseInterfaz.MostrarAlerta(13, $"No hay pedidos pendientes para Mesa {mesaCobrar}.");
                    Console.SetCursorPosition(0, 15);
                    return true;
                }

                // RF03: sumar subtotales de todos los ítems de la mesa
                double total = 0;
                int fila = 13;
                ClaseInterfaz.DibujarSeparador(fila); fila++;

                Console.SetCursorPosition(8, fila);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  CUENTA \u2014 MESA {mesaCobrar}");
                Console.ResetColor(); fila++;
                ClaseInterfaz.DibujarSeparador(fila); fila++;

                foreach (Pedido ped in pedidosMesa)
                {
                    Console.SetCursorPosition(8, fila);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  {ped.PlatoPedido.Nombre,-30} x{ped.Cantidad}  =  S/ {ped.Subtotal:0.00}");
                    Console.ResetColor();
                    total += ped.Subtotal;
                    fila++;
                }
                ClaseInterfaz.DibujarSeparador(fila); fila++;
                Console.SetCursorPosition(8, fila);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  TOTAL A PAGAR: S/ {total:0.00}  ");
                Console.ResetColor(); fila += 2;

                ClaseInterfaz.EscribirEtiqueta(8, fila, "\u25B6 Confirmar pago (1=Sí / 2=No) :");
                int confirma = LeerEnteroCajaGris(45, fila, 15, 1, 2);
                if (confirma == -1 || confirma == 2) return false;
                fila += 2;

                ClaseInterfaz.EscribirEtiqueta(8, fila, "\u25B6 Comprobante (1=Boleta / 2=Factura) :");
                int tipo = LeerEnteroCajaGris(51, fila, 15, 1, 2);
                if (tipo == -1) return false;

                string tipoNombre = tipo == 1 ? "Boleta" : "Factura";
                if (tipo == 1) totalBoletas++; else totalFacturas++;

                string texto = $"Mesa: {mesaCobrar}\nTipo: {tipoNombre}\nTOTAL: S/ {total:0.00}";
                GestorArchivos.GuardarComprobanteTXT(texto);
                foreach (Pedido ped in pedidosMesa) ped.Estado = "pagado";
                totalVentasDia += total;

                fila += 2;
                ClaseInterfaz.MostrarExito(fila, $"Mesa {mesaCobrar} cobrada. {tipoNombre} generada. Total: S/ {total:0.00}");
                Console.SetCursorPosition(0, fila + 2);
                return true;
            }

            // ══════════════════════════════════════════
            //  RF04 – modificarPedido()  ← NUEVO
            // ══════════════════════════════════════════
            public bool ModificarPedido()
            {
                ClaseInterfaz.DibujarTituloFormulario("EDITAR / ELIMINAR PEDIDO");

                ClaseInterfaz.EscribirEtiqueta(8, 11, "\u25B6 Número de Mesa :");
                Console.SetCursorPosition(5, 30);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("  [ESC] Cancelar y volver");
                Console.ResetColor();

                int mesa = LeerEnteroCajaGris(30, 11, 25, 1, 20);
                if (mesa == -1) return false;

                // Buscar pedidos pendientes de esa mesa
                List<Pedido> pedidosMesa = new List<Pedido>();
                foreach (Pedido p in listaPedidos)
                    if (p.Mesa == mesa && p.Estado == "pendiente")
                        pedidosMesa.Add(p);

                if (pedidosMesa.Count == 0)
                {
                    ClaseInterfaz.MostrarAlerta(13, $"No hay pedidos pendientes para Mesa {mesa}.");
                    Console.SetCursorPosition(0, 16);
                    return true;
                }

                // Mostrar lista numerada de pedidos
                int fila = 13;
                ClaseInterfaz.DibujarSeparador(fila); fila++;
                Console.SetCursorPosition(8, fila);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  PEDIDOS PENDIENTES \u2014 MESA {mesa}");
                Console.ResetColor(); fila++;
                ClaseInterfaz.DibujarSeparador(fila); fila++;

                for (int i = 0; i < pedidosMesa.Count; i++)
                {
                    Console.SetCursorPosition(8, fila);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"  [{i + 1}] ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{pedidosMesa[i].PlatoPedido.Nombre,-30} x{pedidosMesa[i].Cantidad}  S/ {pedidosMesa[i].Subtotal:0.00}");
                    Console.ResetColor();
                    fila++;
                }
                ClaseInterfaz.DibujarSeparador(fila); fila++;

                ClaseInterfaz.EscribirEtiqueta(8, fila, "\u25B6 Selecciona ítem # :");
                int seleccion = LeerEnteroCajaGris(32, fila, 10, 1, pedidosMesa.Count);
                if (seleccion == -1) return false;
                fila += 2;

                Pedido pedidoEditar = pedidosMesa[seleccion - 1];

                ClaseInterfaz.EscribirEtiqueta(8, fila, "\u25B6 Acción (1=Cambiar cantidad / 2=Eliminar) :");
                int accion = LeerEnteroCajaGris(55, fila, 10, 1, 2);
                if (accion == -1) return false;
                fila += 2;

                if (accion == 1)
                {
                    // Cambiar cantidad
                    ClaseInterfaz.EscribirEtiqueta(8, fila, $"\u25B6 Nueva cantidad para '{pedidoEditar.PlatoPedido.Nombre}' :");
                    int nuevaCant = LeerEnteroCajaGris(55, fila, 10, 1, 50);
                    if (nuevaCant == -1) return false;

                    pedidoEditar.Cantidad = nuevaCant;
                    pedidoEditar.Subtotal = nuevaCant * pedidoEditar.PlatoPedido.Precio;
                    fila += 2;
                    ClaseInterfaz.MostrarExito(fila,
                        $"Cantidad actualizada: x{nuevaCant}  Nuevo subtotal: S/ {pedidoEditar.Subtotal:0.00}");
                }
                else
                {
                    // Eliminar el pedido
                    listaPedidos.Remove(pedidoEditar);
                    ClaseInterfaz.MostrarExito(fila,
                        $"Pedido '{pedidoEditar.PlatoPedido.Nombre}' eliminado de Mesa {mesa}.");
                }

                Console.SetCursorPosition(0, fila + 2);
                return true;
            }

            // ══════════════════════════════════════════
            //  RF05 – generarReporteDia()
            // ══════════════════════════════════════════
            public bool MostrarReporteDiario()
            {
                ClaseInterfaz.DibujarTituloFormulario("REPORTE DIARIO DE VENTAS");

                int fila = 11;
                ClaseInterfaz.EscribirEtiqueta(5, fila, "\u250C" + new string('\u2500', 55) + "\u2510"); fila++;
                ClaseInterfaz.EscribirEtiqueta(5, fila, "\u2502  RESUMEN DEL DÍA                                       \u2502"); fila++;
                ClaseInterfaz.EscribirEtiqueta(5, fila, "\u251C" + new string('\u2500', 55) + "\u2524"); fila++;

                // Recorrer el arreglo y sumar totales
                double sumaTotal = 0;
                foreach (Pedido p in listaPedidos)
                    if (p.Estado == "pagado")
                        sumaTotal += p.Subtotal;

                Console.SetCursorPosition(5, fila);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\u2502  Total vendido hoy   : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"S/ {totalVentasDia,10:0.00}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(' ', 19) + "\u2502"); fila++;

                Console.SetCursorPosition(5, fila);
                Console.Write($"\u2502  Boletas emitidas    : {totalBoletas,-10}{new string(' ', 21)}\u2502"); fila++;
                Console.SetCursorPosition(5, fila);
                Console.Write($"\u2502  Facturas emitidas   : {totalFacturas,-10}{new string(' ', 21)}\u2502"); fila++;
                Console.SetCursorPosition(5, fila);
                Console.Write($"\u2502  Pedidos registrados : {listaPedidos.Count,-10}{new string(' ', 21)}\u2502"); fila++;
                Console.ResetColor();

                ClaseInterfaz.EscribirEtiqueta(5, fila, "\u2514" + new string('\u2500', 55) + "\u2518"); fila += 2;

                // Mostrar frase de cierre
                Console.SetCursorPosition(5, fila);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"  \u2605  Total vendido: S/ {totalVentasDia:0.00}");
                Console.ResetColor(); fila++;

                Console.SetCursorPosition(0, fila + 2);
                return true;
            }

            // ══════════════════════════════════════════
            //  RF07 – listarPedidos()  ← NUEVO  (Burbuja)
            // ══════════════════════════════════════════
            public bool ListarPedidos()
            {
                ClaseInterfaz.DibujarTituloFormulario("LISTADO DE ÓRDENES (ORDENADO POR MESA)");

                if (listaPedidos.Count == 0)
                {
                    ClaseInterfaz.MostrarAlerta(12, "No hay pedidos registrados todavía.");
                    Console.SetCursorPosition(0, 15);
                    return true;
                }

                // ── Ordenamiento Burbuja por número de mesa ──
                List<Pedido> copia = new List<Pedido>(listaPedidos);
                int n = copia.Count;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (copia[j].Mesa > copia[j + 1].Mesa)
                        {
                            Pedido temp = copia[j];
                            copia[j] = copia[j + 1];
                            copia[j + 1] = temp;
                        }
                    }
                }

                // Encabezado de tabla
                int fila = 11;
                Console.SetCursorPosition(5, fila);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  {"MESA",4}  {"PLATO",-28}  {"CANT",4}  {"SUBTOTAL",10}  {"ESTADO",-10}  ");
                Console.ResetColor(); fila++;

                ClaseInterfaz.DibujarSeparador(fila); fila++;

                int mesaActual = -1;
                foreach (Pedido p in copia)
                {
                    if (fila >= 36) break;

                    // Separador de mesa
                    if (p.Mesa != mesaActual)
                    {
                        mesaActual = p.Mesa;
                        Console.SetCursorPosition(5, fila);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"  \u25CF  Mesa {p.Mesa}  ");
                        Console.ResetColor();
                        fila++;
                    }

                    Console.SetCursorPosition(5, fila);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"  {"",4}  {p.PlatoPedido.Nombre,-28}  {p.Cantidad,4}  S/ {p.Subtotal,7:0.00}  ");
                    if (p.Estado == "pendiente")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("PENDIENTE ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("PAGADO    ");
                    }
                    Console.ResetColor();
                    fila++;
                }

                ClaseInterfaz.DibujarSeparador(fila); fila++;
                Console.SetCursorPosition(5, fila);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"  Total de pedidos mostrados: {copia.Count}");
                Console.ResetColor();

                Console.SetCursorPosition(0, fila + 2);
                return true;
            }

            // ══════════════════════════════════════════
            //  SIMULAR 50 REGISTROS
            // ══════════════════════════════════════════
            public bool Simular50Registros()
            {
                ClaseInterfaz.DibujarTituloFormulario("SIMULACIÓN DE REGISTROS");

                Console.SetCursorPosition(8, 12);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  Generando 50 registros simulados...");
                Console.ResetColor();

                Random rnd = new Random();
                for (int i = 0; i < 50; i++)
                    listaPedidos.Add(new Pedido(rnd.Next(1, 21), cartaPlatos[rnd.Next(cartaPlatos.Count)], rnd.Next(1, 4)));

                ClaseInterfaz.MostrarExito(14, "¡50 registros generados con éxito!");
                Console.SetCursorPosition(0, 17);
                return true;
            }
        }
    }
}