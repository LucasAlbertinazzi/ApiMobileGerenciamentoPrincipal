﻿using System;
using System.Collections.Generic;

namespace API_AppGerenciamento.Models;

/// <summary>
/// dados das gerenciamentos
/// </summary>
public partial class Tblgerenciamento
{
    public int Idgerenciamento { get; set; }

    public string? Cnpj { get; set; }

    public string? Ie { get; set; }

    public string? NomeFantasia { get; set; }

    public string? NomeEmpresarial { get; set; }

    public string? Endereco { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public string? Cep { get; set; }

    public string? Uf { get; set; }
}
