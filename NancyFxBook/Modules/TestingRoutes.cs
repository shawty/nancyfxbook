using Nancy;

namespace nancybook.modules
{
  public class TestingRoutes : NancyModule
  {
    public TestingRoutes()
    {
      //Get[@"/"] = _ => "Hello World";

      Post[@"/save"] = _ =>
      {
        if (!Request.Form.Name.HasValue || !Request.Form.Email.HasValue)
        {
          return Response.AsRedirect("/posterror");
        }

        if (Request.Form.Name == "Existing Person" && Request.Form.Email == "existing@anemail.com")
        {
          return HttpStatusCode.Conflict;
        }

        return HttpStatusCode.OK;
      };

    }
  }
}