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
    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public string Nombre { get; set; } = null!;

    [StringLength(11, MinimumLength = 11,ErrorMessage = "The field Cédula must be a string with a length of 11.")]
    [CedulaValidator(nameof(Usuario),nameof(UsuariosId))]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;


    [Display(Name = "Tipo usuario")]
    [RegularExpression(@"^[^@#$*]+$", ErrorMessage = "El campo no puede contener caracteres especiales como @, #, $, *")]
    public int TipoUsuario { get; set; }


    [Display(Name = "Límite de Crédito")]
    [Required(ErrorMessage = "El Límite de Crédito es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Límite de Crédito no puede ser negativo.")]
    public decimal? LimiteCredito { get; set; }


    [Display(Name = "Fecha de registro")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "10/1/2023", "12/31/9999", ErrorMessage = "La fecha no puede ser anterior al 1 de octubre de 2023.")]
    public DateTime FechaRegistro { get; set; }

    public bool Estado { get; set; }
    public virtual Tiposusuario? TipoUsuarioNavigation { get; set; } = null!;
}