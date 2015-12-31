using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nancybook.Models
{
  public class Address
  {
    [Required]
    public int RecordId { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [RegularExpression(@"^[0-9]{1,3}\s.*")]
    public string BuildingAddress { get; set; }

    [Range(18, 150)]
    public int Age { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

  }
}