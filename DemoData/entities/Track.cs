// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : Track.cs
// // Purpose          : Entity representing a music track
// //===========================================================================================

using System;

namespace demodata.entities
{
  public class Track
  {
    public int Pkid { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public int BitRate { get; set; }
    public int Channels { get; set; }
    public int SampleRate { get; set; }
    public TimeSpan Duration { get; set; }
    public string Title { get; set; }
    public int TrackNumber { get; set; }
    public int YearReleased { get; set; }
    public int AlbumId { get; set; }
    public int GenreId { get; set; }
    public int ArtistId { get; set; }
    public string Artist { get; set; }
  }
}