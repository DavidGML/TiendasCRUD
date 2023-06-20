using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string EmailUsuario { get; set; } = null!;

    public string PassUsuario { get; set; } = null!;

    public string RolUsuario { get; set; } = null!;
}
