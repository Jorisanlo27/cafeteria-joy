using cafeteria_joy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Proveedore
{
    public int ProveedoresId { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string NombreComercial { get; set; } = null!;

    [StringLength(9, MinimumLength = 9, ErrorMessage = "The field RNC must be a string with a length of 9.")]
    [RncValidator(nameof(Proveedore), nameof(ProveedoresId))]
    public string Rnc { get; set; } = null!;

    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "10/1/2023", "12/31/9999", ErrorMessage = "La fecha no puede ser anterior al 1 de octubre de 2023.")]
    public DateTime FechaRegistro { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
