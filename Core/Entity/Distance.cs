using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entity
{
   public class Distance
    {
        public Distance()
        {
            RdateTime = DateTime.Now;
        }
        public int Id { get; set; }
        public double FirstLat { get; set; }
        public double FirstLon { get; set; }
        public double SecondLat { get; set; }
        public double SecondLon { get; set; }
        public DateTime RdateTime { get; set; }
        public string TyepOfDistance { get; set; }
        public double FinalDistance { get; set; }
        [ForeignKey("UsersId")]
        public string UsersId { get; set; }
        
        public Users Users { get; set; }
    }
}
