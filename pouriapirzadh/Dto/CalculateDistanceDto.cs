using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class CalculateDistanceDto
    {
        public double FirstLat { get; set; }
        public double FirstLon { get; set; }
        public double SecondLat { get; set; }
        public double SecondLon { get; set; }

        public string Type { get; set; }
  
    }
}
