using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class Articulo
{
    public int CodigoArticulo { get; set; }

    public string? DescripcionArticulo { get; set; }

    public decimal PrecioArticulo { get; set; }

    public string? ImagenArticulo { get; set; }

    public int? StockArticulo { get; set; }

    public bool? ArticuloOculto { get; set; }

    public virtual ICollection<ArticuloTiendum> ArticuloTienda { get; set; } = new List<ArticuloTiendum>();

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; set; } = new List<ClienteArticulo>();
}
