using Microsoft.EntityFrameworkCore;
using ServerRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerRepository
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {
        }

        DbSet<ServerEntity> Server { get; set; }
    }
}
