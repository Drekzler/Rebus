using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.RebusClient.Components.Models;
using Test.RebusClient.Repository;

namespace Test.RebusClient.EventHandler
{
    public class MessageEventHandler : IHandleMessages<Messages>
    {
        private readonly IClientRebusRepository repo;

        public MessageEventHandler(IClientRebusRepository repo)
        {
            this.repo = repo;
        }


        public async Task Handle(Messages message)
        {
           await repo.AddAsync(message);
        }
    }
}
