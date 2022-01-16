using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITechArtPizzaDelivery.Infrastructure.DataSeeds
{
    public class RolesSeeds
    {
        public static readonly List<IdentityRole<int>> Roles = new()
        {
            new IdentityRole<int>
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            }
        };
    }
}