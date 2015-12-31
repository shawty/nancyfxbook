// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : BaseRoutes.cs
// // Purpose          : Provides the basic routes for nancy in response to leaf nodes hanging off /
// //===========================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.Responses;

namespace nancybook.modules
{
  public class BaseRoutes : NancyModule
  {
    public BaseRoutes()
    {
      Get[@"/"] = _ => Response.AsFile("Pages/Index.html", "text/html");
    }
  }
}