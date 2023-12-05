using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Lineafactura
{
    public int LineaFacturaId { get; set; }

    [Display(Name = "No factura")]
    public int FacturacionArticulosId { get; set; }
    [Display(Name = "Artículo")]
    public int ArticuloId { get; set; }

    [Display(Name = "Unidades vendidas")]
    [Range(0, int.MaxValue, ErrorMessage = "Las unidades no puedes ser negativas.")]
    public int UnidadesVendidas { get; set; }

    public decimal? Total { get; set; }

    public virtual Articulo? Articulo { get; set; } = null!;

    [Display(Name = "No factura")]
    public virtual Facturacionarticulo? FacturacionArticulos { get; set; } = null!;
}
