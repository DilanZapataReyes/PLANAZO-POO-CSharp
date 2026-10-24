namespace PLANAZO_POO_CSharp.Modelos
{
    public class Taller : Evento
    {
        public Taller(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima)
        {
        }

        public override decimal CalcularPrecioFinal()
        {
            // Aplica descuento del 10% (multiplica por 0.90)
            return PrecioBase * 0.90m;
        }
    }
}