namespace PLANAZO_POO_CSharp.Modelos
{
    public abstract class Evento
    {
        // Encapsulamiento con propiedades protegidas/públicas
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioBase { get; set; }
        public int CapacidadMaxima { get; set; }
        public int CuposDisponibles { get; set; }

        protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima; // Al iniciar, los cupos disponibles son el total de la capacidad
        }

        // Método abstracto que obligará a las clases hijas a implementar su propio cálculo (Polimorfismo)
        public abstract decimal CalcularPrecioFinal();

        public override string ToString()
        {
            return $"[{Id}] {Nombre} - Fecha: {Fecha.ToShortDateString()} - Precio Base: ${PrecioBase:N0} - Cupos: {CuposDisponibles}/{CapacidadMaxima}";
        }
    }
}