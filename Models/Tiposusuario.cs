using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Tiposusuario
{
    public int TipoUsuarioId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
