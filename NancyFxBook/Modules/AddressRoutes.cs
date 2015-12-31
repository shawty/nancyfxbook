// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : AddressRoutes.cs
// // Purpose          : Provides the basic routes for nancy in response to leaf nodes hanging off /address
// //===========================================================================================

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;
using Nancy.Validation;

namespace nancybook.modules
{
  public class AddressRoutes : NancyModule
  {
    public AddressRoutes() : base("/address")
    {
      Get[@"/"] = _ => View["address/index"];

      Post[@"/save"] = _ =>
      {
        var myAddress = this.Bind<Address>();
        var result = this.Validate(myAddress);

        return result.IsValid ? View["address/display", myAddress] : View["address/error"];
      };

      Get[@"/{id}"] = parameters =>
      {
        int recId = parameters.id;
        var record = new Address();
        return Negotiate
          .WithModel(record)
          .WithHeader("X-CustomHeader", "Some custom value");
      };

    }
  }
}