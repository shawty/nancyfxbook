// //===========================================================================================
// // Project          : nancybook
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 25/04/2015
// // Module           : AlbumsModel.cs
// // Purpose          : Provides a view model holding an optional list of albums
// //===========================================================================================

using System.Collections.Generic;
using demodata.entities;

namespace nancybook.Models
{
  public class AlbumsModel
  {
    public bool AlbumsPresent { get; set; }
    public List<Album> Albums { get; set; }
  }
}