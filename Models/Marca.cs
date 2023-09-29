using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Marca
{
    public int MarcaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
