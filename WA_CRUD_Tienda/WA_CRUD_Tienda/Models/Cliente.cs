using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? NombreCliente { get; set; }

    public string? ApelllidosCliente { get; set; }

    public string? DireccionCliente { get; set; }

    public bool? ClienteOculto { get; set; }

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; set; } = new List<ClienteArticulo>();
}
