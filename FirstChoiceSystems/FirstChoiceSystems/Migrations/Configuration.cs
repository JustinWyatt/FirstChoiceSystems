namespace FirstChoiceSystems.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FirstChoiceSystems.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstChoiceSystems.Models.ApplicationDbContext context)
        {
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
            //

            var userStore = new UserStore<BusinessUser>(context);
            var userManager = new UserManager<BusinessUser>(userStore);
            ApplicationDbContext db = new ApplicationDbContext();
            if (!(context.Users.Any( x=>x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@wholehogcafe.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 2
                {
                    PersonOfContact = "Marcus Johnson",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/2/2015"),
                    Balance = 500,
                    CompanyAddress = "2516 Cantrell Rd,",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72202,
                    CompanyName = "Whole Hog Cafe",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@wholehogcafe.com",
                    CompanyWebsite = "www.wholehogcafe.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@cajunswharf.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 3
                {
                    PersonOfContact = "Rick Smith",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "2400 Cantrell Rd",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Cajun's Wharf",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@cajunswharf.com",
                    CompanyWebsite = "www.cajunswharf.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Soda",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Beverages"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Oranges",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Produce"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "License",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Parking Lot Space",
                            ItemDescription = "",
                            PricePerUnit = 50.65,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Real Estate"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.Email == "test@chipotle.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/1/2015"),
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Spatula",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },
                        new Item()
                        {
                            ItemName = "Stationery",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 42
                        },new Item()
                        {
                            ItemName = "Signage",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 6
                        },new Item()
                        {
                            ItemName = "Knives",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15
                        },new Item()
                        {
                            ItemName = "Shirts",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 34
                        },new Item()
                        {
                            ItemName = "Hats",
                            ItemDescription = "",
                            PricePerUnit = 15.50,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 24
                        }
                    }
                };
                userManager.Create(userToInsert, "Password@123");
           } 

        }
    }
}
