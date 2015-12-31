using System.Text;
using demodata;
using demodata.entities;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace nancybook
{
  public class CustomBootstrapper : DefaultNancyBootstrapper
  {
    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      base.ConfigureConventions(nancyConventions);

      Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/scripts", @"Scripts"));
      Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/fonts", @"fonts"));
      Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/images", @"Images"));
    }

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      container.Register<IDataProvider<Genre>>(new GenreDataProvider());
      container.Register<IDataProvider<Album>>(new AlbumDataProvider());
      container.Register<IDataProvider<Track>>(new TrackDataProvider());
      container.Register<IDataProvider<Artist>>(new ArtistDataProvider());

      container.Register<CacheService>().AsSingleton();
    }

    protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
    {
      base.ConfigureRequestContainer(container, context);
      container.Register<IUserMapper, DatabaseUser>();
    }

    protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
    {
      base.RequestStartup(container, pipelines, context);

      var formsAuthConfiguration = new FormsAuthenticationConfiguration
      {
        RedirectUrl = "~/account/login",
        UserMapper = container.Resolve<IUserMapper>()
      };

      FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
    }

    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
      base.ApplicationStartup(container, pipelines);

      // Add an error handler to catch our entity not found exceptions
      pipelines.OnError += (context, exception) =>
      {
        // If we've raised an EntityNotFound exception in our data layer
        if (exception is EntityNotFoundException)
          return new Response()
          {
            StatusCode = HttpStatusCode.NotFound,
            ContentType = "text/html",
            Contents = (stream) =>
            {
              var errorMessage = Encoding.UTF8.GetBytes("A Data Entity with the requested ID was not found in the database.");
              stream.Write(errorMessage, 0, errorMessage.Length);
            }
          };

        // If none of the above handles our exception, then pass it on as a 500
        throw exception;
      };
      // End error handler

      CookieBasedSessions.Enable(pipelines);

    }

  }
}