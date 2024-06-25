

namespace api_cine_search.Models
{
  public class UserSaltDetails
  {
    public int? Id { get; set; }
    public int? UserId { get; set; }
    public byte[]? Salt { get; set; }
    public int SaltSize { get; set; }
  }
}