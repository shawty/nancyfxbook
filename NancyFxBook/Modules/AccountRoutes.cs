// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : AccountRoutes.cs
// // Purpose          : Provides the routes for nancy in response to leaf nodes hanging off /accounts
// //===========================================================================================

using System.Linq;
using nancybook.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class AccountRoutes : NancyModule
  {
    public AccountRoutes() : base("/account")
    {
      Get[@"/login"] = _ => View["account/login"];

      Post[@"/login"] = _ =>
      {
        var loginParams = this.Bind<LoginParams>();
        FakeDatabaseUser user;

        DatabaseUser db = new DatabaseUser();

        user = db.Users.FirstOrDefault(x => x.UserName.Equals(loginParams.LoginName) && x.Password.Equals(loginParams.Password));

        if(user== null)
        {
          return View["account/loginerror"];
        }

        return this.Login(user.UserId, fallbackRedirectUrl: "~/auth");
      };

      Get[@"/logout"] = _ => this.LogoutAndRedirect("~/account/login");

    }
  }
}