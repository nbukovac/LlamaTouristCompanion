namespace TestBotApplication
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<UserLanguage> UserLanguage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Languages>()
                .HasMany(e => e.UserLanguage)
                .WithRequired(e => e.Languages)
                .HasForeignKey(e => e.LanguageId)
                .WillCascadeOnDelete(false);
        }
    }
}
