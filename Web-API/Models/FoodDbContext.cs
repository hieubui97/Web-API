namespace Web_API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FoodDbContext : DbContext
    {
        public FoodDbContext()
            : base("name=FoodDbContext")
        {
        }

        public virtual DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
