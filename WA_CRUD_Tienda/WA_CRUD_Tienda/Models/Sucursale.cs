using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class Sucursale
{
    public int CodigoSucursal { get; set; }

    public string? DireccionSucursal { get; set; }

    public bool? SucursalOculta { get; set; }

    public virtual ICollection<ArticuloTiendum> ArticuloTienda { get; set; } = new List<ArticuloTiendum>();
}
