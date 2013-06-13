using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DCL.Models
{
    public class DCLDb : DbContext
    {
        public DCLDb() : base("name=DefaultConnection")
        {

        }

      // public DbSet<UserProfile> UserProfiles { get; set; }
       public DbSet<Restaurant> Restaurants { get; set; }
       public DbSet<RestaurantReview> Reviews { get; set; }
    }
}