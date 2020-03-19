using System.Collections.Generic;

namespace MovieNight
{
    public class Genre
    {
        private int genreId;

        public int GenreId
        {
            get { return genreId; }
            set { genreId = value; }
        }

        private string genreName;

        public string GenreName
        {
            get { return genreName; }
            set { genreName = value; }
        }

        public Genre(int genreId, string genreName)
        {
            GenreId = genreId;
            GenreName = genreName;
        }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }
    }
}