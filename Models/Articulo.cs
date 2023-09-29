using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Articulo
{
    public int ArticuloId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? Marca { get; set; }

    public decimal? Costo { get; set; }

    public int? Proveedor { get; set; }

    public int? Existencia { get; set; }

    public bool Estado { get; set; }

    public virtual Marca? MarcaNavigation { get; set; }

    public virtual Proveedore? ProveedorNavigation { get; set; }
}
