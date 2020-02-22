using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcAuthNBlog.Models;

namespace MvcAuthNBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Category> Category { get; set; }
        /*public DbSet<BlogCategory> BlogCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategory>().HasKey(pt => new { pt.BlogId, pt.CategoryId });
            modelBuilder.Entity<BlogCategory>().HasOne(pt => pt.Blog).WithMany(pt =>BlogCategory).HasForeignKey(p=>p.BlogId);
            modelBuilder.Entity<BlogCategory>().HasOne(pt => pt.Category).WithMany(pt => BlogCategory).HasForeignKey(p => p.CategoryId);

        }*/
    }
}
