using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Shared.DataManagement
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisualizationClasses>()
                .HasKey(bc => new { bc.ClassId, bc.VisualizationId, bc.Id });
            modelBuilder.Entity<VisualizationClasses>()
                .HasOne(bc => bc.Classes)
                .WithMany(b => b.VisualizationClasses)
                .HasForeignKey(bc => bc.ClassId);
            modelBuilder.Entity<VisualizationClasses>()
                .HasOne(bc => bc.Visualization)
                .WithMany(c => c.VisualizationClasses)
                .HasForeignKey(bc => bc.VisualizationId);
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Visualization> Visualizations { get; set; }
        public DbSet<VisualizationClasses> VisualizationClasses { get; set; }


    }
}
