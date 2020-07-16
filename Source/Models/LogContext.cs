using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Source.Models
{
    public class LogContext : DbContext
    {        

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoLog> TiposLog { get; set; }
        public DbSet<Ambiente> Ambientes { get; set; }
        public DbSet<Log> Logs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CodenationProjetoPratico;Trusted_Connection=True");

        }


    }
}
