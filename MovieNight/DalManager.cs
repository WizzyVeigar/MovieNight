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
        /// Returns all movies from the Movie table
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

        /// <summary>
        /// Gets all movies containing <paramref name="keyword"/>
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets all actors from the Actor table
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Gets all actors containing <paramref name="NameKeyword"/>
        /// </summary>
        /// <param name="NameKeyword"></param>
        /// <returns></returns>
        public List<Actor> GetActors(string NameKeyword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                List<Actor> actors = new List<Actor>();
                SqlParameter sp = new SqlParameter();

                connection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Actor where firstName like @keyword OR lastName like @keyword", connection);
                sp.ParameterName = "@keyword".ToLower();
                sp.Value = "%" + NameKeyword + "%";
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
        /// <summary>
        /// Returns a list of all genres in the Database
        /// </summary>
        /// <returns></returns>
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
                    "Select title, movieDescription " +
                    "from Movie " +
                    "inner join Genre on Movie.movieId = Genre.movieId " +
                    "inner join genreType on genreType.genreId = Genre.genreId " +
                    "where genreType.genreName = @keyword",
                    connection);

                sp.ParameterName = "@keyword";
                sp.Value = genreName;
                cmd.Parameters.Add(sp);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie((string)reader["title"], (string)reader["movieDescription"]));
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
                        movie.Genres.Add(new Genre((int)(short)reader["genreId"], (string)reader["genreName"]));
                    }
                }
                return movies;
            }
        }
        /// <summary>
        /// Inserts <paramref name="a"/> into films, chaining those references together in the Starring table
        /// </summary>
        /// <param name="a"></param>
        public void InsertActorInFilms(Actor a)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Starring(movieId, actorId) values (@movieId, @actorId)", connection);

                for (int i = 0; i < a.StarringIn.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@movieId", a.StarringIn[i].MovieId));
                    cmd.Parameters.Add(new SqlParameter("@actorId", a.ActorId));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

        }
        /// <summary>
        /// Inserts an Actor into the db
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Actor InsertActor(Actor a)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Actor(firstName, lastName) Output inserted.actorId values (@fn, @ln)", connection);

                cmd.Parameters.Add(new SqlParameter("@fn", a.FirstName));
                cmd.Parameters.Add(new SqlParameter("@ln", a.LastName));

                a.ActorId = (int)(short)cmd.ExecuteScalar();
            }
            return a;
        }
        /// <summary>
        /// Inserts a movie into the db
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public Movie InsertMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Movie(title, release, movieDescription) Output inserted.movieId values (@title, @release, @desc)", connection);

                cmd.Parameters.Add(new SqlParameter("@title", m.Title));
                cmd.Parameters.Add(new SqlParameter("@release", m.ReleaseYear));
                cmd.Parameters.Add(new SqlParameter("@desc", m.Description));

                m.MovieId = (int)(short)cmd.ExecuteScalar();

            }
            return m;
        }
        /// <summary>
        /// Inserts a genre into the db
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public Genre InsertGenre(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into genreType(genreName) Output inserted.genreId values (@genre)", connection);

                cmd.Parameters.Add(new SqlParameter("@genre", g.GenreName));

                g.GenreId = (int)(short)cmd.ExecuteScalar();

            }
            return g;
        }
        /// <summary>
        /// Inserts references from what movie was added, to what genres it has
        /// </summary>
        /// <param name="m"></param>
        public void InsertMovieGenres(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Genre(genreId, movieId) values (@genreId, @movieId)", connection);

                for (int i = 0; i < m.Genres.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@genreId", m.Genres[i].GenreId));
                    cmd.Parameters.Add(new SqlParameter("@movieID", m.MovieId));
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }


            }
        }

        /// <summary>
        /// updates an existing Actor with information from <paramref name="a"/>
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public string UpdateActor(Actor a)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update Actor Set firstName = @fn, lastName = @ln where actorId = @actorId", connection);

                cmd.Parameters.Add(new SqlParameter("@actorId", a.ActorId));
                cmd.Parameters.Add(new SqlParameter("@fn", a.FirstName));
                cmd.Parameters.Add(new SqlParameter("@ln", a.LastName));
                cmd.ExecuteNonQuery();
            }
            return a.GetFullName() + " was updated";
        }
        /// <summary>
        /// updates an existing Movie with information from <paramref name="m"/>
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public string UpdateMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update Movie Set title = @title, release = @release, movieDescription = @desc where movieId = @movieId", connection);

                cmd.Parameters.Add(new SqlParameter("@movieId", m.MovieId));
                cmd.Parameters.Add(new SqlParameter("@title", m.Title));
                cmd.Parameters.Add(new SqlParameter("@release", m.ReleaseYear));
                cmd.Parameters.Add(new SqlParameter("@desc", m.Description));
                cmd.ExecuteNonQuery();

            }
            return m.Title + " was updated";
        }
        /// <summary>
        /// updates an existing Genre with information from <paramref name="g"/>
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public string UpdateGenre(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("update genreType Set genreName = @gname where genreId = @genreId", connection);

                cmd.Parameters.Add(new SqlParameter("@genreId", g.GenreId));
                cmd.Parameters.Add(new SqlParameter("@gname", g.GenreName));
                cmd.ExecuteNonQuery();
            }
            return g.GenreName + " was updated";
        }

        public string DeleteActor(Actor a)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("delete from Actor where actorId = @actorId", connection);

                cmd.Parameters.Add(new SqlParameter("@actorId", a.ActorId));
                cmd.ExecuteNonQuery();

            }
            return a.GetFullName() + " was deleted";
        }
        public string DeleteMovie(Movie m)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("delete from Movie where movieId = @movieId", connection);

                cmd.Parameters.Add(new SqlParameter("@movieId", m.MovieId));
                cmd.ExecuteNonQuery();

            }
            return m.Title + " was deleted";
        }
        public string DeleteGenre(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("delete from genreType where genreId = @genreId", connection);

                cmd.Parameters.Add(new SqlParameter("@genreId", g.GenreId));
                cmd.ExecuteNonQuery();

            }
            return g.GenreName + " was deleted";
        }
    }
}




