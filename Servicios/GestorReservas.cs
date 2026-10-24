using System;
using System.Collections.Generic;
using System.Linq;
using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios
{
    public class GestorReservas
    {
        private List<Evento> eventos;
        private List<Reserva> reservas;
        private int contadorIdReserva;

        public GestorReservas()
        {
            eventos = new List<Evento>();
            reservas = new List<Reserva>();
            contadorIdReserva = 1;
            InicializarEventos();
        }

        private void InicializarEventos()
        {
            // Registramos al menos 6 eventos: 2 películas (Cine), 2 conciertos y 2 talleres[cite: 1]
            eventos.Add(new Cine(1, "Avengers: Endgame", DateTime.Now.AddDays(5), 15000m, 100));
            eventos.Add(new Cine(2, "Interestelar (Reestreno)", DateTime.Now.AddDays(7), 18000m, 80));
            
            eventos.Add(new Concierto(3, "Festival Rock al Parque", DateTime.Now.AddDays(15), 60000m, 200));
            eventos.Add(new Concierto(4, "Concierto Sinfónico de Verano", DateTime.Now.AddDays(20), 80000m, 150));
            
            eventos.Add(new Taller(5, "Taller de Programación en C#", DateTime.Now.AddDays(10), 40000m, 30));
            eventos.Add(new Taller(6, "Taller de Fotografía Digital", DateTime.Now.AddDays(12), 35000m, 25));

            eventos.Add(new EventoDeportivo(7, "Partido Clasico de Futbol", DateTime.Now.AddDays(18), 50000m, 300));
        }

        public void MostrarEventosDisponibles()
        {
            Console.WriteLine("\n================================");
            Console.WriteLine("      EVENTOS DISPONIBLES       ");
            Console.WriteLine("================================");
            foreach (var evento in eventos)
            {
                decimal precioFinal = evento.CalcularPrecioFinal();
                Console.WriteLine($"ID: {evento.Id} | {evento.Nombre} ({evento.GetType().Name})");
                Console.WriteLine($"   Fecha: {evento.Fecha.ToShortDateString()} | Cupos: {evento.CuposDisponibles}/{evento.CapacidadMaxima}");
                Console.WriteLine($"   Precio Final: ${precioFinal:N0} (Base: ${evento.PrecioBase:N0})");
                Console.WriteLine("--------------------------------");
            }
        }

        public void ConsultarEventoPorId(int id)
        {
            var evento = eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null)
            {
                throw new KeyNotFoundException($"El evento con ID {id} no existe.");
            }

            Console.WriteLine("\n--- DETALLES DEL EVENTO ---");
            Console.WriteLine($"Tipo: {evento.GetType().Name}");
            Console.WriteLine($"ID: {evento.Id}");
            Console.WriteLine($"Nombre: {evento.Nombre}");
            Console.WriteLine($"Fecha: {evento.Fecha.ToShortDateString()}");
            Console.WriteLine($"Precio Base: ${evento.PrecioBase:N0}");
            Console.WriteLine($"Precio Final: ${evento.CalcularPrecioFinal():N0}");
            Console.WriteLine($"Capacidad Máxima: {evento.CapacidadMaxima}");
            Console.WriteLine($"Cupos Disponibles: {evento.CuposDisponibles}");
        }

        public void RealizarReserva(string nombrePersona, int eventoId, int cantidadEntradas)
        {
            // Validación 1: Cantidades negativas o iguales a cero[cite: 1]
            if (cantidadEntradas <= 0)
            {
                throw new ArgumentException("La cantidad de entradas debe ser mayor a cero.");
            }

            var evento = eventos.FirstOrDefault(e => e.Id == eventoId);
            if (evento == null)
            {
                throw new KeyNotFoundException($"El evento con ID {eventoId} no existe.");
            }

            // Validación 2: No permitir reservar más entradas que los cupos disponibles[cite: 1]
            if (cantidadEntradas > evento.CuposDisponibles)
            {
                throw new InvalidOperationException($"No hay suficientes cupos disponibles. Solo quedan {evento.CuposDisponibles} cupos.");
            }

            // Validación 3: Disminuir los cupos al confirmar una reserva[cite: 1]
            evento.CuposDisponibles -= cantidadEntradas;

            // Crear y registrar la reserva
            var nuevaReserva = new Reserva(contadorIdReserva++, nombrePersona, evento, cantidadEntradas);
            reservas.Add(nuevaReserva);

            Console.WriteLine($"\n¡Reserva exitosa! Se ha generado la reserva #{nuevaReserva.IdReserva} para {nombrePersona}.");
        }

        public void VerReservasRealizadas()
        {
            if (reservas.Count == 0)
            {
                Console.WriteLine("\nNo hay reservas registradas en el sistema.");
                return;
            }

            Console.WriteLine("\n================================");
            Console.WriteLine("      LISTA DE RESERVAS         ");
            Console.WriteLine("================================");
            foreach (var reserva in reservas)
            {
                Console.WriteLine(reserva);
            }
        }

        public void CancelarReserva(int idReserva)
        {
            var reserva = reservas.FirstOrDefault(r => r.IdReserva == idReserva);
            if (reserva == null)
            {
                throw new KeyNotFoundException($"La reserva con ID {idReserva} no existe.");
            }

            // Validación 5: Impedir que una misma reserva sea cancelada dos veces[cite: 1]
            if (reserva.EstaCancelada)
            {
                throw new InvalidOperationException("Esta reserva ya se encuentra cancelada.");
            }

            // Cambiar estado y devolver cupos[cite: 1]
            reserva.EstaCancelada = true;
            reserva.EventoReservado.CuposDisponibles += reserva.CantidadEntradas;

            Console.WriteLine($"\nReserva #{idReserva} cancelada exitosamente. Se han devuelto {reserva.CantidadEntradas} cupos al evento.");
        }

        public void VerResumenReservas()
        {
            var reservasActivas = reservas.Where(r => !r.EstaCancelada).ToList();
            int cantidadActivas = reservasActivas.Count;
            int totalEntradasReservadas = reservasActivas.Sum(r => r.CantidadEntradas);
            decimal valorTotalActivas = reservasActivas.Sum(r => r.ValorTotal);

            Console.WriteLine("\n================================");
            Console.WriteLine("     RESUMEN DE RESERVAS        ");
            Console.WriteLine("================================");
            Console.WriteLine($"Cantidad de reservas activas: {cantidadActivas}");
            Console.WriteLine($"Cantidad de entradas reservadas: {totalEntradasReservadas}");
            Console.WriteLine($"Valor total de reservas activas: ${valorTotalActivas:N0}");
        }
    }
}