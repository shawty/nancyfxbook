using nancybook.modules;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace NancyTesting
{
  class GetTest
  {
    [Test]
    public void SimpleTestOne()
    {
      // Arrange
      var browser = new Browser(with => with.Module(new TestingRoutes()));

      // Act
      var response = browser.Get("/test");

      // Assert
      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      
    }

    [Test]
    public void PostTest_Should_Return_200()
    {
      // Arrange
      var browser = new Browser(with => with.Module(new TestingRoutes()));

      // Act
      var response = browser.Post("/test/save/", (with) =>
      {
        with.HttpRequest();
        with.FormValue("Name", "Joe Smith");
        with.FormValue("Email", "joe@anemail.com");
      });

      // Assert
      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
    }

    [Test]
    public void PostTest_Should_Return_409()
    {
      // Arrange
      var browser = new Browser(with => with.Module(new TestingRoutes()));

      // Act
      var response = browser.Post("/test/save/", (with) =>
      {
        with.HttpRequest();
        with.FormValue("Name", "Existing Person");
        with.FormValue("Email", "existing@anemail.com");
      });

      // Assert
      Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);

    }

    [Test]
    public void PostTest_Should_Return_500()
    {
      // Arrange
      var browser = new Browser(with => with.Module(new TestingRoutes()));

      // Act
      var response = browser.Post("/test/save/", (with) =>
      {
        with.HttpRequest();
        with.FormValue("NOTName", "Existing Person");
        with.FormValue("NOTEmail", "existing@anemail.com");
      });

      // Assert
      response.ShouldHaveRedirectedTo("/posterror");

    }

  }
}
