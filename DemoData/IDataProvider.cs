// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : IDataProvider.cs
// // Purpose          : COntract Interface for the data providers
// //===========================================================================================

using System.Collections.Generic;

namespace demodata
{
  public interface IDataProvider<T>
  {
    T CreateEntity(T newEntity);
    T GetEntityById(int id);
    List<T> GetAll();
    T UpdateEntity(T updatedEntity);
    void DeleteEntity(T entityToDelete);

  }
}