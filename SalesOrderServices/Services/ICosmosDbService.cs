using SalesOrderServices.Model;

namespace SalesOrderServices.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<SalesOrder>> GetMultipleAsync(string query);
        Task<SalesOrder> GetAsync(string id);
        Task AddAsync(SalesOrder item);
        Task UpdateAsync(string id, SalesOrder item);
        Task DeleteAsync(string id);
    }
}
