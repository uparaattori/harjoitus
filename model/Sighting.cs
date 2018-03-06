
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace harjoitus.model
{
[Table("sightings")]
public class Sighting
{
    [Key]
    public  Int32 SightingID {get ;set;}
    public  DateTime SightingDate {get;set;}

    public  Int32 BirdID {get ;set;}
    [ForeignKey("BirdID")]
    public virtual Bird Bird{get ;set;}

} 
}