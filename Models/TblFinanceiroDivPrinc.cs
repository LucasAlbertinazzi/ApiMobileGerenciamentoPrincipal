﻿using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

public partial class TblFinanceiroDivPrinc
{
    public int IdDivPrinc { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<TblFinanceiroDivSecun> TblFinanceiroDivSecuns { get; set; } = new List<TblFinanceiroDivSecun>();
}
