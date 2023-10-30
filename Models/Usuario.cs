using cafeteria_joy.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace cafeteria_joy.Models;

public partial class Usuario
{
    public int UsuariosId { get; set; }

    [MinLength(3)]
    public string Nombre { get; set; } = null!;

    [StringLength(11, MinimumLength = 11,ErrorMessage = "The field Cédula must be a string with a length of 11.")]
    [CedulaValidator(nameof(Usuario))]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;


    [Display(Name = "Tipo usuario")]
    public int TipoUsuario { get; set; }


    [Display(Name = "Límite de Crédito")]
    [Required]
    public decimal? LimiteCredito { get; set; }


    [Display(Name = "Fecha de registro")]
    [DataType(DataType.Date)]
    public DateTime FechaRegistro { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Facturacionarticulo> Facturacionarticulos { get; set; } = new List<Facturacionarticulo>();

    public virtual Tiposusuario? TipoUsuarioNavigation { get; set; } = null!;
}