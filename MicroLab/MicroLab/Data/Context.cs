using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroLab.Models;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        //public DbSet<MiniFormula> MiniFormulas { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<InputOption> InputOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //modelBuilder.Entity<Variable>()
            //    .HasOptional<Variable>(s => s.FirstVariable)
            //    .WithOptionalDependent(ad => ad.FirstVariable);
            //modelBuilder.Entity<Variable>()
            //    .HasOptional<Variable>(s => s.SecondVariable)
            //    .WithOptionalDependent(ad => ad.SecondVariable);
            //modelBuilder.Entity<Variable>()
            //    .HasRequired<Formula>(s => s.Formula)
            //    .WithMany(g => g.Variables)
            //    .HasForeignKey<int>(s => s.FormulaId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Formula>()
            //.HasMany<MiniFormula>(g => g.MiniFormulas)
            //.WithRequired(s => s.Formula)
            //.WillCascadeOnDelete(false);
            //modelBuilder.Entity<Formula>()
            //.HasMany<Variable>(g => g.Variables)
            //.WithRequired(s => s.Formula)
            //.WillCascadeOnDelete(false);
        }
    }
}
