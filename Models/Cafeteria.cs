using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Cafeteria
{
    public int CafeteriaId { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string Descripcion { get; set; } = null!;

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int Campus { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int Encargado { get; set; }

    public bool Estado { get; set; }

    public virtual Campus? CampusNavigation { get; set; } = null!;

    public virtual Empleado? EncargadoNavigation { get; set; } = null!;
}
