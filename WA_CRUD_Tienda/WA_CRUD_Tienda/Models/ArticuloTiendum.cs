using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class ArticuloTiendum
{
    public int CodigoArticuloAt { get; set; }

    public int CodigoSucursalAt { get; set; }

    public DateTime? FechaAs { get; set; }

    public virtual Articulo CodigoArticuloAtNavigation { get; set; } = null!;

    public virtual Sucursale CodigoSucursalAtNavigation { get; set; } = null!;
}
