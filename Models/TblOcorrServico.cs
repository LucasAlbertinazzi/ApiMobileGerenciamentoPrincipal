﻿using System;
using System.Collections.Generic;

namespace API_AppPousada.Models;

public partial class TblOcorrServico
{
    public int Codigo { get; set; }

    public string? Descricao { get; set; }

    public bool? Ativo { get; set; }

    public int? Codfuncao { get; set; }
}
