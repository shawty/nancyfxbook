using Nancy;

namespace nancybook.modules
{
  public class BaseRoutes : NancyModule
  {
    public BaseRoutes()
    {
      Get[@"/"] = Index;
    }

    private dynamic Index(dynamic parameters)
    {
      return View["Home/Index"];
    }

  }
}