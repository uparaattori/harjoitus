using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using harjoitus.Database;
using harjoitus.model;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace harjoitus.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        
        private SightingService _service;


        public SampleDataController()
        {
            log.Debug("SampleDataControllerissa  ollaan");
            _service = new SightingService();            
        }

        

        [HttpGet("[action]")]
        public IEnumerable<SightingData> Sightings()
        {
            log.Debug("Sightings action");
            List<SightingData> targetList = new List<SightingData>();

            try {                
                targetList = _service.getSightings();                
            } catch (ApplicationException ex)
            {
                log.Error("Getting Sightings failed : "+ex.Message);
            }
            
            return targetList;
        }


        [HttpPut("[action]/{id}")]
        public IActionResult Sightings(int id)
        {
        
           log.Debug("Sightings PUT "+id);            
           try 
           {
                _service.addSighting(Convert.ToInt32(id));
           } catch (ApplicationException ex) {
                log.Error("Getting Sightings failed:"+ex.Message);
                return new NotFoundResult();
           }
            
            return new NoContentResult();
        }


        [HttpGet("[action]")]
        public IEnumerable<BirdData> Birds()
        {
            log.Debug("Sightings action");
            List<BirdData> targetList = new List<BirdData>();
            try {
                targetList = _service.getBirds();
            }catch (ApplicationException ex) {
                log.Error("Getting Sightings failed:"+ex.Message);
           }
                       
            // foreach ( BirdData x in targetList)
            // {
            //     log.Info("Lintu : " + x.BirdName);
            // }

            return targetList;
        }

        
        [HttpPost("[action]")]
        //public IActionResult Birds([FromBody] Birdy testi)
        public IActionResult Birds( [FromBody] JObject json)
        {       
           string BirdName="";
            
        // couldn't get the data out of the json automatically, so let's do it 
        // the hard way
           JToken value;
           bool found  =json.TryGetValue("Birdy", out value);            
            if (found)
            {
                log.Debug("\r\nBirds -- "+ value.ToString());
                BirdName = value.FirstOrDefault().ToObject<String>();
            }

  //          log.Info("\r\n ------ Birds palikka ----- "+ json);            
  //          log.Info("\r\n******** Birds action ************* : "+ BirdName);

           try {
                if (!_service.addBird(BirdName))
                {
                    ContentResult tmp =new ContentResult();
                    tmp.StatusCode=300;                
                    return tmp;
                }
           } catch (ApplicationException ex) {
                log.Error("Updating bird failed:"+ex.Message);
           }
 
            return  new NoContentResult();
        }
    }

    public class Birdy {
        public String nameofbird {get;set;}
    }
}
