using Microsoft.EntityFrameworkCore;
using HamraKitab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamraKitab.DataAccess.Data
{
    public class ApplicationDbContext : DbContext    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Genre> Genres { get; set; }
    }
}
