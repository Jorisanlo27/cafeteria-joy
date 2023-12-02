using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Lineafactura
{
    public int LineaFacturaId { get; set; }

    public int FacturacionArticulosId { get; set; }

    public int ArticuloId { get; set; }

    public int UnidadesVendidas { get; set; }

    public decimal? Total { get; set; }

    public virtual Articulo Articulo { get; set; } = null!;

    public virtual Facturacionarticulo FacturacionArticulos { get; set; } = null!;
}
