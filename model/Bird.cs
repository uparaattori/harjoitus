using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace harjoitus.model
{
[Table("birds")]
public class Bird
{
    
    public Int32 BirdID {get ;set;}
    [Column("name")]
    public String Name {get;set;}

  public Int32 Count {get ;set;}

   public virtual ICollection<Sighting> Sightings { get; set; }

}
}