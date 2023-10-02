using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Facturacionarticulo
{
    public int FacturacionArticulosId { get; set; }

    public string NoFactura { get; set; } = null!;

    public int Empleado { get; set; }

    public int Articulo { get; set; }

    public int Usuario { get; set; }

    public DateOnly FechaVenta { get; set; }

    public decimal MontoArticulo { get; set; }

    public int UnidadesVendidas { get; set; }

    public string? Comentario { get; set; }

    public bool Estado { get; set; }

    public virtual Articulo ArticuloNavigation { get; set; } = null!;

    public virtual Empleado EmpleadoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
