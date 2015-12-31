// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : Album.cs
// // Purpose          : Entity representing a music album
// //===========================================================================================
namespace demodata.entities
{
  public class Album
  {
    public int Pkid { get; set; }
    public string AlbumName { get; set; }
    public string Artist { get; set; }
  }
}