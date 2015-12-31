using System.Linq;
using nancybook.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class AccountRoutes : NancyModule
  {
    public AccountRoutes()
      : base("/account")
    {
      Get[@"/login"] = GetLogin;
      Post[@"/login"] = PostLogin;
      Get[@"/logout"] = _ => this.LogoutAndRedirect("~/account/login"); // This is not a method due to how Logout works
    }

    private dynamic GetLogin(dynamic parameters)
    {
      return View["account/login"];
    }

    private dynamic PostLogin(dynamic parameters)
    {
      var loginParams = this.Bind<LoginParams>();

      DatabaseUser db = new DatabaseUser();

      var user = db.Users.FirstOrDefault(x => x.UserName.Equals(loginParams.LoginName) && x.Password.Equals(loginParams.Password));

      if (user == null)
      {
        return View["account/loginerror"];
      }

      return this.Login(user.UserId, fallbackRedirectUrl: "~/auth");
    }

  }
}