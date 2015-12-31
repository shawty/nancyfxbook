// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 28/04/2015
// // Module           : AlbumsModel.cs
// // Purpose          : Provides a view model holding an optional list of artists
// //===========================================================================================

using System.Collections.Generic;
using demodata.entities;

namespace nancybook.Models
{
  public class ArtistsModel
  {
    public bool ArtistsPresent { get; set; }
    public List<Artist> Artists { get; set; }
  }
}