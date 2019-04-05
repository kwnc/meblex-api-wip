using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Meblex.API.Context
{
    public class MeblexDbContext:DbContext
    {
        public MeblexDbContext(DbContextOptions<MeblexDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CustomSizeForm> CustomSizeForms { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PieceOfFurniture> Furniture { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
