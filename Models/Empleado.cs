using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Empleado
{
    public int EmpleadosId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string TandaLabor { get; set; } = null!;

    public decimal? PorcientoComision { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public bool Estado { get; set; }
}
