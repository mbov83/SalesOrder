using Microsoft.Azure.Cosmos;
using SalesOrderServices.Model;
using SalesOrderServices.Services;

public class CosmosDbService : ICosmosDbService
{
    private Container _container;
    public CosmosDbService(
        CosmosClient cosmosDbClient,
        string databaseName,
        string containerName)
    {
        _container = cosmosDbClient.GetContainer(databaseName, containerName);
    }
    public async Task AddAsync(SalesOrder item)
    {
        await _container.CreateItemAsync(item, new PartitionKey(item.Id));
    }
    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<SalesOrder>(id, new PartitionKey(id));
    }
    public async Task<SalesOrder> GetAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<SalesOrder>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException) //For handling item not found and other exceptions
        {
            return null;
        }
    }
    public async Task<IEnumerable<SalesOrder>> GetMultipleAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<SalesOrder>(new QueryDefinition(queryString));
        var results = new List<SalesOrder>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
    public async Task UpdateAsync(string id, SalesOrder item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(id));
    }


  
}