using System;
using System.Collections.Generic;

namespace WA_CRUD_Tienda.Models;

public partial class ClienteArticulo
{
    public int IdClienteCa { get; set; }

    public int CodigoArticuloCa { get; set; }

    public DateTime? FechaAc { get; set; }

    public virtual Articulo CodigoArticuloCaNavigation { get; set; } = null!;

    public virtual Cliente IdClienteCaNavigation { get; set; } = null!;
}
