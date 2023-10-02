using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Usuario
{
    public int UsuariosId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public int TipoUsuario { get; set; }

    public decimal? LimiteCredito { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Facturacionarticulo> Facturacionarticulos { get; set; } = new List<Facturacionarticulo>();

    public virtual Tiposusuario TipoUsuarioNavigation { get; set; } = null!;
}
