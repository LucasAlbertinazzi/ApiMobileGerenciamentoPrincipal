﻿using System;
using System.Collections.Generic;

namespace API_AppPousada.Models;

public partial class TblEntregaUf
{
    public int CodigoUf { get; set; }

    public string? Uf { get; set; }

    public string? UnidadeFederacao { get; set; }
}
