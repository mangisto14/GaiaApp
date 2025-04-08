using System.Collections.Generic;
using GaiaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GaiaApi.Data
{

    public class GaiaDbContext : DbContext
    {
        public GaiaDbContext(DbContextOptions<GaiaDbContext> options) : base(options) { }

        public DbSet<OperationRecord> Operations { get; set; }
    }
}