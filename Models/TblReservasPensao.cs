using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

public partial class TblReservasPensao
{
    public int IdPensao { get; set; }

    public string Tipo { get; set; } = null!;

    public int RefeicoesDiarias { get; set; }

    public string Descricao { get; set; } = null!;
}
