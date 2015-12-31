using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class SessionRoutes : NancyModule
  {
    public SessionRoutes() : base("/session")
    {
      Get[@"/"] = _ => View["session/index.html"];

      Get[@"/viewsessionvars"] = _ =>
      {
        var sessionVariables = new SessionDemo
        {
          FullName = (string) Session["FullName"],
          EmailAddress = (string) Session["EmailAddress"],
          ResidencePlace = (string)Session["ResidencePlace"]
        };
        return View["getsessionvars.html", sessionVariables];
      };

      Post[@"/setsessionvars"] = _ =>
      {
        var sessionVariables = this.Bind<SessionDemo>();
        Session["FullName"] = sessionVariables.FullName;
        Session["EmailAddress"] = sessionVariables.EmailAddress;
        Session["ResidencePlace"] = sessionVariables.ResidencePlace;
        return View["session/setsessionvars.html"];
      };

      Get[@"/clearsessionvars"] = _ =>
      {
        Session.DeleteAll();
        return View["session/index.html"];
      };

    }
  }
}