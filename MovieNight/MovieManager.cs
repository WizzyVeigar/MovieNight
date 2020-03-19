using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight
{
    class MovieManager
    {
        public List<Movie> GetMovies()
        {
            return DalManager.Instance.GetMovies();
        }

        public List<Movie> GetMovies(string titleKeyword)
        {
            return DalManager.Instance.GetMovies(titleKeyword);
        }

        public List<Actor> GetActors()
        {
            return DalManager.Instance.GetActors();
        }

        public List<Actor> GetActors(string firstNameKeyword)
        {
            return DalManager.Instance.GetActors(firstNameKeyword);
        }

        public List<Genre> GetGenres()
        {
            return DalManager.Instance.GetGenres();
        }

        public List<Movie> GetMoviesByGenre(string str)
        {
            return DalManager.Instance.GetMoviesByGenre(str);
        }

        public List<Movie> GetGenresOnMovies(List<Movie> movies)
        {
           return DalManager.Instance.GetGenresOnMovies(movies);
        }

        public Actor InsertActor(Actor a)
        {
            return DalManager.Instance.InsertActor(a);
        }

        public Movie InsertMovie(Movie m)
        {
            return DalManager.Instance.InsertMovie(m);
        }
    }
}