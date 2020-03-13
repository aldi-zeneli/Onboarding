using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.Models
{
    public class CityDTO
    {

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get { return PointsOfInterest.Count; }
        }

        public ICollection<PointsOfInterestDTO> PointsOfInterest { get; set; }
         = new List<PointsOfInterestDTO>();

    }
}
