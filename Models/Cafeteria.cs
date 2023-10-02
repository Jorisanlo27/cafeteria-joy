using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Cafeteria
{
    public int CafeteriaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Campus { get; set; }

    public int Encargado { get; set; }

    public bool Estado { get; set; }

    public virtual Campus CampusNavigation { get; set; } = null!;

    public virtual Empleado EncargadoNavigation { get; set; } = null!;
}
