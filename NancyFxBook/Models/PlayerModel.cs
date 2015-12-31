// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 23/04/2015
// // Module           : PlayerModel.cs
// // Purpose          : Provides a view model for the MP3 player main player/home page view
// //===========================================================================================

using demodata.entities;

namespace nancybook.Models
{
  public class PlayerModel
  {
    public bool TrackAvailable { get; set; } 
    public Track TrackToPlay { get; set; }
  }
}