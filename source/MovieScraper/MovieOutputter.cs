using System.IO;

namespace MovieScraper
{
    public class MovieOutputter
    {
        private static string path = "output.txt";

        /// <summary>
        /// How many movies have been outputted so far.
        /// </summary>
        private static int movieCount = 0;

        /// <summary>
        /// Write a movie's details to the output file.
        /// </summary>
        /// <param name="movie"></param>
        public static void OutputMovie(Movie movie)
        {
            File.AppendAllText(path, ++movieCount + ": " + movie.Title.Trim() + ", " + movie.ReleaseDate.Day + '/' + movie.ReleaseDate.Month + '/' + movie.ReleaseDate.Year + '\n');
        }
    }
}   