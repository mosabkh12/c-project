namespace Library.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class sign_up12 : DbContext
    {
        public sign_up12()
        //: base("name=sign_up7")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<request>().ToTable("request");
        }

        public virtual DbSet<request> request { get; set; }
    }
}
