using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Articulo
{
    public int ArticuloId { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string Descripcion { get; set; } = null!;

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int? Marca { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El costo no puede ser negativo.")]
    public decimal? Costo { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int? Proveedor { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "La existencia no puede ser negativo.")]
    public int? Existencia { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Facturacionarticulo> Facturacionarticulos { get; set; } = new List<Facturacionarticulo>();

    public virtual Marca? MarcaNavigation { get; set; }

    public virtual Proveedore? ProveedorNavigation { get; set; }
}
