using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Models
{
    public class PointOfInterestForUpdateDTO  //la regola è di creare una classe per ogni manipolazione che posso fare ad una risorsa 
                                              //(insert, update..),  notare che le regole di validazione dei dati sn le stesse quindi le copio da insert
    {
        [Required(ErrorMessage = "name is mandatory")]
        [MaxLength(20)]
        public String Name { get; set; }

        [MaxLength(20)]
        public String Description { get; set; }
    }
}
