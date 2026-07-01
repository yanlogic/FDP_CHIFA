using System.IO;

namespace Chifa.Entidades
{
    public class Pedido
    {
        public int Mesa { get; set; }
        public Plato PlatoPedido { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
        public string Estado { get; set; }

        public Pedido(int mesa, Plato plato, int cantidad)
        {
            Mesa = mesa;
            PlatoPedido = plato;
            Cantidad = cantidad;
            Subtotal = cantidad * plato.Precio;
            Estado = "pendiente";
        }

        // RF06: Validar datos
        public static bool ValidarDatos(double precio, int cantidad)
        {
            return precio > 0 && cantidad > 0;
        }
    }
}