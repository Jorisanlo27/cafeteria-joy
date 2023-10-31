using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Facturacionarticulo
{
    public int FacturacionArticulosId { get; set; }

    public string NoFactura { get; set; } = null!;

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int Empleado { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int Articulo { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int Usuario { get; set; }

    [Range(typeof(DateTime), "10/1/2023", "12/31/9999", ErrorMessage = "La fecha no puede ser anterior al 1 de octubre de 2023.")]
    public DateOnly FechaVenta { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El monnto no puede ser negativo.")]
    public decimal MontoArticulo { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Las unidades vendidas no pueden ser negativas.")]
    public int UnidadesVendidas { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string? Comentario { get; set; }

    public bool Estado { get; set; }

    public virtual Articulo ArticuloNavigation { get; set; } = null!;

    public virtual Empleado EmpleadoNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
