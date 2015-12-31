using System.Collections.Generic;
using Nancy.Security;

namespace nancybook
{
  public class AuthenticatedUser : IUserIdentity
  {
    public string UserName { get; set; }
    public IEnumerable<string> Claims { get; set; }
  }
}