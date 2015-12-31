// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : GenreData.cs
// // Purpose          : Simple ADO.NET crud interface to the artists table
// //===========================================================================================

using System.Collections.Generic;
using System.Data.SqlClient;
using demodata.entities;

namespace demodata
{
  public class ArtistDataProvider : IDataProvider<Artist>
  {
    private readonly string _connectionString;

    public ArtistDataProvider()
    {
      _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["nancydata"].ConnectionString;
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();
    }

    public Artist CreateEntity(Artist newEntity)
    {
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using(SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "insert into artists(artistname) values(@artist);select cast(scope_identity() as int)";
        using(var insertCommand = new SqlCommand(sql, connection))
        {
          insertCommand.Parameters.AddWithValue("@artist", newEntity.ArtistName);
          var newId = (int) insertCommand.ExecuteScalar();
          newEntity.Pkid = newId;
        }

        connection.Close();
      }

      return newEntity;
    }

    public Artist GetEntityById(int id)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      Artist result;

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select top 1 pkid,artistname from  artists where pkid = @pkid";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          selectCommand.Parameters.AddWithValue("@pkid", id);
          using(var dataReader = selectCommand.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              dataReader.Read();
              result = new Artist
              {
                Pkid = (int) dataReader["pkid"],
                ArtistName = (string)dataReader["artistname"]
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

    public List<Artist> GetAll()
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      List<Artist> results = new List<Artist>();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select * from artists";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          using (var dataReader = selectCommand.ExecuteReader())
          {
            while (dataReader.Read())
            {
              results.Add(new Artist
              {
                Pkid = (int)dataReader["pkid"],
                ArtistName = (string)dataReader["artistname"]
              });              
            }
          }
        }

        connection.Close();
      }

      return results;
    }

    public Artist UpdateEntity(Artist updatedEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "update artists set artistname = @artistname where pkid = @pkid";
        using (var updateCommand = new SqlCommand(sql, connection))
        {
          updateCommand.Parameters.AddWithValue("@artistname", updatedEntity.ArtistName);
          updateCommand.Parameters.AddWithValue("@pkid", updatedEntity.Pkid);
          updateCommand.ExecuteNonQuery();
        }

        connection.Close();
      }

      return updatedEntity;
    }

    public void DeleteEntity(Artist entityToDelete)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "delete from artists where pkid = @pkid";
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