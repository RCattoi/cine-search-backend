using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_cine_search.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cine_search.Infra.Db
{
    public class AppDbContext : DbContext
    {
       public DbSet<UserModel> Users { get; set; }

       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
       {
        
       }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);
       } 
    }
}