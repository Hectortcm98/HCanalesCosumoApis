using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vuelo
    {
        public  static (bool, string, List<ML.Vuelo>, Exception) GetAll()
        {
            List<ML.Vuelo> vuelos = new List<ML.Vuelo>();
            try
            {
                using(var contex = new ConsumoApisContext())
                {
                    var query = (from obj in contex.Vuelos
                                 join arl in contex.Aerolineas on obj.IdAerolinea equals arl.IdAerolinea
                                 select new {obj, Aerolinea = arl.AerolineaNombre}).ToList();
                    if(query !=null )
                    {
                        foreach( var item in query)
                        {
                            ML.Vuelo vuelo = new ML.Vuelo();

                            vuelo.IdVuelo = item.obj.Idvuelo;
                            vuelo.NumeroVuelo = item.obj.NumeroVuelo;
                            vuelo.Origen = item.obj.Origen;
                            vuelo.Destino = item.obj.Destino;
                            vuelo.HoraSalida = item.obj.HoraSalida;
                            vuelo.HoraLLegada = item.obj.HoraLlegada;

                            vuelo.Aerolinea = new ML.Aerolinea();
                            vuelo.Aerolinea.IdAerolinea = Convert.ToInt32(item.obj.IdAerolinea);
                            vuelo.Aerolinea.AerolineaNombre = item.Aerolinea;

                            vuelos.Add( vuelo );
                        }
                        contex.SaveChanges();
                          
                    }
                    return (true, null, vuelos, null);
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, null, null);
            }
        }
        public static (bool, string, Exception) Add(ML.Vuelo vuelo)
        {
            try
            {
                using (var context = new ConsumoApisContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddVuelo {vuelo.NumeroVuelo}, '{vuelo.Destino}', '{vuelo.Origen}', '{vuelo.HoraSalida}', '{vuelo.HoraLLegada}', {vuelo.Aerolinea.IdAerolinea}");
                    if (query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception) Update(ML.Vuelo vuelo)
        {
            try
            {
                using (var context = new ConsumoApisContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UpdateVuelo {vuelo.IdVuelo},{vuelo.NumeroVuelo}, '{vuelo.Destino}', '{vuelo.Origen}', '{vuelo.HoraSalida}', '{vuelo.HoraLLegada}', {vuelo.Aerolinea.IdAerolinea}");
                    if (query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        //public static (bool, string, ML.Vuelo, Exception) Update(ML.Vuelo vuelo)
        //{
        //    //  bool resultado = false;
        //    try

        //    {
        //        using (DL.ConsumoApisContext context = new DL.ConsumoApisContext())
        //        {
        //            var rowAffected = context.Database.ExecuteSqlRaw($"UpdateVuelo {vuelo.IdVuelo},{vuelo.NumeroVuelo},{vuelo.Destino},'{vuelo.Origen}','{vuelo.HoraSalida}','{vuelo.HoraSalida}',{vuelo.Aerolinea.IdAerolinea}");
        //            if (rowAffected > 0)
        //            {
        //                // resultado = true;
        //                return (true, null, null, null);

        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        return (false, ex.Message, null, ex); //En esta me sale un error, no me permite editar un usuario
        //    }

        //    return (false, null, null, null);
        //}

        public static (bool, string?, ML.Vuelo, Exception?) GetById(int IdVuelos)
        {
            ML.Vuelo vuelo = new ML.Vuelo();
            try
            {
                using (var context = new ConsumoApisContext())
                {
                    var query = (from obj in context.Vuelos
                                 where obj.Idvuelo.Equals(IdVuelos)
                                 join arl in context.Aerolineas on obj.IdAerolinea equals arl.IdAerolinea
                                 select new { obj, aerolinea = arl.AerolineaNombre }).Single();

                    vuelo.IdVuelo = query.obj.Idvuelo;
                    vuelo.NumeroVuelo = query.obj.NumeroVuelo;
                    vuelo.Origen = query.obj.Origen;
                    vuelo.Destino = query.obj.Destino;
                    vuelo.HoraSalida = query.obj.HoraSalida;
                    vuelo.HoraLLegada = query.obj.HoraLlegada;
                    vuelo.Aerolinea = new ML.Aerolinea();
                    vuelo.Aerolinea.IdAerolinea = Convert.ToInt32(query.obj.IdAerolinea);
                    vuelo.Aerolinea.AerolineaNombre = query.aerolinea;

                    if (query != null)
                    {
                        return (true, null, vuelo, null);
                    }
                    else
                    {
                        return (false, null, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, null, ex);
            }
        }
        public static (bool, string, Exception) Delete(int IdVuelo)
        {
            try
            {
                using (var context = new ConsumoApisContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DeleteVuelo {IdVuelo}");
                    if (query != null)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
    }
}
