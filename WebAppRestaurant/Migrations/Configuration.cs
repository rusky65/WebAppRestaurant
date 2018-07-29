namespace WebAppRestaurant.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAppRestaurant.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebAppRestaurant.Models.ApplicationDbContext";
        }

        /// <summary>
        /// This process run always after the "update-database" migration command.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(WebAppRestaurant.Models.ApplicationDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );


            // Insert the categories
            var category1 = new Category { Name = "Levesek" };
            var category2 = new Category { Name = "Hideg elõételek" };
            var category3 = new Category { Name = "Meleg elõételek" };

            context.Categories.AddOrUpdate(x => x.Name, category1, category2, category3);
            //context.Categories.AddOrUpdate(x => x.Name, category2);
            //context.Categories.AddOrUpdate(x => x.Name, category3);

            // Insert the menus
            //            Id Name    Description Price   Category_Id
            //1   Tengeri hal trió Atlanti lazactatár, pácolt lazacfilé és tonhal lazackaviárral   7500    2
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Tengeri hal trió Atlanti lazactatár",
                Description = "pácolt lazacfilé és tonhal lazackaviárral",
                Price = 7500,
                Category = category2
            });

            //2   Füstölt pisztráng Gundel módra  Burgonyasaláta  4500    2
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Füstölt pisztráng Gundel módra",
                Description = "Burgonyasaláta",
                Price = 4500,
                Category = category2
            });

            //3   Borjúesszencia Zöldséges gyöngytyúk galuska    4500    1
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Borjúesszencia",
                Description = "Zöldséges gyöngytyúk galuska",
                Price = 4500,
                Category = category1
            });

            //4   Gundel saláta 1910  Spárga, paradicsom, uborka, sült paprika, zöldbab, gomba, jégsaláta 3500    2
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Gundel saláta 1910",
                Description = "Spárga, paradicsom, uborka, sült paprika, zöldbab, gomba, jégsaláta",
                Price = 3500,
                Category = category2
            });

            //5   Szarvasgomba cappuccino Finom, nagyon.  4500    1
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Szarvasgomba cappuccino",
                Description = "Finom, nagyon. :)",
                Price = 4500,
                Category = category1
            });

            //6   Hirtelen sült fogasderék illatos erdei gombákkal    meleg   4500    3
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Hirtelen sült fogasderék illatos erdei gombákkal",
                Description = "meleg",
                Price = 4500,
                Category = category3
            });

            //7   Szárított érlelt bélszín carpaccio  Öreg Trappista sajt, keserû levelek 5000    2
            context.MenuItems.AddOrUpdate(x => x.Name, new MenuItem() {
                Name = "Szárított érlelt bélszín carpaccio",
                Description = "Öreg Trappista sajt, keserû levelek",
                Price = 5000,
                Category = category2
            });

            //Insert locations
            var location1 = new Location() { Name = "Nem dohányzó terem", isNonSmoking = true };
            var location2 = new Location() { Name = "Dohányzó terem", isNonSmoking = false };
            var location3 = new Location() { Name = "Terasz", isNonSmoking = false };

            context.Locations.AddOrUpdate(x => x.Name, location1, location2, location3);

            //Insert Tables
            context.Tables.AddOrUpdate(x => x.Name,
                new Table { Name = "1. asztal", Location = location1 },
                new Table { Name = "2. asztal", Location = location1 },
                new Table { Name = "3. asztal", Location = location2 },
                new Table { Name = "4. asztal", Location = location2 },
                new Table { Name = "5. asztal", Location = location3 },
                new Table { Name = "6. asztal", Location = location3 }
                );

            //Creating cook, waiter, admin  permission groups
            AddRoleIfNotExists(context, "admin");
            AddRoleIfNotExists(context, "cook");
            AddRoleIfNotExists(context, "waiter");

            //Creating users
            AddUserIfNotExists(context, "szilard@szilardconto.hu", "szilard@szilardconto.hu", "admin,cook,waiter");
            AddUserIfNotExists(context, "pincer@p.hu", "pincer@p.hu", "waiter");
            AddUserIfNotExists(context, "szakacs@p.hu", "szakacs@p.hu", "cook");

        }

        /// <summary>
        /// Insert permission group, if not exists
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleName"></param>
        private void AddRoleIfNotExists(ApplicationDbContext context, string roleName) {
            //RoleStore is responsible for datas
            //RoleManager is the surface of developing.
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            var roleExists = manager.FindByName(roleName);
            if (roleExists == null) {
                //If not exists, create
                var role = new IdentityRole(roleName);
                var result = manager.Create(role);
                if (!result.Succeeded) {
                    throw new Exception(string.Join(",", result.Errors));
                }
            }
        }

        /// <summary>
        /// users insert, if not exists
        /// we use the services of Identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        private static void AddUserIfNotExists(ApplicationDbContext context, string userName, string email, string roles) {
            var user = new ApplicationUser { UserName = userName, Email = email };

            //UserStore is responsible for datas
            //UserManager is the surface of developing.
            // contex <- UserStore <- UserManager

            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);

            //check if the user ist already exists
            var userExists = manager.FindByEmail(user.Email);

            if (null == userExists) {
                var result = manager.Create(user, "Aa12345#");

                if (!result.Succeeded) {
                    throw new Exception(string.Join(",", result.Errors));
                }
            }

            //Setting the user to the role
            foreach (var role in roles.Split(',')) {
                manager.AddToRole(user.Id, role);
            }
        }
    }
}
