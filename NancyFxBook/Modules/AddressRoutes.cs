using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;
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

    }
  }
}