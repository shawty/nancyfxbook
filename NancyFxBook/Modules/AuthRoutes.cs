// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : AuthRoutes.cs
// // Purpose          : Provides the routes for nancy in response to leaf nodes hanging off /auth
// //===========================================================================================

using Nancy;
using Nancy.Security;

namespace nancybook.modules
{
  public class AuthRoutes : NancyModule
  {
    public AuthRoutes() : base("/auth")
    {
      this.RequiresAuthentication();

      Get[@"/"] = _ => View["auth/index"];
    }
  }
}