using Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IDistanceRepository : IRepository<Distance>
    {
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2, string unit);
        Task<ICollection< Distance>> GetAllDistanceReq(string userId);
    }
}
