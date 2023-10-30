using cafeteria_joy.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cafeteria_joy.Models;

public partial class Empleado
{
    public int EmpleadosId { get; set; }

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string Nombre { get; set; } = null!;

    [StringLength(11, MinimumLength = 11, ErrorMessage = "The field Cédula must be a string with a length of 11.")]
    [CedulaValidator(nameof(Usuario))]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;

    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string TandaLabor { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "El porcentaje de comisión no puede ser negativo.")]
    public decimal? PorcientoComision { get; set; }

    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "10/1/2023", "12/31/9999", ErrorMessage = "La fecha no puede ser anterior al 1 de octubre de 2023.")]
    public DateTime? FechaIngreso { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Cafeteria> Cafeteria { get; set; } = new List<Cafeteria>();

    public virtual ICollection<Facturacionarticulo> Facturacionarticulos { get; set; } = new List<Facturacionarticulo>();
}
