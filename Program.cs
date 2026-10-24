using System;
using PLANAZO_POO_CSharp.Servicios;

namespace PLANAZO_POO_CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestorReservas gestor = new GestorReservas();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("    PLANAZO - SISTEMA DE GESTIÓN DE EVENTOS      ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Mostrar eventos disponibles");
                Console.WriteLine("2. Consultar evento por ID");
                Console.WriteLine("3. Realizar una reserva");
                Console.WriteLine("4. Ver reservas realizadas");
                Console.WriteLine("5. Cancelar una reserva");
                Console.WriteLine("6. Ver resumen de reservas");
                Console.WriteLine("7. Salir");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción (1-7): ");

                // Validación robusta: si escriben una letra, no se cae el programa
                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    MostrarMensajeError("Entrada inválida. Debe ingresar un número entero del 1 al 7.");
                    continue;
                }

                Console.WriteLine();

                try
                {
                    switch (opcion)
                    {
                        case 1:
                            gestor.MostrarEventosDisponibles();
                            PausarConsola();
                            break;

                        case 2:
                            ConsultarEventoUI(gestor);
                            break;

                        case 3:
                            RealizarReservaUI(gestor);
                            break;

                        case 4:
                            gestor.VerReservasRealizadas();
                            PausarConsola();
                            break;

                        case 5:
                            CancelarReservaUI(gestor);
                            break;

                        case 6:
                            gestor.VerResumenReservas();
                            PausarConsola();
                            break;

                        case 7:
                            continuar = false;
                            Console.WriteLine("¡Gracias por usar PLANAZO! Hasta luego.");
                            break;

                        default:
                            MostrarMensajeError("Opción no válida. Por favor, seleccione entre 1 y 7.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Captura global de excepciones para cumplir con el requisito de robustez
                    MostrarMensajeError($"Error en la operación: {ex.Message}");
                }
            }
        }

        private static void ConsultarEventoUI(GestorReservas gestor)
        {
            gestor.MostrarEventosDisponibles();
            Console.WriteLine("--------------------------------------------------");
            Console.Write("Ingrese el ID del evento que desea consultar: ");
            
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensajeError("ID inválido. Debe ingresar un número.");
                return;
            }

            gestor.ConsultarEventoPorId(id);
            PausarConsola();
        }

        private static void RealizarReservaUI(GestorReservas gestor)
        {
            gestor.MostrarEventosDisponibles();
            Console.WriteLine("--------------------------------------------------");
            
            Console.Write("Ingrese el nombre del cliente: ");
            string cliente = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(cliente))
            {
                MostrarMensajeError("El nombre del cliente no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el ID del evento que desea reservar: ");
            if (!int.TryParse(Console.ReadLine(), out int idEvento))
            {
                MostrarMensajeError("ID inválido. Debe ingresar un número.");
                return;
            }

            Console.Write("Ingrese la cantidad de entradas a reservar: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad))
            {
                MostrarMensajeError("Cantidad inválida. Debe ingresar un número entero.");
                return;
            }

            // Llamada respetando el orden exacto de tu GestorReservas: (string nombrePersona, int eventoId, int cantidadEntradas)[cite: 7]
            gestor.RealizarReserva(cliente, idEvento, cantidad);
            PausarConsola();
        }

        private static void CancelarReservaUI(GestorReservas gestor)
        {
            gestor.VerReservasRealizadas();
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Ingrese el ID de la reserva que desea cancelar: ");
            if (!int.TryParse(Console.ReadLine(), out int idReserva))
            {
                MostrarMensajeError("ID inválido. Debe ingresar un número.");
                return;
            }

            gestor.CancelarReserva(idReserva);
            PausarConsola();
        }

        private static void MostrarMensajeError(string mensaje)
        {
            Console.WriteLine($"\n⚠️ ERROR: {mensaje}");
            PausarConsola();
        }

        private static void PausarConsola()
            {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}