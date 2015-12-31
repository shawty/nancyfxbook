// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : GenreData.cs
// // Purpose          : Simple ADO.NET crud interface to the genres table
// //===========================================================================================

using System.Collections.Generic;
using System.Data.SqlClient;
using demodata.entities;

namespace demodata
{
  public class GenreDataProvider : IDataProvider<Genre>
  {
    private readonly string _connectionString;

    public GenreDataProvider()
    {
      _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["nancydata"].ConnectionString;
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();
    }

    public Genre CreateEntity(Genre newEntity)
    {
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using(SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "insert into genres(genredescription) values(@genre);select cast(scope_identity() as int)";
        using(var insertCommand = new SqlCommand(sql, connection))
        {
          insertCommand.Parameters.AddWithValue("@genre", newEntity.GenreDescription);
          var newId = (int) insertCommand.ExecuteScalar();
          newEntity.Pkid = newId;
        }

        connection.Close();
      }

      return newEntity;
    }

    public Genre GetEntityById(int id)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      Genre result;

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select top 1 pkid,genredescription from  genres where pkid = @pkid";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          selectCommand.Parameters.AddWithValue("@pkid", id);
          using(var dataReader = selectCommand.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              dataReader.Read();
              result = new Genre
              {
                Pkid = (int) dataReader["pkid"],
                GenreDescription = (string) dataReader["genredescription"]
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

    public List<Genre> GetAll()
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      List<Genre> results = new List<Genre>();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select * from genres";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          using (var dataReader = selectCommand.ExecuteReader())
          {
            while (dataReader.Read())
            {
              results.Add(new Genre
              {
                Pkid = (int)dataReader["pkid"],
                GenreDescription = (string)dataReader["genredescription"]
              });              
            }
          }
        }

        connection.Close();
      }

      return results;
    }

    public Genre UpdateEntity(Genre updatedEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "update genres set genredescription = @genredescription where pkid = @pkid";
        using (var updateCommand = new SqlCommand(sql, connection))
        {
          updateCommand.Parameters.AddWithValue("@genredescription", updatedEntity.GenreDescription);
          updateCommand.Parameters.AddWithValue("@pkid", updatedEntity.Pkid);
          updateCommand.ExecuteNonQuery();
        }

        connection.Close();
      }

      return updatedEntity;
    }

    public void DeleteEntity(Genre entityToDelete)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "delete from genres where pkid = @pkid";
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