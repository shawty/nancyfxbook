// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 25/04/2015
// // Module           : AlbumsModel.cs
// // Purpose          : Provides a view model holding an optional list of tracks
// //===========================================================================================

using System.Collections.Generic;
using demodata.entities;

namespace nancybook.Models
{
  public class TracksModel
  {
    public bool TracksPresent { get; set; }
    public List<Track> Tracks { get; set; }
  }
}