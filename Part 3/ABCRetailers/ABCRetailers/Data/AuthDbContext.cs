using System.Collections.Generic;
using ABCRetailers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ABCRetailers.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();



        //Add these two:
        public DbSet<Cart> Cart => Set<Cart>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

    }

}  