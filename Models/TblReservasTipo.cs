﻿using System;
using System.Collections.Generic;

namespace API_AppPousada.Models;

public partial class TblReservasTipo
{
    public int IdReservaTipo { get; set; }

    public string Tipo { get; set; } = null!;
}