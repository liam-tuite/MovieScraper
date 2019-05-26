using System;

namespace MovieScraper
{
    /// <summary>
    /// A movie with a title, release date and link to its page.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The title of the movie.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The movie's date of release.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// The link to this movie's IMDb page.
        /// </summary>
        public string PageLink { get; set; }
    }
}