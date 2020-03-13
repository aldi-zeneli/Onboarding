using CityInfo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2
{
    public class CitiesDataStore        //classe da cui recuperiamo i dati (verrebbe sostituita con lettura da db piu avanti)
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();   //ci ritorna sempre i 3 dati di prova sotto, fimchè non riavviamo l app 

        public List<CityDTO> Cities { get; set; }

        public CitiesDataStore()
        {

            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id = 1,
                    Name = "prova1",
                    Description = "description1",
                    PointsOfInterest = new List<PointsOfInterestDTO>()
                    {
                        new PointsOfInterestDTO()
                        {
                            Id = 1,
                            Description = "descPOI1",
                            Name = "namePOI1"                            
                        },
                        new PointsOfInterestDTO()
                        {
                            Id = 2,
                            Description = "descPOI2",
                            Name = "namePOI2"
                        }
                    }
                },
                new CityDTO()
                {
                    Id = 2,
                    Name = "prova2",
                    Description = "description2"
                },
                new CityDTO()
                {
                    Id = 3,
                    Name = "prova3",
                    Description = "description3"
                }
            };

        }


    }
}
