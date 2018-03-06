
using System.Collections.Generic;
using System.Linq;
using System;

using harjoitus.model;

namespace harjoitus.Database
{

    public class SightingDAO : IDatabaseInterface
    {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SightingDAO()
        {
            // When starting db connection, check if there are any birds in db.
            // If not then add two;
            try {

            
            using (var ctx = new SightingContext())
                {
                    int count = ctx.birds.Count();
                    if (count==0)
                    {
                        addBird(Constants.FirstBird);
                        addBird(Constants.SecondtBird);
                    }
                }
            } catch (Exception ex) {
                log.Error("\r\n Could not open database.\r\n",ex);
                throw new ApplicationException("Could not open database",ex);
            }
        }

        /// <summary>
        /// Get all birds
        /// </summary>
        /// <returns>List of Birds</returns>
        
        public List<Bird> getBirds()
        {
            List<Bird> birds;
            
            try {

            
                using (var ctx = new SightingContext())
                {
                    birds = ctx.birds.ToList();
                }

            
            } catch (Exception ex) {
                log.Error("Error when getting bairds",ex);
                throw new ApplicationException("Error when getting birds",ex);
            }
            return birds;
        }

        public List<Sighting> getSightings()
        {
            List<Sighting> sightings = new List<Sighting>();
            try {
                using (var ctx = new SightingContext())
                {
                    sightings = ctx.sightings.ToList();
                }
            } catch (Exception ex ){
                log.Error("Error when getting sightingss",ex); 
                throw new ApplicationException("Error when getting sightings",ex);
            }
            return sightings;
        }


        public Bird getBird(string Name)
        {
            Bird tmp = null;
            try 
            {
            using (var ctx = new SightingContext())
            {               
                tmp = ctx.birds
                    .Where(b => b.Name == Name) 
                    .FirstOrDefault(); 

            }
             } catch (Exception ex ){
                log.Error("Error when adding bird : {Name}",ex); 
                throw new ApplicationException("Error when adding bird : {Name}",ex);
            }
            return tmp;
        }
       
        public bool addBird(string Name)
        {
            bool retval=false;
            try 
            {
            using (var ctx = new SightingContext())
            {               
                 var stud = new Bird() { Name = Name ,Count=0};        
                 ctx.birds.Add(stud);
                 ctx.SaveChanges();    
                retval=true;
            }
             } catch (Exception ex ){
                log.Error("Error when adding bird : {Name}",ex); 
                throw new ApplicationException("Error when adding bird : {Name}",ex);
            }
            return retval;
        }

        public bool addSighting(Int32 birdID)
        {
            bool retval=false;
            try
            {
            using (var ctx = new SightingContext())
            {
                 var stud = new Sighting() { BirdID= birdID, SightingDate=DateTime.Now };        
                 ctx.sightings.Add(stud);
                 ctx.SaveChanges(); 

                 retval=true;
            }
            } catch (Exception ex ){
                log.Error("Error when adding sighting : {birdID}",ex); 
                throw new ApplicationException("Error when adding sighting : {birdID}",ex);
            }
            
            return retval;
        }

        public Bird updateBirdCount(Int32 birdID)
        {
            Bird tmp;
            try
            {
            using (var ctx = new SightingContext())
            {
                tmp = ctx.birds.SingleOrDefault(b => b.BirdID == birdID);
                if (tmp!=null)
                {
                    tmp.Count=tmp.Count+1;
                    ctx.SaveChanges(); 
                }
                else {
                    log.Error("Could not find bird : " + birdID);
                }                                    
            }
            } catch (Exception ex ){
                log.Error("Error when adding sighting : {birdID}",ex); 
                throw new ApplicationException("Error when adding sighting : {birdID}",ex);
            }
            
            return tmp;
        }
    }
}

