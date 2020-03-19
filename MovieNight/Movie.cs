using System;
using System.Collections.Generic;
using System.Text;

namespace MovieNight
{
    public class Movie
    {
        private string releaseYear;

        public string ReleaseYear
        {
            get { return releaseYear; }
            set { releaseYear = value; }
        }

        private int movieId;

        public int MovieId
        {
            get { return movieId; }
            set { movieId = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string description;


        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private List<Genre> genres = new List<Genre>();

        public List<Genre> Genres
        {
            get { return genres; }
            set { genres = value; }
        }


        public Movie(string releaseYear, int movieId, string title, string description)
        {
            ReleaseYear = releaseYear;
            MovieId = movieId;
            Title = title;
            Description = description;
        }

        public Movie(string releaseYear, string title, string description)
        {
            ReleaseYear = releaseYear;
            Title = title;
            Description = description;
        }

        public Movie(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
