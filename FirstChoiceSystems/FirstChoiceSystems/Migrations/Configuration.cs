using FirstChoiceSystems.Models.DBModels;

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
        private object userManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FirstChoiceSystems.Models.ApplicationDbContext context)
        {
            var db = new ApplicationDbContext();

            context.BusinessCategories.AddOrUpdate(b => b.CategoryName, new BusinessCategory { CategoryName = "Restaraunt" });
            context.ItemCategories.AddOrUpdate(i => i.CategoryName,
                new ItemCategory { CategoryName = "Silverware" },
                new ItemCategory { CategoryName = "Stationery" },
                new ItemCategory { CategoryName = "Advertisement Media" },
                new ItemCategory { CategoryName = "Beverages" },
                new ItemCategory { CategoryName = "Produce" },
                new ItemCategory { CategoryName = "Real Estate" },
                new ItemCategory { CategoryName = "Dry Goods" }
                );

            var userStore = new UserStore<BusinessUser>(context);
            var manager = new ApplicationUserManager(userStore);

            if (!(context.Users.Any(x => x.UserName == "test@chipotle.com")))
            {

                var user = new BusinessUser //Todo Business User 1
                {
                    PersonOfContact = "Jimmy Calhoun",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Now,
                    Balance = 500,
                    CompanyAddress = "100 S University Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Chipotle Mexican Grill",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@chipotle.com",
                    CompanyWebsite = "www.chipotle.com",
                    UserName = "test@chipotle.com",
                    ItemsUpForSale = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Chipotle Tortilla",
                            ItemDescription = "",
                            PricePerUnit = 41.25,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 15,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleTortilla.jpg"
                                }
                            }
                        },
                        new Item()
                        {
                            ItemName = "Unique Chipolte Foil Wrap",
                            ItemDescription = "",
                            PricePerUnit = 89.98,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                            UnitsAvailable = 100,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleFoilWrap.jpg"
                                },

                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleFoilWrap2.jpg"
                                }
                            }
                        },
                        new Item()
                        {
                            ItemName = "Chipotle Signage",
                            ItemDescription = "",
                            PricePerUnit = 263.25,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 0,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleSignage.jpg"
                                }
                            }
                        },
                        new Item()
                        {
                            ItemName = "Chipotle Knives",
                            ItemDescription = "",
                            PricePerUnit = 89.55,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                            UnitsAvailable = 185,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleKnives.jpg"
                                }
                            }
                        },
                        new Item()
                        {
                            ItemName = "Chipotle Shirts",
                            ItemDescription = "",
                            PricePerUnit = 10.25,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 78,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath =  @"~\Content\Images\ChipotleShirt.jpg"
                                }
                            }
                        },
                        new Item()
                        {
                            ItemName = "Chipotle Hats",
                            ItemDescription = "",
                            PricePerUnit = 78.25,
                            ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                            UnitsAvailable = 29,
                            Images = new List<ItemImage>()
                            {
                                new ItemImage()
                                {
                                    ImagePath = @"~\Content\Images\ChipotleHat.jpg"
                                }
                            }
                        }
                    }
                };
                manager.Create(user, "P@ssword1");
            };
            if (!(context.Users.Any(x => x.UserName == "test@wholehogcafe.com")))
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
                    UserName = "test@wholehogcafe.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Whole Hog Spatula",
                        ItemDescription = "",
                        PricePerUnit = 33.65,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 89,

                    },
                    new Item()
                    {
                        ItemName = "Whole Hog Stationery",
                        ItemDescription = "",
                        PricePerUnit = 100.00,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Whole Hog Signage",
                        ItemDescription = "",
                        PricePerUnit = 89.25,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "Whole Hog Knives",
                        ItemDescription = "",
                        PricePerUnit = 25.00,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 200,

                    },new Item()
                    {
                        ItemName = "Whole Hog Shirts",
                        ItemDescription = "",
                        PricePerUnit = 26.00,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 340,

                    },new Item()
                    {
                        ItemName = "Whole Hog Screen Time",
                        ItemDescription = "",
                        PricePerUnit = 2622.00,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 50,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@cajunswharf.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 3
                {
                    PersonOfContact = "Rick Smith",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/3/2015"),
                    Balance = 500,
                    CompanyAddress = "2400 Cantrell Rd",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    UserName = "test@cajunswharf.com",
                    CompanyName = "Cajun's Wharf",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@cajunswharf.com",
                    CompanyWebsite = "www.cajunswharf.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Cajun's Warf Soda",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Beverages"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "Cajun's Oranges",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Produce"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Cajun's Signage",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "Cajun's License",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "Cajun's Parking Lot Space",
                        ItemDescription = "",
                        PricePerUnit = 50.65,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Real Estate"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "Cajun's Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@cafebossanova.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 4
                {
                    PersonOfContact = "Pam Michaels",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/4/2015"),
                    Balance = 500,
                    CompanyAddress = "2701 Kavanaugh Blvd #105",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    UserName = "test@cafebossanova.com",
                    CompanyName = "Cafe Bossa Nova",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@cafebossanova.com",
                    CompanyWebsite = "www.cafebossanova.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Bossa Nova License",
                        ItemDescription = "",
                        PricePerUnit = 230.25,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "Bossa Nova Wheat",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Dry Goods"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Bossa Nova Rice",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Dry Goods"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "Bossa Nova Knives",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "Bossa Nova Shirts",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "Bossa Nova Ice Cream Scoop",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@oneeleven.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 5
                {
                    PersonOfContact = "Baxley Porter",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/5/2015"),
                    Balance = 500,
                    CompanyAddress = "111 W Markham St",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72201,
                    UserName = "test@oneeleven.com",
                    CompanyName = "One Eleven",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@oneeleven.com",
                    CompanyWebsite = "www.oneelevenatthecapital.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "One Eleven Spatula",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "One Eleven Stationery",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "One Eleven Signage",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "One Eleven Knives",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "One Eleven Shirts",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "One Eleven Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@ejseatsanddrinks.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 6
                {
                    PersonOfContact = "Dane Bradford",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/6/2015"),
                    Balance = 500,
                    CompanyAddress = "523 Center St.",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "EJ's Eats and Drinks",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@ejseatsanddrinks.com",
                    UserName = "test@ejseatsanddrinks.com",
                    CompanyWebsite = "www.ejslittlerock.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Restaurant Equipment For Sale - EJ's",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "EJ's Stationery",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42

                    },new Item()
                    {
                        ItemName = "EJ's Signage",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "EJ's Knives",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "EJ's Shirts",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "EJ's Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@irianaspizza.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 7
                {
                    PersonOfContact = "Mike Rowe",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/7/2015"),
                    Balance = 500,
                    CompanyAddress = "201 E Markham St",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72201,
                    CompanyName = "Iriana's Pizza",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@irianaspizza.com",
                    UserName = "test@irianaspizza.com",
                    CompanyWebsite = "www.irianaspizza.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "EJ's Dough Rollers",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "EJ's Pizza Paper - Irania's Pizza",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "EJ's Signage",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "EJ's Knives By Irania's Pizza Restaurant",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "EJ's Irania's Shirts",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "EJ's Pizza Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,
                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@sushicafe.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 8
                {
                    PersonOfContact = "Bruce Smith",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/8/2015"),
                    Balance = 500,
                    CompanyAddress = "5823 Kavanaugh Blvd",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72205,
                    CompanyName = "Sushi Cafe",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@sushicafe.com",
                    UserName = "test@sushicafe.com",
                    CompanyWebsite = "www.sushicaferocks.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Sushi Cafe Bowls",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,
                    },
                    new Item()
                    {
                        ItemName = "Stationery By Sushi Cafe",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Signage - Sushi Cafe Material",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "Spoons And Delicates - Sushi Cafe",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "Sushi Shirts For Advertisement",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "Sushi Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@flyingfish.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 9
                {
                    PersonOfContact = "Jeremy Gibson",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/9/2015"),
                    Balance = 500,
                    CompanyAddress = "511 President Clinton Ave",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72201,
                    CompanyName = "Flying Fish",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@flyingfish.com",
                    UserName = "test@flyingfish.com",
                    CompanyWebsite = "www.flyingfishinthe.net",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Flying Fish Sliverware Stock",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "Flying Stationery",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Signs By Flying Fish",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,
                    },new Item()
                    {
                        ItemName = "Flying Fish Pots",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "Flying Fish Shirts",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 34,

                    },new Item()
                    {
                        ItemName = "Flyig Fish Hat Stock",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,
                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }
            if (!(context.Users.Any(x => x.UserName == "test@reddoor.com")))
            {
                var userToInsert = new BusinessUser //Todo Business User 10
                {
                    PersonOfContact = "Rose Arnolds",
                    PhoneNumber = "555-555-5555",
                    DateRegistered = DateTime.Parse("1/10/2015"),
                    Balance = 500,
                    CompanyAddress = "3701 Cantrell Rd.",
                    City = "Little Rock",
                    State = "Arkansas",
                    Postal = 72202,
                    CompanyName = "Red Door Restaraunt",
                    BusinessCategory = db.BusinessCategories.FirstOrDefault(x => x.CategoryName == "Restaraunt"),
                    Email = "test@reddoor.com",
                    UserName = "test@reddoor.com",
                    CompanyWebsite = "www.reddoorrestaraunt.com",
                    ItemsUpForSale = new List<Item>()
                {
                    new Item()
                    {
                        ItemName = "Red Door Spoons",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },
                    new Item()
                    {
                        ItemName = "Red Door Paper Products",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Stationery"),
                        UnitsAvailable = 42,

                    },new Item()
                    {
                        ItemName = "Signs By Red Door",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 6,

                    },new Item()
                    {
                        ItemName = "Red Door Knives",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Silverware"),
                        UnitsAvailable = 15,

                    },new Item()
                    {
                        ItemName = "Red Door Wine Bottles",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Beverages"),
                        UnitsAvailable = 34,

                    },
                        new Item()
                    {
                        ItemName = "Red Door Hats",
                        ItemDescription = "",
                        PricePerUnit = 15.50,
                        ItemCategory = db.ItemCategories.FirstOrDefault(x=>x.CategoryName == "Advertisement Media"),
                        UnitsAvailable = 24,

                    }
                }
                };
                manager.Create(userToInsert, "Password@123");
            }

        }
    }
}

