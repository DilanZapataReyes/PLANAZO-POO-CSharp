namespace PLANAZO_POO_CSharp.Modelos
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public string NombrePersona { get; set; }
        public Evento EventoReservado { get; set; }
        public int CantidadEntradas { get; set; }
        public decimal ValorTotal { get; set; }
        public bool EstaCancelada { get; set; }

        public Reserva(int idReserva, string nombrePersona, Evento eventoReservado, int cantidadEntradas)
        {
            IdReserva = idReserva;
            NombrePersona = nombrePersona;
            EventoReservado = eventoReservado;
            CantidadEntradas = cantidadEntradas;
            // Calculamos el valor total multiplicando las entradas por el precio final polimórfico del evento
            ValorTotal = cantidadEntradas * eventoReservado.CalcularPrecioFinal();
            EstaCancelada = false; // Al crearla, la reserva está activa
        }

        public override string ToString()
        {
            string estado = EstaCancelada ? "Cancelada" : "Activa";
            return $"[Reserva #{IdReserva}] - Persona: {NombrePersona} - Evento: {EventoReservado.Nombre} - Entradas: {CantidadEntradas} - Total: ${ValorTotal:N0} - Estado: {estado}";
        }
    }
}