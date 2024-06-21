using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aerolinea
    {
        public static (bool, string, List<ML.Aerolinea>, Exception) GetAll()
        {
            List<ML.Aerolinea>  aerolineas =  new List<ML.Aerolinea> ();

            try
            {
                using (var contex = new ConsumoApisContext())
                {
                    var query = contex.Aerolineas.FromSql($"EXECUTE dbo.GetAllAerolinea").ToList();

                    if(query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Aerolinea aerolinea =  new ML.Aerolinea();

                            aerolinea.IdAerolinea = item.IdAerolinea;
                            aerolinea.AerolineaNombre = item.AerolineaNombre;

                            aerolineas.Add(aerolinea);
                        }
                        contex.SaveChanges();
                    }
                    return (true, null,aerolineas,null);
                }
            }
            catch (Exception ex)
            {

                return(false, ex.Message, null,null);
            }
        }
    }
}
