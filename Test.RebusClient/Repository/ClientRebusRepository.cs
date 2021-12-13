using ClientRepository;
using ClientRepository.Models;
using ServerRepository;
using System;
using System.Threading.Tasks;
using Test.RebusClient.Components.Models;

namespace Test.RebusClient.Repository
{
    public class ClientRebusRepository : IClientRebusRepository
    {
        private readonly ClientDbContext Data;
        public ClientRebusRepository(ClientDbContext db)
        {
            Data = db;
        }
        public async Task AddAsync(Messages message)
        {
            var entity = new MessageEntity()
            {
                HiddenMessage = message.HiddenMessage,
                Message = message.Message
            };

            await Data.Messages.AddAsync(entity);
            await Data.SaveChangesAsync();
        }
    }
}
