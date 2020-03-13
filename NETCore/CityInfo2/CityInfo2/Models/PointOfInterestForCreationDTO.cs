using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Models
{
    public class PointOfInterestForCreationDTO
    {

        [Required(ErrorMessage = "name is mandatory")]
        [MaxLength(20)]
        public String Name { get; set; }

        [MaxLength(20)]
        public String Description { get; set; }
    }
}
