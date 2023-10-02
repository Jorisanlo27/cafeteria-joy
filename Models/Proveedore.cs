using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Proveedore
{
    public int ProveedoresId { get; set; }

    public string NombreComercial { get; set; } = null!;

    public string Rnc { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime FechaRegistro { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
