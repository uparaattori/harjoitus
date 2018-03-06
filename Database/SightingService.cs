
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

using harjoitus.model;

namespace harjoitus.Database
{

    public class SightingService 
    {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    IDatabaseInterface dao = new SightingDAO();
        public SightingService()
        {
            // When starting db connection, check if there are any birds in db.
            // If not then add two;
            try {

            
                    List<Bird> tmp  = dao.getBirds();
                    if (tmp==null || tmp.Count==0)
                    {
                        dao.addBird(Constants.FirstBird);
                        dao.addBird(Constants.SecondtBird);
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
        
        public List<BirdData> getBirds()
        {
            List<Bird> birds;
            List<BirdData> targetList;
            try {

                birds = dao.getBirds();
            
                targetList = birds.Select(x => new BirdData() { BirdName = x.Name, BirdID= x.BirdID, count=x.Count })
                .ToList();
            
            } catch (Exception ex) {
                log.Error("Error when getting bairds",ex);
                throw new ApplicationException("Error when getting birds",ex);
            }
            return targetList;
        }

        ///<summary>
        /// Get all sightings.
        ///</summary>
        public List<SightingData> getSightings()
        {
            List<Sighting> sightings = new List<Sighting>();
            List<SightingData> retList = new List<SightingData>() ;

            try {
                sightings = dao.getSightings();


                // update birdnames to data
                List<Bird> birds = dao.getBirds();
                foreach (Bird bird in birds)
                {
                    SightingData tmp ;

                    foreach (Sighting xx in sightings.Where(s => s.BirdID == bird.BirdID).ToList())
                    {
                         tmp = new SightingData() { BirdName =bird.Name, tmpDate=xx.SightingDate, SightingID=xx.SightingID };
                        retList.Add(tmp) ;
                    }             
                }

           
                
            } catch (Exception ex ){
                log.Error("Error when getting sightingss",ex); 
                throw new ApplicationException("Error when getting sightings",ex);
            }
            return retList.OrderBy( tipu => tipu.DateFormatted).ToList();
        }

       ///<summary>
       /// Add new Bird.
       ///</summary>
        public bool addBird(string Name)
        {
            bool retval=false;
            try 
            {
                Bird tmp = dao.getBird(Name);

                if (tmp==null)
                {
                    dao.addBird(Name);
                    log.Info(" - Lajin lisäys : "+Name);
                }
                else{
                    log.Error("Bird is already in Database. Name : "+Name);
                }
             } catch (Exception ex ){
                log.Error("Error when adding bird : "+Name,ex); 
                throw new ApplicationException("Error when adding bird : "+Name,ex);
            }
            return retval;
        }

        ///<summary>
        ///Add new Sighting.
       ///</summary>
        public bool addSighting(Int32 birdID)
        {
            bool retval=false;
            try
            {
                retval = dao.addSighting(birdID);

                Bird tipu = dao.updateBirdCount(birdID);                 
            

                // write line to log
                List<Bird> tiput = dao.getBirds();                
                StringBuilder sb = new StringBuilder();
                sb.Append("- uusi havainto: "+tipu.Name);
                sb.Append(" - kaikki havainnot:");
                

                foreach (Bird xx in tiput)
                {
                    sb.Append(" "+xx.Name+" "+xx.Count);
                    if (xx.Count==1)
                    {
                        sb.Append(" kappale,");
                    }
                    else 
                    {
                        sb.Append(" kappaletta,");
                    }
                }
                // change last comma to dot
                string tmp = sb.ToString();
                tmp = tmp.TrimEnd(',') + ".";
                log.Info(tmp);      

            } catch (Exception ex ){
                log.Error("Error when adding sighting : {birdID}",ex); 
                throw new ApplicationException("Error when adding sighting : {birdID}",ex);
            }
            
            return retval;
        }
    }
}

