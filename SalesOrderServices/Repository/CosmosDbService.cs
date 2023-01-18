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
    public async Task AddAsync(Product item)
    {
        await _container.CreateItemAsync(item, new PartitionKey(item.Id));
    }
    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
    }
    public async Task<Product> GetAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<Product>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException) //For handling item not found and other exceptions
        {
            return null;
        }
    }
    public async Task<IEnumerable<Product>> GetMultipleAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
        var results = new List<Product>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
    public async Task UpdateAsync(string id, Product item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(id));
    }


  
}