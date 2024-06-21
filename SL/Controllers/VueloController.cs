using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VueloController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Vuelo vuelos = new ML.Vuelo();
            var result = BL.Vuelo.GetAll();
            vuelos.ListVuelos = new List<ML.Vuelo>();
            if (result.Item1)
            {

                vuelos.ListVuelos = result.Item3;
                return Ok(result.Item3);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Vuelo? vuelos)
        {
            var result = BL.Vuelo.Add(vuelos);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int IdVuelo)
        {
            var result = BL.Vuelo.GetById(IdVuelo);
            if (result.Item1)
            {
                return Ok(result.Item3);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ML.Vuelo? vuelos)
        {
            var result = BL.Vuelo.Update(vuelos);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int IdVuelo)
        {
            var result = BL.Vuelo.Delete(IdVuelo);
            if (result.Item1)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
