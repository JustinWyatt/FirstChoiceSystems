using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceSystems.Models.DBModels;

namespace FirstChoiceSystems.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class BusinessUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BusinessUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }   

        public string PersonOfContact { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Postal { get; set; }
        public string CompanyWebsite { get; set; }
        public decimal Balance { get; set; }
        public virtual BusinessCategory BusinessCategory { get; set; }
        public int AccountNumber { get; set; }
        public DateTime DateRegistered { get; set; }
        public virtual List<PurchaseItem> Purchases { get; set; }
        public virtual ICollection<Item> ItemsUpForSale { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<BusinessUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { 
           
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IEntity<int> && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current != null && HttpContext.Current.User != null
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntity<int>)entity.Entity).CreatedOn = DateTime.Now;
                    ((IEntity<int>)entity.Entity).CreatedBy = currentUsername;
                    }

                ((IEntity<int>)entity.Entity).ModifiedOn = DateTime.Now;
                ((IEntity<int>)entity.Entity).ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<ItemImage> Images { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}