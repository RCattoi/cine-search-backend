namespace api_cine_search.Models
{
  public class UserModel
  {

    public int? Id { get; set; }
    public string? Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}