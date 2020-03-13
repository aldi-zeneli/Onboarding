using CityInfo2.Models;
using CityInfo2.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {

        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }


        [HttpGet("{id}/pointsofinterest")]  //se non avessi messo il Route era [HttpGet("api/cities/{id}")]
        public IActionResult GetPointsOfInterest(int id)  //il parametro id deve essere del tipo che mi viene ritornato dalla get ({id})
        {
            try
            {
                //throw new Exception("prova exception");

                var res = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

                if (res == null)
                {
                    _logger.LogInformation($"città {id} inesistente"); //ci sn vari metodi di _logger in base al livello di criticità che voglio loggare
                    return NotFound();
                }

                return Ok(res.PointsOfInterest);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"errore città {id}");
                return StatusCode(500);     //a causa dell exception, devo ritornare manualmente lo status code
            }            
        }

        [HttpGet("{cityId}/pointsofinterest/{poiId}", Name = "percorso")]
        public IActionResult GetPointOfInterest(int cityId, int poiId)
        {
            var res = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (res == null)
                return NotFound();

            var poi = res.PointsOfInterest.FirstOrDefault(x => x.Id == poiId);

            if (poi == null)
                return NotFound();

            return Ok(poi);
        }

        [HttpPost("{cityId}/pointsofinterest")]     //per creare risorsa uso POST
        //con [fromBody] indico che quel param viene valorizzato con valori contenuti nel body della http request
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDTO pointOfInterest)
        {
            //se pointOfView è null, non è stato formato correttamente del consumer di quest api => ritorno status code 400 bad request
            if (pointOfInterest == null)
                return BadRequest();

            //validazione "custom"
            if (pointOfInterest.Name == "nomeNonAccettato")
                ModelState.AddModelError("invalidName", "name is not valid");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
                return NotFound();

            var newPoi = new PointsOfInterestDTO
            {
                Id = 4,//a caso
                Description = pointOfInterest.Description,
                Name = pointOfInterest.Name
            };

            //aggiungo nuovo elemento alla lista
            city.PointsOfInterest.Add(newPoi);

            //ritorno status code che indica creazione nuova risorsa, richiede uri della nuova risorsa per indicare
            //dove è possibile trovarlo nel body della hhtp response
            return CreatedAtRoute("percorso",
                new
                {   //oggetto con i 2 campi tra {} dell'uri con come percorso
                    cityId = city.Id,
                    poiId = newPoi.Id
                }, newPoi);
        }


        [HttpPut("{cityId}/pointsofinterest/{id}")]     //PUT per full update. 
        //oss:full update richede update tutti i campi, se val nuovo per campo non fornito => mette in automatico il val di def per quel tipo dato
        public IActionResult UpdatePointOfInterest(int cityId, int id
            , [FromBody] PointOfInterestForUpdateDTO pointOfInterest)
        {
            if (RequestIsWellFormed(pointOfInterest))
            {
                if (PoiExists(cityId, id))
                {
                    var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
                    var poi = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

                    //update dei campi. essendo full update, i campi nn forniti sn messi a val di def per quel tipo
                    poi.Name = pointOfInterest.Name;
                    poi.Description = pointOfInterest.Description;

                    return NoContent(); //noContent indica update successful e nn ritorna il dato aggiornato
                }
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id
            , [FromBody] JsonPatchDocument<PointOfInterestForUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            if (PoiExists(cityId, id))
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
                var poi = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);


                var poiToPatch = new PointOfInterestForUpdateDTO()
                    {
                        Name = poi.Name,
                        Description = poi.Description
                    };

                //applyTo applica in automatico le modifiche se ho rispettato il formato di JSON Patch, se no
                //salva l'errore nel modelState
                patchDoc.ApplyTo(poiToPatch, ModelState);


                //l'ApplyTo controlla se il JSON Patch è corretto e propaga errore al modelState, ma dobbiamo
                //anche controllare che le regole di validazione di un poi siano rispettate
                TryValidateModel(poiToPatch);
                    

                if (ModelState.IsValid == false)
                    return BadRequest(ModelState);

                //applico le modifiche sul vero poi (sopra era un poiUpdateDTO)
                poi.Name = poiToPatch.Name;
                poi.Description = poiToPatch.Description;

                return NoContent();
            }
            else
                return NotFound();
        }


        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (PoiExists(cityId, id))
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
                var poi = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

                city.PointsOfInterest.Remove(poi);

                _mailService.Send("delete", $"{poi.Name} with id {poi.Id} was deleted");

                return NoContent();
            }
            else
                return NotFound();
        }

        public bool RequestIsWellFormed(PointOfInterestForUpdateDTO pointOfInterest)
        {
            return pointOfInterest == null ? false : true;
        }

        public bool PoiExists(int cityId, int idPoi)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
                return false;

            var poi = city.PointsOfInterest.FirstOrDefault(x => x.Id == idPoi);
            return city == null ? false : true;
        }
    }
}
