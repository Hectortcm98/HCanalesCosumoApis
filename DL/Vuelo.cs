using System;
using System.Collections.Generic;

namespace DL;

public partial class Vuelo
{
    public int Idvuelo { get; set; }

    public int NumeroVuelo { get; set; }

    public string Destino { get; set; } = null!;

    public string Origen { get; set; } = null!;

    public string HoraSalida { get; set; } = null!;

    public string HoraLlegada { get; set; } = null!;

    public int? IdAerolinea { get; set; }

    public virtual Aerolinea? IdAerolineaNavigation { get; set; }
}
