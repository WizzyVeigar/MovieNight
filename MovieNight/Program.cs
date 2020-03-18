using System;

namespace MovieNight
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieManager movieManager = new MovieManager();

            Console.WriteLine("Search for a movie title..");
            string str = "";


            while (string.IsNullOrWhiteSpace(str))
            {
                str = Console.ReadLine();
            }        
            foreach (Movie movie in movieManager.GetMovies(str))
            {
                Console.WriteLine(movie.Title);
            }
            str = string.Empty;

            Console.WriteLine("Search for Actors..");
            while (string.IsNullOrWhiteSpace(str))
            {
                str = Console.ReadLine();
            }
            foreach (Actor actor in movieManager.GetActors(str))
            {
                Console.WriteLine(actor.FirstName + "\n");
            }
            str = string.Empty;

            foreach (Genre genre in movieManager.GetGenres())
            {
                Console.WriteLine(genre.GenreId +"  "+genre.GenreName);
            }

            Console.WriteLine("Search for movies with the number of a genre...");
            while (string.IsNullOrWhiteSpace(str))
            {
                str = Console.ReadLine();
            }
            foreach (Movie movie in movieManager.GetMoviesByGenre(str))
            {
                Console.WriteLine(movie.Title + "    "+movie.Genres);
            }


            //gets all movies
            foreach (Movie movie in movieManager.GetMovies())
            {
                Console.WriteLine(movie.Title + " " + movie.ReleaseYear + "\n");
            }
            //Gets all actors
            foreach (Actor actor in movieManager.GetActors())
            {
                Console.WriteLine(actor.FirstName +" "+ actor.LastName +" "+ actor.ActorId);
            }

            Console.ReadLine();
        }
    }
}
