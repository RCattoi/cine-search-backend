namespace api_cine_search.Infra.Databases.Configurations
{
  public class DatabaseSettings
  {
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public string SetCollectionName(string collectionName) => collectionName;

  }
}