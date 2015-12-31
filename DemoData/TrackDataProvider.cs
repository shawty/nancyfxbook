// //===========================================================================================
// // Project          : demodata
// // Author           : Peter Shaw (Digital Solutions UK)
// // Date             : 22/04/2015
// // Module           : GenreData.cs
// // Purpose          : Simple ADO.NET crud interface to the tracks table
// //===========================================================================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using demodata.entities;

namespace demodata
{
  public class TrackDataProvider : IDataProvider<Track>
  {
    private readonly string _connectionString;

    public TrackDataProvider()
    {
      _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["nancydata"].ConnectionString;
      if(string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();
    }

    public Track CreateEntity(Track newEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "insert into tracks(filename,path,bitrate,channels,samplerate,duration,artist,title,tracknumber,yearreleased,albumid,genreid,artistid) values(@filename,@path,@bitrate,@channels,@samplerate,@duration,@title,@tracknumber,@yearreleased,@albumid,@genreid,@artistid);select cast(scope_identity() as int)";
        using (var insertCommand = new SqlCommand(sql, connection))
        {
          insertCommand.Parameters.AddWithValue("@filename", newEntity.FileName);
          insertCommand.Parameters.AddWithValue("@path", newEntity.Path);
          insertCommand.Parameters.AddWithValue("@bitrate", newEntity.BitRate);
          insertCommand.Parameters.AddWithValue("@channels", newEntity.Channels);
          insertCommand.Parameters.AddWithValue("@samplerate", newEntity.SampleRate);
          insertCommand.Parameters.AddWithValue("@duration", newEntity.Duration);
          insertCommand.Parameters.AddWithValue("@title", newEntity.Title);
          insertCommand.Parameters.AddWithValue("@tracknumber", newEntity.TrackNumber);
          insertCommand.Parameters.AddWithValue("@yearreleased", newEntity.YearReleased);
          insertCommand.Parameters.AddWithValue("@albumid", newEntity.AlbumId);
          insertCommand.Parameters.AddWithValue("@genreid", newEntity.GenreId);
          insertCommand.Parameters.AddWithValue("@artistid", newEntity.ArtistId);
          var newId = (int)insertCommand.ExecuteScalar();
          newEntity.Pkid = newId;
        }

        connection.Close();
      }

      return newEntity;
    }

    public Track GetEntityById(int id)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      Track result;

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select top 1 pkid,filename,path,bitrate,channels,samplerate,duration,title,tracknumber,yearreleased,albumid,genreid,artistid from tracks where pkid = @pkid";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          selectCommand.Parameters.AddWithValue("@pkid", id);
          using (var dataReader = selectCommand.ExecuteReader())
          {
            if (dataReader.HasRows)
            {
              dataReader.Read();
              result = new Track
              {
                Pkid = (int)dataReader["pkid"],
                FileName = (string)dataReader["filename"],
                Path = (string)dataReader["path"],
                BitRate = (int)dataReader["bitrate"],
                Channels = (int)dataReader["channels"],
                SampleRate = (int)dataReader["samplerate"],
                Duration = TimeSpan.Parse((string)dataReader["duration"]),
                Title = (string)dataReader["title"],
                TrackNumber = (int)dataReader["tracknumber"],
                YearReleased = (int)dataReader["yearreleased"],
                AlbumId = (int)dataReader["albumid"],
                GenreId = (int)dataReader["genreid"],
                ArtistId = (int)dataReader["artistid"]
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

    public List<Track> GetAll()
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      List<Track> results = new List<Track>();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "select * from tracks";
        using (var selectCommand = new SqlCommand(sql, connection))
        {
          using (var dataReader = selectCommand.ExecuteReader())
          {
            while (dataReader.Read())
            {
              results.Add(new Track
              {
                Pkid = (int)dataReader["pkid"],
                FileName = (string)dataReader["filename"],
                Path = (string)dataReader["path"],
                BitRate = (int)dataReader["bitrate"],
                Channels = (int)dataReader["channels"],
                SampleRate = (int)dataReader["samplerate"],
                Duration = TimeSpan.Parse((string)dataReader["duration"]),
                Title = (string)dataReader["title"],
                TrackNumber = (int)dataReader["tracknumber"],
                YearReleased = (int)dataReader["yearreleased"],
                AlbumId = (int)dataReader["albumid"],
                GenreId = (int)dataReader["genreid"],
                ArtistId = (int)dataReader["artistid"]
              });
            }
          }
        }

        connection.Close();
      }

      return results;
    }

    public Track UpdateEntity(Track updatedEntity)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "update tracks set filename = @filename, path = @path, bitrate = @bitrate, channels = @channels, samplerate = @samplerate, duration = @duration, title = @title, tracknumber = @tracknumber, yearreleased = @yearreleased, albumid = @albumid, genreid = @genreid, artistid = @artistid where pkid = @pkid";
        using (var updateCommand = new SqlCommand(sql, connection))
        {
          updateCommand.Parameters.AddWithValue("@pkid", updatedEntity.Pkid);
          updateCommand.Parameters.AddWithValue("@filename", updatedEntity.FileName);
          updateCommand.Parameters.AddWithValue("@path", updatedEntity.Path);
          updateCommand.Parameters.AddWithValue("@bitrate", updatedEntity.BitRate);
          updateCommand.Parameters.AddWithValue("@channels", updatedEntity.Channels);
          updateCommand.Parameters.AddWithValue("@samplerate", updatedEntity.SampleRate);
          updateCommand.Parameters.AddWithValue("@duration", updatedEntity.Duration);
          updateCommand.Parameters.AddWithValue("@title", updatedEntity.Title);
          updateCommand.Parameters.AddWithValue("@tracknumber", updatedEntity.TrackNumber);
          updateCommand.Parameters.AddWithValue("@yearreleased", updatedEntity.YearReleased);
          updateCommand.Parameters.AddWithValue("@albumid", updatedEntity.AlbumId);
          updateCommand.Parameters.AddWithValue("@genreid", updatedEntity.GenreId);
          updateCommand.Parameters.AddWithValue("@artistid", updatedEntity.GenreId);
          updateCommand.ExecuteNonQuery();
        }

        connection.Close();
      }

      return updatedEntity;
    }

    public void DeleteEntity(Track entityToDelete)
    {
      if (string.IsNullOrEmpty(_connectionString))
        throw new ConnectionStringNotFoundException();

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        const string sql = "delete from tracks where pkid = @pkid";
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