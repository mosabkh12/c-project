
namespace Library.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Web.Services.Description;

    public partial class sign_up15 : DbContext
    {
        public sign_up15()
        //: base("name=sign_up7")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<message3>().ToTable("message3");
        }

        public virtual DbSet<message3> message3 { get; set; }
    }
}
