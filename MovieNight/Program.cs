using System;
using System.Collections.Generic;

namespace MovieNight
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieManager movieManager = new MovieManager();
            List<Actor> actors = movieManager.GetActors();
            List<Movie> movies = movieManager.GetMovies();
            List<Genre> genres = movieManager.GetGenres();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?.." +
                    "\n" +
                    "\n" +
                    "1. Insert Actor \n" +
                    "2. Insert Movie \n" +
                    "3. Insert Genre \n" +
                    "4. Update Actor \n" +
                    "5. Update Movie \n" +
                    "6. Update Genre \n" +
                    "7. Delete Actor \n" +
                    "8. Delete Movie \n" +
                    "9. Delete Genre \n");

                string str = "";

                while (string.IsNullOrWhiteSpace(str))
                {
                    str = Console.ReadLine();
                }

                Console.Clear();
                switch (str)
                {
                    #region CreateSection
                    case "1":
                    {
                        Console.WriteLine("Enter First name");
                        string fn = Console.ReadLine();
                        Console.WriteLine("Enter Last name");
                        string ln = Console.ReadLine();
                        Actor actor = new Actor(fn, ln);

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("What movies is this actor in? (Type done if you are finished)\n");
                            if (Console.ReadLine().ToLower() == "done")
                            {
                                break;
                            }
                            for (int i = 0; i < movies.Count; i++)
                            {
                                Console.WriteLine(movies[i].Title + "    " + movies[i].ReleaseYear);
                            }
                            str = "";
                            while (string.IsNullOrWhiteSpace(str))
                            {
                                str = Console.ReadLine();
                            }
                            for (int i = 0; i < movies.Count; i++)
                            {
                                if (str.ToLower() == movies[i].Title.ToLower())
                                {
                                    actor.StarringIn.Add(movies[i]);
                                    break;
                                }
                            }
                        }
                        Console.WriteLine(movieManager.InsertActor(actor).GetFullName() + " has been added");
                        actors.Add(actor);
                        break;

                    }
                    case "2":
                    {
                        Movie movie;
                        List<Genre> movieGenres = movieManager.GetGenres();

                        Console.WriteLine("Enter Title");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter release date (YYYY-MM-DD)");
                        string release = Console.ReadLine();
                        Console.WriteLine("Enter Description");
                        string desc = Console.ReadLine();

                        movie = new Movie(release, title, desc);

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("What genres is this movie? (Type done if you are finished)\n");
                            if (Console.ReadLine().ToLower() == "done")
                            {
                                break;
                            }
                            for (int i = 0; i < movieGenres.Count; i++)
                            {
                                Console.WriteLine(movieGenres[i].GenreName);
                            }
                            str = "";
                            while (string.IsNullOrWhiteSpace(str))
                            {
                                str = Console.ReadLine();
                            }
                            for (int i = 0; i < movieGenres.Count; i++)
                            {
                                if (str.ToLower() == movieGenres[i].GenreName.ToLower())
                                {
                                    movie.Genres.Add(movieGenres[i]);
                                    break;
                                }
                            }
                        }
                        Console.WriteLine(movieManager.InsertMovie(movie).Title + " has been added");
                        movies.Add(movie);
                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine("Enter Genre name");
                        Genre genre = new Genre(Console.ReadLine());
                        Console.WriteLine(movieManager.InsertGenre(genre).GenreName + " has been added");
                        genres.Add(genre);
                        break;
                    }
                    #endregion
                    #region UpdateSection
                    case "4":
                    {
                        Console.WriteLine("Which actor needs an update?");
                        foreach (Actor actor in actors)
                        {
                            Console.WriteLine(actor.ActorId + "  " + actor.GetFullName());
                        }
                        try
                        {
                            for (int i = 0; i < actors.Count; i++)
                            {
                                if (int.Parse(Console.ReadLine()) == actors[i].ActorId)
                                {
                                    Console.WriteLine("Enter First name");
                                    actors[i].FirstName = Console.ReadLine();

                                    Console.WriteLine("Enter Last name");
                                    actors[i].LastName = Console.ReadLine();

                                    Console.WriteLine(movieManager.UpdateActor(actors[i]));
                                    break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Something went wrong! try again");
                        }
                        break;
                    }
                    case "5":
                    {
                        Console.WriteLine("Which movie needs an update?");
                        foreach (Movie movie in movies)
                        {
                            Console.WriteLine(movie.MovieId + "  " + movie.Title);
                        }
                        try
                        {
                            for (int i = 0; i < movies.Count; i++)
                            {
                                if (int.Parse(Console.ReadLine()) == movies[i].MovieId)
                                {
                                    Console.WriteLine("What is the title?");
                                    movies[i].Title = Console.ReadLine();
                                    Console.WriteLine("What date was it released? (YYYY-MM-DD)");
                                    movies[i].ReleaseYear = Console.ReadLine();
                                    Console.WriteLine("What is the description?");
                                    movies[i].Description = Console.ReadLine();
                                    movieManager.UpdateMovie(movies[i]);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! try again");
                        }
                        break;
                    }
                    case "6":
                    {
                        Console.WriteLine("Which genre needs an update?");
                        foreach (Genre genre in genres)
                        {
                            Console.WriteLine(genre.GenreId + "  " + genre.GenreName);
                        }
                        try
                        {
                            for (int i = 0; i < genres.Count; i++)
                            {
                                Console.WriteLine("Enter Genre name to update");

                                if (int.Parse(Console.ReadLine()) == genres[i].GenreId)
                                {
                                    Console.WriteLine("Enter new genre name");
                                    genres[i].GenreName = Console.ReadLine();
                                    Console.WriteLine(movieManager.UpdateGenre(genres[i]));
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! try again");
                        }
                        break;
                    }
                    #endregion
                    #region deleteSection
                    case "7":
                    {
                        Console.WriteLine("Which Actor would you like to delete? (use their ID)");
                        foreach (Actor actor in actors)
                        {
                            Console.WriteLine(actor.ActorId + "  " + actor.GetFullName());
                        }
                        try
                        {
                            int input = int.Parse(Console.ReadLine());
                            for (int i = 0; i < actors.Count; i++)
                            {
                                if (input == actors[i].ActorId)
                                    Console.WriteLine(movieManager.DeleteActor(actors[i]));
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! try again");
                        }
                        break;
                    }
                    case "8":
                    {
                        Console.WriteLine("Which Movie would you like to delete? (use their ID)");
                        foreach (Movie movie in movies)
                        {
                            Console.WriteLine(movie.MovieId + "  " + movie.Title);
                        }
                        try
                        {
                            int input = int.Parse(Console.ReadLine());
                            for (int i = 0; i < movies.Count; i++)
                            {
                                if (input == movies[i].MovieId)
                                    movieManager.DeleteMovie(movies[i]);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! try again");
                        }

                        break;
                    }
                    case "9":
                    {
                        Console.WriteLine("Which Genre would you like to delete? (use their ID)");
                        foreach (Genre genre in genres)
                        {
                            Console.WriteLine(genre.GenreId + "  " + genre.GenreName);
                        }
                        try
                        {
                            int input = int.Parse(Console.ReadLine());
                            for (int i = 0; i < genres.Count; i++)
                            {
                                if (input == genres[i].GenreId)
                                    movieManager.DeleteGenre(genres[i]);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Something went wrong! try again");
                        }
                        break;
                    }
                    #endregion
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
                Console.WriteLine("Press any key to continues...");
                Console.ReadLine();
            }
            #region Movie Night part 1
            //Console.WriteLine("Search for a movie title..");
            //string str = "";

            //while (string.IsNullOrWhiteSpace(str))
            //{
            //    str = Console.ReadLine();
            //}        
            //foreach (Movie movie in movieManager.GetMovies(str))
            //{
            //    Console.WriteLine(movie.Title);
            //}
            //str = string.Empty;

            //Console.WriteLine("Search for Actors..");
            //while (string.IsNullOrWhiteSpace(str))
            //{
            //    str = Console.ReadLine();
            //}
            //foreach (Actor actor in movieManager.GetActors(str))
            //{
            //    Console.WriteLine(actor.GetFullName() + "\n");
            //}
            //str = string.Empty;

            //foreach (Genre genre in movieManager.GetGenres())
            //{
            //    Console.WriteLine(genre.GenreId +"  "+genre.GenreName);
            //}

            //Console.WriteLine("Search for movies with the number of a genre...");
            //while (string.IsNullOrWhiteSpace(str))
            //{
            //    str = Console.ReadLine();
            //}
            //foreach (Movie movie in movieManager.GetMoviesByGenre(str))
            //{
            //    Console.WriteLine(movie.Title + "    "+movie.Genres);
            //}

            //Console.ReadLine();

            ////gets all movies
            //foreach (Movie movie in movieManager.GetMovies())
            //{
            //    Console.WriteLine(movie.Title + " " + movie.ReleaseYear + "\n");
            //}
            ////Gets all actors
            //foreach (Actor actor in movieManager.GetActors())
            //{
            //    Console.WriteLine(actor.FirstName +" "+ actor.LastName +" "+ actor.ActorId);
            //}
            #endregion
        }
    }
}
