using System.Threading.Tasks;
using Test.RebusClient.Components.Models;

namespace Test.RebusClient.Repository
{
    public interface IClientRebusRepository
    {
        public Task AddAsync(Messages message);
    }
}
