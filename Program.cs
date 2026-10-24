using System;
using PLANAZO_POO_CSharp.Servicios;

class Program
{
    static void Main(string[] args)
    {
        GestorReservas gestor = new GestorReservas();
        int opcion = -1;

        while (opcion != 0)
        {
            MostrarMenu();
            string entrada = Console.ReadLine();

            // Validar que la opción ingresada sea un número entero
            if (!int.TryParse(entrada, out opcion))
            {
                Console.WriteLine("\n[Error] Por favor, ingrese un número válido.");
                Continuar();
                continue;
            }

            try
            {
                switch (opcion)
                {
                    case 1:
                        gestor.MostrarEventosDisponibles();
                        break;

                    case 2:
                        Console.Write("\nIngrese el ID del evento a consultar: ");
                        if (int.TryParse(Console.ReadLine(), out int idConsulta))
                        {
                            gestor.ConsultarEventoPorId(idConsulta);
                        }
                        else
                        {
                            Console.WriteLine("[Error] ID inválido.");
                        }
                        break;

                    case 3:
                        gestor.MostrarEventosDisponibles();
                        Console.Write("\nIngrese el Nombre de la persona que reserva: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Ingrese el ID del evento: ");
                        if (int.TryParse(Console.ReadLine(), out int eventoId))
                        {
                            Console.Write("Ingrese la cantidad de entradas: ");
                            if (int.TryParse(Console.ReadLine(), out int cantidad))
                            {
                                gestor.RealizarReserva(nombre, eventoId, cantidad);
                            }
                            else
                            {
                                Console.WriteLine("[Error] Cantidad de entradas inválida.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("[Error] ID de evento inválido.");
                        }
                        break;

                    case 4:
                        gestor.VerReservasRealizadas();
                        break;

                    case 5:
                        gestor.VerReservasRealizadas();
                        Console.Write("\nIngrese el ID de la reserva que desea cancelar: ");
                        if (int.TryParse(Console.ReadLine(), out int idReserva))
                        {
                            gestor.CancelarReserva(idReserva);
                        }
                        else
                        {
                            Console.WriteLine("[Error] ID de reserva inválido.");
                        }
                        break;

                    case 6:
                        gestor.VerResumenReservas();
                        break;

                    case 0:
                        Console.WriteLine("\n¡Gracias por usar PLANAZO! Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("\n[Error] Opción no válida. Seleccione una opción entre 0 y 6.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Capturamos cualquier excepción personalizada (ArgumentException, KeyNotFoundException, InvalidOperationException)
                // y evitamos que el programa se cierre abruptamente
                Console.WriteLine($"\n[AVISO DE ERROR]: {ex.Message}");
            }

            if (opcion != 0)
            {
                Continuar();
            }
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("        PLANAZO - EVENTOS       ");
        Console.WriteLine("================================");
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("0. Salir");
        Console.Write("\nSeleccione una opción: ");
    }

    static void Continuar()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}