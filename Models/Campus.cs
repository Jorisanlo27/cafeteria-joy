using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Campus
{
    public int CampusId { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cafeteria> Cafeteria { get; set; } = new List<Cafeteria>();
}
