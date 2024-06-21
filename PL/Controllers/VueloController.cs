using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VueloController : Controller
    {
        public IActionResult GetAll()
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7210/api/Vuelo/");
                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ML.Vuelo>>();
                    readTask.Wait();

                    var jsonResult = readTask.Result;

                    if (jsonResult != null)
                    {
                        ML.Vuelo vuelos = new ML.Vuelo();
                        vuelos.Aerolinea = new ML.Aerolinea();
                        vuelos.ListVuelos = new List<ML.Vuelo>();
                        var result1 = BL.Aerolinea.GetAll();
                        vuelos.Aerolinea.Listaerolineas = result1.Item3;
                        vuelos.ListVuelos = jsonResult;

                        return View(vuelos);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ML.Vuelo vuelos = new ML.Vuelo();
                    vuelos.Aerolinea = new ML.Aerolinea();
                    vuelos.ListVuelos = new List<ML.Vuelo>();
                    return View(vuelos);
                }
            }
        }

        [HttpGet]
        public IActionResult Form(int IdVuelo)
        {
            ML.Vuelo vuelos = new ML.Vuelo();
            vuelos.Aerolinea = new ML.Aerolinea();
            vuelos.Aerolinea.Listaerolineas = new List<ML.Aerolinea>();

            var result1 = BL.Aerolinea.GetAll();
            vuelos.Aerolinea.Listaerolineas = result1.Item3;

            if (IdVuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7210/api/Vuelo/");

                    var responseTask = client.GetAsync("GetById?IdVuelo=" + IdVuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Vuelo>();
                        readTask.Wait();

                        var jsonResult = readTask.Result;

                        if (jsonResult != null)
                        {
                            vuelos = readTask.Result;
                            var result2 = BL.Aerolinea.GetAll();
                            vuelos.Aerolinea.Listaerolineas = result2.Item3;
                            return View(vuelos);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

            }
            else
            {
                return View(vuelos);

            }
        }

        [HttpPost]
        public IActionResult Form(ML.Vuelo vuelo)
        {

            if (vuelo.IdVuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7210/api/Vuelo/");

                    var responseTask = client.PutAsJsonAsync<ML.Vuelo>("Update", vuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se actualizó correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se actualizó";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7210/api/Vuelo/");

                    var responseTask = client.PostAsJsonAsync<ML.Vuelo>("Add", vuelo);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se registró correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se agregó";
                        return PartialView("Modal");
                    }
                }
            }
        }




        public IActionResult Delete(int? IdVuelo)
        {
            if (IdVuelo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7210/api/Vuelo/");

                    var responseTask = client.DeleteAsync("Delete?IdVuelo=" + IdVuelo);

                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "El vuelo se elimino correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "El vuelo no se elimino";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                ViewBag.Text = "Hubo un error";
                return PartialView("Modal");
            }
        }
    }
}
