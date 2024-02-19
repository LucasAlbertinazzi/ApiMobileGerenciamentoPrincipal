using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

public partial class TblReservasHospede
{
    public string IdReserva { get; set; } = null!;

    public long IdHospede { get; set; }
}
