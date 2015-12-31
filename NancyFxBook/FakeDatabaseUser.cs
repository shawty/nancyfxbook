using System;
using System.Collections.Generic;

namespace nancybook
{
  public class FakeDatabaseUser
  {
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Claims { get; set; }
    public string Password { get; set; }
  }
}