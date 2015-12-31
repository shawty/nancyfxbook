// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : FluentValidationRoutes.cs
// // Purpose          : Provides the basic routes for nancy in response to leaf nodes hanging off /fluentvalidation
// //===========================================================================================

using System.Collections.Generic;
using System.Linq;
using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class FluentValidationRoutes : NancyModule
  {
    public FluentValidationRoutes()
      : base("/fluentvalidation")
    {
      Get[@"/"] = _ =>
      {
        SetDefaultClasses();
        FluentValidationExample startObject = new FluentValidationExample();
        return View["index.html", startObject];
      };

      Post[@"/validate"] = _ =>
      {
        SetDefaultClasses();
        var validationResult = this.BindAndValidate<FluentValidationExample>();

        if (!ModelValidationResult.IsValid)
        {
          if (ModelValidationResult.Errors.ContainsKey("UserName"))
          {
            ViewBag.UserNameClasses = "form-group has-error";
            ViewBag.UserNameHelpClasses = "help-block hidden";
            ViewBag.UserNameMessageClasses = "help-block";
            ViewBag.UserNameMessages =
              (from error in ModelValidationResult.Errors.Where(e => e.Key.Equals("UserName"))
               from msg in error.Value
               select msg.ErrorMessage).ToList().First();
          }

          if (ModelValidationResult.Errors.ContainsKey("EmailAddress"))
          {
            ViewBag.EmailAddressClasses = "form-group has-error";
            ViewBag.EmailAddressHelpClasses = "help-block hidden";
            ViewBag.EmailAddressMessageClasses = "help-block";
            ViewBag.EmailAddressMessages =
              (from error in ModelValidationResult.Errors.Where(e => e.Key.Equals("EmailAddress"))
               from msg in error.Value
               select msg.ErrorMessage).ToList().First();
          }

          if (ModelValidationResult.Errors.ContainsKey("Password"))
          {
            ViewBag.PasswordClasses = "form-group has-error";
            ViewBag.PasswordHelpClasses = "help-block hidden";
            ViewBag.PasswordMessageClasses = "help-block";

            ViewBag.PasswordMessages =
              (from error in ModelValidationResult.Errors.Where(e => e.Key.Equals("Password"))
               from msg in error.Value
               select msg.ErrorMessage).ToList().First();
          }

          if (ModelValidationResult.Errors.ContainsKey("PinCode"))
          {
            ViewBag.PinCodeClasses = "form-group has-error";
            ViewBag.PinCodeHelpClasses = "help-block hidden";
            ViewBag.PinCodeMessageClasses = "help-block";

            ViewBag.PinCodeMessages =
              (from error in ModelValidationResult.Errors.Where(e => e.Key.Equals("PinCode"))
               from msg in error.Value
               select msg.ErrorMessage).ToList().First();
          }

          return View["index.html", validationResult];
        }

        return View["validated.html"];
      };

    }

    private void SetDefaultClasses()
    {
      ViewBag.UserNameClasses = "form-group";
      ViewBag.UserNameHelpClasses = "help-block";
      ViewBag.UserNameMessageClasses = "help-block hidden";
      ViewBag.UserNameMessages = "";

      ViewBag.EmailAddressClasses = "form-group";
      ViewBag.EmailAddressHelpClasses = "help-block";
      ViewBag.EmailAddressMessageClasses = "help-block hidden";
      ViewBag.EmailAddressMessages = "";

      ViewBag.PasswordClasses = "form-group";
      ViewBag.PasswordHelpClasses = "help-block";
      ViewBag.PasswordMessageClasses = "help-block hidden";
      ViewBag.PasswordMessages = "";

      ViewBag.PinCodeClasses = "form-group";
      ViewBag.PinCodeHelpClasses = "help-block";
      ViewBag.PinCodeMessageClasses = "help-block hidden";
      ViewBag.PinCodeMessages = "";
    }

  }
}