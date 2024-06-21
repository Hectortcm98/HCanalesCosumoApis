using System;
using System.Collections.Generic;

namespace DL;

public partial class Aerolinea
{
    public int IdAerolinea { get; set; }

    public string AerolineaNombre { get; set; } = null!;

    public virtual ICollection<Vuelo> Vuelos { get; set; } = new List<Vuelo>();
}
