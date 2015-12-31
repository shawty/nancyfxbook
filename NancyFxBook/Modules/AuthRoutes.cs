using Nancy;
using Nancy.Security;

namespace nancybook.modules
{
  public class AuthRoutes : NancyModule
  {
    public AuthRoutes() : base("/auth")
    {
      this.RequiresAuthentication();

      Get[@"/"] = Index;
    }

    private dynamic Index(dynamic parameters)
    {
      return View["auth/index"];
    }

  }
}