using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RunningStatContext : DbContext
    {
        public RunningStatContext(DbContextOptions<RunningStatContext> options) : base(options)
        {

        }

        public DbSet<RunningStatConverted> RunningStats { get; set; }
    }
}