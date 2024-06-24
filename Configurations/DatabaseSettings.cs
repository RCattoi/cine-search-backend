namespace api_cine_search.Configurations
{
  public class DatabaseSettings
  {
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public string SetCollectionName(string collectionName) => collectionName;

  }
}