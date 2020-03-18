using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace MovieNight
{
    class DalManager
    {
        private string connectionString = "Server=DESKTOP-M6E4F8M;Database=MovieNight;User Id=caustic;Password=kage123";

        public string ConnectionString
        {
            get { return connectionString; }
        }


        private DalManager()
        {
        }               
        private static DalManager instance = null;
        public static DalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DalManager();
                }
                return instance;
            }
        }


        /// <summary>
        /// Returns all movies from the Movie Table
        /// </summary>
        /// <returns></returns>
        public List<Movie> GetMovies()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Movie> movies = new List<Movie>();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Movie", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new Movie(((DateTime)reader["release"]).ToShortDateString(), (int)(short)reader["movieId"], (string)reader["title"], (string)reader["movieDescription"]));
                }
                return movies;
            }
        }



        public List<Movie> GetMovies(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Movie> movies = new List<Movie>();
                SqlParameter sp = new SqlParameter();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Movie where title like @keyword", connection);
                sp.ParameterName = "@keyword".ToLower();
                sp.Value = "%" + keyword + "%";
                cmd.Parameters.Add(sp);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie(((DateTime)reader["release"]).ToShortDateString(), (int)(short)reader["movieId"], (string)reader["title"], (string)reader["movieDescription"]));
                    }
                    return movies;
                }
            }
        }

        public List<Actor> GetActors()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Actor> actors = new List<Actor>();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Actor", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    actors.Add(new Actor((string)reader["firstName"], (string)reader["lastName"]));
                }
                return actors;
            }
        }

        public List<Actor> GetActors(string firstNameKeyword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Actor> actors = new List<Actor>();
                SqlParameter sp = new SqlParameter();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Actor where firstName like @keyword", connection);
                sp.ParameterName = "@keyword".ToLower();
                sp.Value = "%" + firstNameKeyword + "%";
                cmd.Parameters.Add(sp);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        actors.Add(new Actor((string)reader["firstName"], (string)reader["lastName"]));
                    }
                }
                return actors;
            }
        }

        public List<Genre> GetGenres()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Genre> genres = new List<Genre>();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from genreType", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(new Genre((int)(short)reader["genreId"], (string)reader["genreName"]));
                }
                return genres;
            }
        }

        /// <summary>
        /// Returns a list of movies depending on <paramref name="genreName"/>
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        public List<Movie> GetMoviesByGenre(string genreName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Movie> movies = new List<Movie>();
                SqlParameter sp = new SqlParameter();

                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "Select title, movieDescription, " +
                    "from Movie " +
                    "inner join Genre on Movie.movieId = Genre.movieId " +
                    "inner join genreType on genreType.genreId = Genre.genreId " +
                    "where genreType.genreName = @keyword",
                    connection);

                sp.ParameterName = "@keyword".ToLower();
                sp.Value = "'" + genreName + "'";
                cmd.Parameters.Add(sp);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie(((DateTime)reader["release"]).ToShortDateString(), (int)(short)reader["movieId"], (string)reader["title"], (string)reader["movieDescription"]));
                    }
                }
                return movies;
            }
        }

        /// <summary>
        /// Adds genres to <paramref name="movies"/> movies.
        /// </summary>
        /// <param name="movies"></param>
        /// <returns></returns>
        public List<Movie> GetGenresOnMovies(List<Movie> movies)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlParameter sqlParameter = new SqlParameter();

                foreach (Movie movie in movies)
                {
                    SqlCommand cmd = new SqlCommand(
                        "Select genreName, genreId from genreType" +
                        "inner join Genre" +
                        "on genreType.genreId = Genre.genreId" +
                        "inner join Movie" +
                        "on Movie.movieId = Genre.movieId" +
                        "where movieId = @movieId", connection);

                    sqlParameter.ParameterName = "@movieId";
                    sqlParameter.Value = movie.MovieId;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        movie.Genres.Add(new Genre((int)(short)reader["genreId"],(string)reader["genreName"]));
                    }
                }
                return movies;
            }
        }
    }
}




