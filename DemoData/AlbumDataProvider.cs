// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : GenreData.cs
// // Purpose          : Simple ADO.NET crud interface to the albums table
// //===========================================================================================

using System.Collections.Generic;
using System.Data.SqlClient;
using demodata.entities;

namespace demodata
{
  public class AlbumDataProvider : IDataProvider<Album>
  {
    private readonly string _connectionString;

    public AlbumDataProvider()
    {
      _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["nancydata"].ConnectionString;
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();
    }

    public Album CreateEntity(Album newEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "insert into albums(albumname, artist) values(@albumname,@artist);select cast(scope_identity() as int)";
        using (var insertCommand = new SqlCommand(sql, connection))
        {
          insertCommand.Parameters.AddWithValue("@albumname", newEntity.AlbumName);
          insertCommand.Parameters.AddWithValue("@artist", newEntity.Artist);
          var newId = (int)insertCommand.ExecuteScalar();
          newEntity.Pkid = newId;
        }

        connection.Close();
      }

      return newEntity;
    }

    Album IDataProvider<Album>.GetEntityById(int id)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      Album result;

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select top 1 pkid,albumname,artist from  albums where pkid = @pkid";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          selectCommand.Parameters.AddWithValue("@pkid", id);
          using (var dataReader = selectCommand.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              dataReader.Read();
              result = new Album
              {
                Pkid = (int)dataReader["pkid"],
                AlbumName = (string)dataReader["albumname"],
                Artist = (string)dataReader["artist"]
              };
            }
            else
            {
              throw new EntityNotFoundException();
            }
          }
        }

        connection.Close();
      }

      return result;
    }

    List<Album> IDataProvider<Album>.GetAll()
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      List<Album> results = new List<Album>();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select * from albums";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          using (var dataReader = selectCommand.ExecuteReader())
          {
            while (dataReader.Read())
            {
              results.Add(new Album
              {
                Pkid = (int)dataReader["pkid"],
                AlbumName = (string)dataReader["albumname"],
                Artist = (string)dataReader["artist"]
              });
            }
          }
        }

        connection.Close();
      }

      return results;
    }

    public Album UpdateEntity(Album updatedEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "update albums set albumname = @albumname, artist = @artist where pkid = @pkid";
        using (var updateCommand = new SqlCommand(sql, connection))
        {
          updateCommand.Parameters.AddWithValue("@albumname", updatedEntity.AlbumName);
          updateCommand.Parameters.AddWithValue("@artist", updatedEntity.Artist);
          updateCommand.Parameters.AddWithValue("@pkid", updatedEntity.Pkid);
          updateCommand.ExecuteNonQuery();
        }

        connection.Close();
      }

      return updatedEntity;
    }

    public void DeleteEntity(Album entityToDelete)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "delete from albums where pkid = @pkid";
        using (var updateCommand = new SqlCommand(sql, connection))
        {
          updateCommand.Parameters.AddWithValue("@pkid", entityToDelete.Pkid);
          updateCommand.ExecuteNonQuery();
        }

        connection.Close();
      }
    }

  }
}