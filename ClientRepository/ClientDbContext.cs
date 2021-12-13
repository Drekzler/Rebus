using ClientRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientRepository
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {
        }
        public DbSet<MessageEntity> Messages { get; set; }
    }
}
