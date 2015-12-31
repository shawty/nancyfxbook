using Nancy;

namespace nancybook.modules
{
  public class FileRoutes : NancyModule
  {
    public FileRoutes()
      : base("/file")
    {
      Get[@"/"] = Index;
      Get[@"/css"] = Css;
      Get[@"/js"] = Js;
      Get[@"/pdf"] = Pdf;
    }

    private dynamic Index(dynamic parameters)
    {
      return View["File/Index"];
    }

    private dynamic Css(dynamic parameters)
    {
      return Response.AsFile("Content/bootstrap.css", "text/css");
    }

    private dynamic Js(dynamic parameters)
    {
      return Response.AsFile("Scripts/bootstrap.js", "application/javascript");
    }

    private dynamic Pdf(dynamic parameters)
    {
      return Response.AsFile("Content/riffnew.pdf", "application/pdf");
    }

  }
}