using System.IO;
using System.Linq;
using demodata;
using demodata.entities;
using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class MP3PlayerRoutes : NancyModule
  {
    private readonly IDataProvider<Track> _trackDataProvider;
    private readonly IDataProvider<Album> _albumDataProvider;
    private readonly IDataProvider<Genre> _genreDataProvider;
    private readonly IDataProvider<Artist> _artistDataProvider;

    public MP3PlayerRoutes(
      IDataProvider<Track> trackDataProvider,
      IDataProvider<Album> albumDataProvider,
      IDataProvider<Genre> genreDataProvider,
      IDataProvider<Artist> artistDataProvider)
      : base("mp3player")
    {
      _trackDataProvider = trackDataProvider;
      _albumDataProvider = albumDataProvider;
      _genreDataProvider = genreDataProvider;
      _artistDataProvider = artistDataProvider;

      Get["/"] = Index;
      Post["/"] = PostIndex;
      Get[@"/playtrack/{id}"] = PlayTrack;
      Get["/albums"] = Albums;
      Get["/artists"] = Artists;
      Get["/genres"] = Genres;
      Get["/tracksbygenre/{id}"] = TracksByGenre;
      Get["/tracksbyartist/{id}"] = TracksByArtist;
      Get["/tracksbyalbum/{id}"] = TracksByAlbum;

    }

    private dynamic Index(dynamic parameters)
    {
      var homeViewModel = new PlayerModel { TrackAvailable = false };
      return View["mp3player/home", homeViewModel];
    }

    private dynamic PostIndex(dynamic parameters)
    {
      InboundTrackInfo inboundTrackInfo = this.Bind();

      PlayerModel homeViewModel;
      var id = inboundTrackInfo.TrackId;

      if (id != null && id != 0)
      {
        homeViewModel = new PlayerModel { TrackAvailable = true, TrackToPlay = _trackDataProvider.GetEntityById(id) };
        homeViewModel.TrackToPlay.Artist = _artistDataProvider.GetEntityById(homeViewModel.TrackToPlay.ArtistId).ArtistName;
        return View["mp3player/home", homeViewModel];
      }

      homeViewModel = new PlayerModel { TrackAvailable = false };
      return View["mp3player/home", homeViewModel];
    }

    private dynamic PlayTrack(dynamic parameters)
    {
      Track track = _trackDataProvider.GetEntityById(parameters.id);
      Stream fileStream = File.OpenRead(Path.Combine(track.Path, track.FileName));
      return Response.FromStream(fileStream, "video/mp4");
    }

    private dynamic Albums(dynamic parameters)
    {
      var allAlbums = _albumDataProvider.GetAll().OrderBy(r => r.Artist).ToList();
      var albumsModel = new AlbumsModel { AlbumsPresent = allAlbums.Any(), Albums = allAlbums };
      return View["mp3player/albumlist", albumsModel];
    }

    private dynamic Artists(dynamic parameters)
    {
      var allArtists = _artistDataProvider.GetAll().OrderBy(a => a.Pkid).ToList();
      var artistsModel = new ArtistsModel { ArtistsPresent = allArtists.Any(), Artists = allArtists };
      return View["mp3player/artistlist", artistsModel];
    }

    private dynamic Genres(dynamic parameters)
    {
      var allGenres = _genreDataProvider.GetAll().OrderBy(g => g.Pkid).ToList();
      var genresModel = new GenresModel { GenresPresent = allGenres.Any(), Genres = allGenres };
      return View["mp3player/genrelist", genresModel];
    }

    private dynamic TracksByAlbum(dynamic parameters)
    {
      int id = parameters.id;
      var albumTracks = _trackDataProvider.GetAll().Where(t => t.AlbumId.Equals(id)).ToList();
      foreach (Track track in albumTracks)
      {
        track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
      }
      var tracksModel = new TracksModel { TracksPresent = albumTracks.Any(), Tracks = albumTracks };
      return View["mp3player/tracksbyalbum", tracksModel];
    }

    private dynamic TracksByArtist(dynamic parameters)
    {
      int id = parameters.id;
      var artistTracks = _trackDataProvider.GetAll().Where(t => t.ArtistId.Equals(id)).ToList();
      foreach (Track track in artistTracks)
      {
        track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
      }
      var tracksModel = new TracksModel { TracksPresent = artistTracks.Any(), Tracks = artistTracks };
      return View["mp3player/tracksbyartist", tracksModel];
    }

    private dynamic TracksByGenre(dynamic parameters)
    {
      int id = parameters.id;
      var genreTracks = _trackDataProvider.GetAll().Where(t => t.GenreId.Equals(id)).ToList();
      foreach (Track track in genreTracks)
      {
        track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
      }
      var tracksModel = new TracksModel { TracksPresent = genreTracks.Any(), Tracks = genreTracks };
      return View["mp3player/tracksbygenre", tracksModel];
    }

  }
}