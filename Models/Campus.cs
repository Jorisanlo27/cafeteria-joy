﻿using System;
using System.Collections.Generic;

namespace cafeteria_joy.Models;

public partial class Campus
{
    public int CampusId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cafeteria> Cafeteria { get; set; } = new List<Cafeteria>();
}
