using harjoitus.model;
using System;
using System.Collections.Generic;

namespace harjoitus.Database
{
public interface IDatabaseInterface
{   
    ///<summary>
    /// Get all birds and sighting counts
    ///</summary>    
    ///<return>List<BirdData></return>
    List<Bird> getBirds();    

    ///<summary>
    /// Get all sightings counts
    ///</summary>    
    ///<return>List<SightingData></return>
    List<Sighting> getSightings();        
       
    ///<summary>
    /// Add new Bird
    ///</summary>    
    ///<return>bool</return>
    bool addBird(string Name);
        
    ///<summary>
    /// Add new Sighting
    ///</summary>    
    ///<return>bool</return>
    bool addSighting(Int32 birdID);

/// <summary>
/// Update count up for a bird.
/// </summary>
    Bird updateBirdCount(Int32 birdID);

 /// <summary>
/// get bird by name
/// </summary>
   Bird getBird(string Name);

    



    }
}
