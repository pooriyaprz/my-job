using Core.Entity;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class DistanceRepository : Repository<Distance>, IDistanceRepository

    {
        public DistanceRepository(MyContext context) : base(context)
        {

        }
        public MyContext UserContext => Context as MyContext;

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2, string unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == "k")
                {
                    dist = dist * 1.609344;
                }
                else if (unit == "N")
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        public async Task<ICollection<Distance>> GetAllDistanceReq(string userId)
        {
            return await UserContext.Distances.Where(x => x.UsersId == userId).ToListAsync();
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
