using System;
using System.Collections.Generic;

namespace MovieNight
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieManager movieManager = new MovieManager();

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
                    case "1":
                    {
                        Console.WriteLine("Enter First name");
                        string fn = Console.ReadLine();
                        Console.WriteLine("Enter Last name");
                        string ln = Console.ReadLine();
                        Console.WriteLine(movieManager.InsertActor(new Actor(fn, ln)).GetFullName() + " has been added");
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
                        break;
                    }
                    case "3":
                    {
                        Console.WriteLine("Enter Genre name");
                        string genre = Console.ReadLine();
                        Console.WriteLine(movieManager.InsertGenre(new Genre(genre)).GenreName + " has been added");
                        break;
                    }
                    case "4":
                    {

                        break;
                    }
                    case "5":
                    {

                        break;
                    }
                    case "6":
                    {

                        break;
                    }
                    case "7":
                    {
                        Console.WriteLine("Which Actor would you like to delete?");
                        foreach (Actor actor in movieManager.GetActors())
                        {
                            Console.WriteLine(actor.ActorId + "  " + actor.GetFullName() + "\n");
                        }
                        break;
                    }
                    case "8":
                    {
                        Console.WriteLine("Which Movie would you like to delete?");
                        foreach (Movie movie in movieManager.GetMovies())
                        {
                            Console.WriteLine(movie.MovieId + "  " + movie.Title + "\n");
                        }

                        break;
                    }
                    case "9":
                    {

                        Console.WriteLine("Which genre would you like to delete?");
                        foreach (Genre genre in movieManager.GetGenres())
                        {
                            Console.WriteLine(genre.GenreName + "\n");
                        }

                        //movieManager.DeleteGenre()
                        break;
                    }

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
