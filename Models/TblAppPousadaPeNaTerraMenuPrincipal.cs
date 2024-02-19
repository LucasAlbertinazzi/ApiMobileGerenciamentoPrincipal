using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

public partial class TblAppgerenciamentoPeNaTerraMenuPrincipal
{
    public int Id { get; set; }

    public string? TextoBtn { get; set; }

    public string? NomeMetodo { get; set; }

    public bool? BtnAtivo { get; set; }

    public string? DepPermitidos { get; set; }

    public DateTime? Data { get; set; }

    public string? CodIcone { get; set; }
}
