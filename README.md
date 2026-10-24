# PLANAZO - Sistema de Gestión de Eventos y Reservas 🎟️

Este proyecto es una aplicación de consola desarrollada en C# y .NET para la gestión de eventos de entretenimiento, consulta de disponibilidad, reservas de entradas y cancelaciones. Fue desarrollado aplicando los principios fundamentales de la Programación Orientada a Objetos (POO).

---

## 🛠️ Funcionalidades Implementadas
* **Consultar Eventos Disponibles:** Muestra el catálogo con fecha, cupos y precio final calculado.
* **Consultar Evento por ID:** Permite examinar los detalles específicos de un evento.
* **Realizar Reservas:** Registra reservas para un cliente validando cupos disponibles.
* **Cancelar Reservas:** Anula una reserva previa y devuelve automáticamente los cupos al evento.
* **Resumen de Reservas:** Muestra métricas financieras y total de reservas activas.
* **Reto Adicional:** Integración de la clase `EventoDeportivo` con sus propias reglas de cobro.
* **Manejo Robusto de Errores:** Control de excepciones y uso de `int.TryParse` para evitar cierres inesperados del programa ante entradas inválidas.

---

## 🚀 Instrucciones de Ejecución

1. Clonar el repositorio:
   ```bash
   git clone [https://github.com/DilanZapataReyes/PLANAZO-POO-CSharp.git](https://github.com/DilanZapataReyes/PLANAZO-POO-CSharp.git)
   cd PLANAZO-POO-CSharp

   ---

## ❓ Respuesta a la Pregunta de Sustentación

**¿Por qué utilizaste una clase abstracta `Evento` en lugar de crear clases completamente independientes para Cine, Concierto y Taller? ¿Qué ventajas tiene esta decisión cuando necesitamos agregar un nuevo tipo de evento?**

> **Respuesta:**
> Utilizar la clase abstracta `Evento` nos permite modelar la estructura base que todos los eventos comparten en un solo lugar (DRY - *Don't Repeat Yourself*). Además, habilita el **Polimorfismo**: la clase `Reserva` y el `GestorReservas` interactúan con una lista de tipo `List<Evento>` sin importar qué derivado específico sea (`Cine`, `Concierto`, `Taller` o `EventoDeportivo`).
>
> **Ventajas al agregar un nuevo tipo de evento:**
> La principal ventaja es el principio de Abierto/Cerrado (SOLID). Cuando agregamos un nuevo evento como `EventoDeportivo`, solo tuvimos que crear una nueva clase que heredara de `Evento` e implementara `CalcularPrecioFinal()`. No fue necesario modificar ninguna línea de código del método `RealizarReserva()`, `CalcularPrecioFinal()` o el resumen en `GestorReservas.cs`, reduciendo el riesgo de introducir bugs y facilitando la escalabilidad del proyecto.