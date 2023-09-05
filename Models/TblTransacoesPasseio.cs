using System;
using System.Collections.Generic;

namespace API_AppPousada.Models;

public partial class TblTransacoesPasseio
{
    public long IdPasseio { get; set; }

    public string Parceiro { get; set; } = null!;

    public string DescricaoTransacao { get; set; } = null!;
}
