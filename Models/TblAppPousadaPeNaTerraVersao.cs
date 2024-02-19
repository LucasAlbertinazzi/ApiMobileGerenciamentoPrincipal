using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

public partial class TblAppGerenciamentoVersao
{
    public int Id { get; set; }

    public DateTime? Data { get; set; }

    public string? Versao { get; set; }
}
