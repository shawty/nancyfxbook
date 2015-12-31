using System.ComponentModel.DataAnnotations;

namespace nancybook.Models
{
  public class LoginParams
  {
    [Required]
    public string LoginName { get; set; }

    [Required]
    public string Password { get; set; }

  }
}