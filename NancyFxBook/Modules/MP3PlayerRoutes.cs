// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 23/04/2015
// // Module           : mp3player.cs
// // Purpose          : Handles routing for base routes on the MP3 player portion of the demo site
// //===========================================================================================

using System.IO;
using System.Linq;
using demodata;
using demodata.entities;
using nancybook.Models;
using Nancy;
using Nancy.ModelBinding;

namespace nancybook.modules
{
  public class MP3Player : NancyModule
  {
    private readonly IDataProvider<Track> _trackDataProvider;
    private readonly IDataProvider<Album> _albumDataProvider;
    private readonly IDataProvider<Genre> _genreDataProvider;
    private readonly IDataProvider<Artist> _artistDataProvider;

    public MP3Player(
      IDataProvider<Track> trackDataProvider,
      IDataProvider<Album> albumDataProvider,
      IDataProvider<Genre> genreDataProvider,
      IDataProvider<Artist> artistDataProvider) : base("mp3player")
    {
      _trackDataProvider = trackDataProvider;
      _albumDataProvider = albumDataProvider;
      _genreDataProvider = genreDataProvider;
      _artistDataProvider = artistDataProvider;

      Get["/"] = _ =>
      {
        var homeViewModel = new PlayerModel {TrackAvailable = false};
        return View["mp3player/home", homeViewModel];
      };

      Post["/"] = parameters =>
      {
        InboundTrackInfo inboundTrackInfo = this.Bind();

        PlayerModel homeViewModel;
        var id = inboundTrackInfo.TrackId;

        if(id != null && id != 0)
        {
          homeViewModel = new PlayerModel { TrackAvailable = true, TrackToPlay = _trackDataProvider.GetEntityById(id) };
          homeViewModel.TrackToPlay.Artist = _artistDataProvider.GetEntityById(homeViewModel.TrackToPlay.ArtistId).ArtistName;
          return View["mp3player/home", homeViewModel];          
        }

        homeViewModel = new PlayerModel { TrackAvailable = false };
        return View["mp3player/home", homeViewModel];
      };

      Get[@"/playtrack/{id}"] = parameters =>
      {
        Track track = _trackDataProvider.GetEntityById(parameters.id);
        Stream fileStream = File.OpenRead(Path.Combine(track.Path, track.FileName));
        return Response.FromStream(fileStream, "video/mp4");
      };

      Get["/albums"] = _ =>
      {
        var allAlbums = _albumDataProvider.GetAll().OrderBy(r => r.Artist).ToList();
        var albumsModel = new AlbumsModel {AlbumsPresent = allAlbums.Any(), Albums = allAlbums };
        return View["mp3player/albumlist", albumsModel];
      };

      Get["/artists"] = _ =>
      {
        var allArtists = _artistDataProvider.GetAll().OrderBy(a => a.Pkid).ToList();
        var artistsModel = new ArtistsModel { ArtistsPresent = allArtists.Any(), Artists = allArtists };
        return View["mp3player/artistlist", artistsModel];
      };

      Get["/genres"] = _ =>
      {
        var allGenres = _genreDataProvider.GetAll().OrderBy(g => g.Pkid).ToList();
        var genresModel = new GenresModel { GenresPresent = allGenres.Any(), Genres = allGenres };
        return View["mp3player/genrelist", genresModel];
      };

      Get["/tracksbygenre/{id}"] = parameters =>
      {
        int id = parameters.id;
        var genreTracks = _trackDataProvider.GetAll().Where(t => t.GenreId.Equals(id)).ToList();
        foreach (Track track in genreTracks)
        {
          track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
        }
        var tracksModel = new TracksModel { TracksPresent = genreTracks.Any(), Tracks = genreTracks };
        return View["mp3player/tracksbygenre", tracksModel];
      };

      Get["/tracksbyartist/{id}"] = parameters =>
      {
        int id = parameters.id;
        var artistTracks = _trackDataProvider.GetAll().Where(t => t.ArtistId.Equals(id)).ToList();
        foreach (Track track in artistTracks)
        {
          track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
        }
        var tracksModel = new TracksModel { TracksPresent = artistTracks.Any(), Tracks = artistTracks };
        return View["mp3player/tracksbyartist", tracksModel];
      };

      Get["/tracksbyalbum/{id}"] = parameters =>
      {
        int id = parameters.id;
        var albumTracks = _trackDataProvider.GetAll().Where(t => t.AlbumId.Equals(id)).ToList();
        foreach (Track track in albumTracks)
        {
          track.Artist = _artistDataProvider.GetEntityById(track.ArtistId).ArtistName;
        }
        var tracksModel = new TracksModel { TracksPresent = albumTracks.Any(), Tracks = albumTracks };
        return View["mp3player/tracksbyalbum", tracksModel];
      };

    }

  }
}