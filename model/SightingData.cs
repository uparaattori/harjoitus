using System;
namespace harjoitus.model
{
public class SightingData
        {
            public String DateFormatted ;            
            public int SightingID{ get; set; }
            public string BirdName { get; set; }

        

            public DateTime tmpDate
            {
                set
                {
                    if (value!=null)
                    {
                        DateFormatted = value.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                      
                }
            }
        }
}