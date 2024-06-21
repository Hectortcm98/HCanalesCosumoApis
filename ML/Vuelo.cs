using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vuelo
    {

        public int IdVuelo { get; set; }

        [Required(ErrorMessage = "campo requeridoo")]
        [Display(Name = "Numero de Vuelo")]
        public int NumeroVuelo { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }
        [Display(Name = "Hora de salida")]
        public string HoraSalida { get; set; }
        [Display(Name = "Hora de llegada")]
        public string HoraLLegada { get; set; }
        public ML.Aerolinea? Aerolinea { get; set; }
        public List<ML.Vuelo>? ListVuelos { get; set;}

    }
}
