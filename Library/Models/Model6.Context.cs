
namespace Library.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web.Services.Description;

    public partial class sign_up14 : DbContext
    {
        public sign_up14()
        //: base("name=sign_up7")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<messagee>().ToTable("messagee");
        }

        public virtual DbSet<messagee> messagee { get; set; }
    }
}
