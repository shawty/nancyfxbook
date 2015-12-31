// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 28/04/2015
// // Module           : AlbumsModel.cs
// // Purpose          : Provides a view model holding an optional list of genres
// //===========================================================================================

using System.Collections.Generic;
using demodata.entities;

namespace nancybook.Models
{
  public class GenresModel
  {
    public bool GenresPresent { get; set; }
    public List<Genre> Genres { get; set; }
  }
}