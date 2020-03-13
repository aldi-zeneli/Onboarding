using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Controllers
{
    [Route("api/cities")]  //funziona come prefisso in modo da nn ripetere il percorso per ogni metodo sotto (vedi teoria)
    public class CitiesController : Controller
    {
        [HttpGet()]  //se nn avessi messo il Route con il prefisso sopra, dovrei mettere qui [HttpGet("api/cities")]
        public JsonResult GetCities()
        {
            //prima versione, ritorno direttamente dei dati, verrà poi sostituita con classi apposite
            //return new JsonResult(new List<object>()
            //{
            //    new { id = 1, Name = "milano"},
            //    new { id = 2, Name = "torino"}
            //});

            return new JsonResult(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]  //se non avessi messo il Route era [HttpGet("api/cities/{id}")]
        public IActionResult GetCity(int id)  //il parametro id deve essere del tipo che mi viene ritornato dalla get ({id}), E DEVE AVERE LO STESSO NOME!
        {
            var res =  CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (res == null)
                return NotFound();

            return Ok(res);
        }




    }
}
