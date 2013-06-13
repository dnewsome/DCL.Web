namespace DCL.Migrations
{
    using DCL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<DCL.Models.DCLDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            
        }

        //  update-database -force
        protected override void Seed(DCL.Models.DCLDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
               new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
               new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
               new Restaurant
               {
                   Name = "Smaka",
                   City = "Gothenburg",
                   Country = "Sweden",
                   Reviews =
                       new List<RestaurantReview> { 
                       new RestaurantReview { Rating = 9, Body="Great food!!", ReviewerName="Scott" }
                   }
               });

            // for (int i = 0; i < 1000; i++)                                                       
            // {                                                                                    
            //    context.Restaurants.AddOrUpdate(r => r.Name,                                      
            //        new Restaurant { Name = i.ToString(), City = "Nowhere", Country = "USA" });   
            // }                                                                                    

            SeedMembership();
        }

        private void SeedMembership()
        {
            //                          
            // Create a user in a role  
            //                          

            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            // Create Roles 
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }

            // Create user 1    
            if (membership.GetUser("danny newsome", false) == null)
            {
                membership.CreateUserAndAccount("danny newsome", "d7777777");
            }
            if (!roles.GetRolesForUser("danny newsome").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "danny newsome" }, new[] { "admin" });
            }
            // Create user 2    
            if (membership.GetUser("dan newsome", false) == null)
            {
                membership.CreateUserAndAccount("dan newsome", "d7777777");
            }
            if (!roles.GetRolesForUser("dan newsome").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "dan newsome" }, new[] { "admin" });
            }
            // Create user 3    
            //if (membership.GetUser("d", false) == null)
            //{
            //    membership.CreateUserAndAccount("d", "d7777777");
            //}
            //if (!roles.GetRolesForUser("d").Contains("Admin"))
            //{
            //    roles.AddUsersToRoles(new[] { "d" }, new[] { "User" });
            //}


            //// Memberships  
            //if (Membership.GetUser("dan newsome") == null)
            //{
            //    Membership.CreateUser("dan newsome", "d7777777");
            //    Roles.AddUserToRole("dan newsome", "Admin");
            //}
            //if (Membership.GetUser("bailey newsome") == null)
            //{
            //    Membership.CreateUser("bailey newsome", "d7777777");
            //    Roles.AddUserToRole("bailey newsome", "Admin");
            //}
            //if (Membership.GetUser("dusty newsome") == null)
            //{
            //    Membership.CreateUser("dusty newsome", "d7777777");
            //    Roles.AddUserToRole("dusty newsome", "User");
            //}















        }
    }
}
