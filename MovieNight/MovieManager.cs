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
            a = DalManager.Instance.InsertActor(a);
            DalManager.Instance.InsertActorInFilms(a);
            return a;
        }

        public Movie InsertMovie(Movie m)
        {
            m = DalManager.Instance.InsertMovie(m);
            DalManager.Instance.InsertMovieGenres(m);
            return m; 
        }

        public Genre InsertGenre(Genre g)
        {
            return DalManager.Instance.InsertGenre(g);
        }

        public string UpdateActor(Actor a)
        {
            return DalManager.Instance.UpdateActor(a);
        }
        public string UpdateMovie(Movie m)
        {
            return DalManager.Instance.UpdateMovie(m);
        }
        public string UpdateGenre(Genre g)
        {
            return DalManager.Instance.UpdateGenre(g);
        }



        public string DeleteActor(Actor a)
        {
            return DalManager.Instance.DeleteActor(a);
        }
        public string DeleteMovie(Movie m)
        {
            return DalManager.Instance.DeleteMovie(m);
        }
        public string DeleteGenre(Genre g)
        {
            return DalManager.Instance.DeleteGenre(g);
        }
    }
}