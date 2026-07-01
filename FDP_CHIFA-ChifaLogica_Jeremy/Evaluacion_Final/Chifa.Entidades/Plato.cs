namespace Chifa.Entidades
{
    public class Plato
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Categoria { get; set; }

        public Plato(string codigo, string nombre, double precio, string categoria)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Categoria = categoria;
        }
    }
}
