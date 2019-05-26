using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieScraper
{
    class MainClass
    {
        public const string INPUT_URL = "https://www.imdb.com/chart/top?ref_=nv_mv_250";
        public const string IMDB_URL = "https://www.imdb.com";
        public const short NUM_MOVIES = 250; // The amount of movies you want to consider. 250 is the max.

        public static void Main(string[] args)
        {
            IMDBHtmlExtractor.OpenNewDocument(INPUT_URL);

            List<string> links = IMDBHtmlExtractor.ExtractMovieLinksFromList();
            List<Movie> movies = new List<Movie>();

            foreach (string link in links)
            {
                MovieBuilder.TryBuildMovie(link, out Movie movie);
                if (movie == null)
                    Logger.Warning("Failed to read movie from link: " + link);
                else
                    movies.Add(movie);
                Logger.Info($"Movie read: {movie.Title}");
            }

            IOrderedEnumerable<Movie> sortedMovies = movies.OrderBy(m => m.ReleaseDate);

            foreach (Movie movie in sortedMovies)
            {
                MovieOutputter.OutputMovie(movie);
            }

            Console.ReadKey();
        }
    }
}
