using lineshift_v3_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace lineshift_v3_backend.DataAccess.Queries
{   
    public static class SportsQueries
    {
        public static IQueryable<Sport> GetSportsAsync(this IQueryable<Sport> sports)
        {
            return sports;
        }
    }
}
