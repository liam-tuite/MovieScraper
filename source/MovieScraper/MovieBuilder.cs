using System;

namespace MovieScraper
{
    /// <summary>
    /// A class for building Movie objects from HTML code.
    /// </summary>
    public static class MovieBuilder
    {
        /// <summary>
        /// Attempt to build a Movie object from its IMDb page.
        /// </summary>
        /// <param name="link">The link to the movie's IMDb page.</param>
        /// <param name="movie">The resulting Movie object (null if failed).</param>
        /// <returns>true if successful, false if not.</returns>
        public static bool TryBuildMovie(string link, out Movie movie)
        {
            try
            {
                movie = new Movie()
                {
                    PageLink = link,

                    Title = IMDBHtmlExtractor.ExtractMovieTitle(link),
                    ReleaseDate = ParseReleaseDate(IMDBHtmlExtractor.ExtractReleaseDateString(link))
                };

                return true;
            }
            catch (Exception e)
            {
                Logger.Warning($"[{nameof(MovieBuilder)}.{nameof(TryBuildMovie)}] Failed to build the movie from link: {link}\n{e.Message}");
                movie = null;
                return false;
            }
        }

        /// <summary>
        /// Parse a DateTime value from the raw release date, eg: "5 December 2004"
        /// </summary>
        /// <param name="rawReleaseDate"></param>
        /// <returns></returns>
        private static DateTime ParseReleaseDate(string rawReleaseDate)
        {
            string[] tokens = rawReleaseDate.Split(' ');
            
            int day = short.Parse(tokens[0]);
            int month = ParseMonth(tokens[1]);
            int year = short.Parse(tokens[2]);

            try
            {
                return new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Logger.Warning($"[{nameof(MovieBuilder)}.{nameof(ParseReleaseDate)}] Date out of range: {tokens[0] + tokens[1] + tokens[2]}");
                throw e;
            }
        }

        private static int ParseMonth(string rawMonth)
        {
            switch(rawMonth)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    Logger.Warning($"[{nameof(MovieBuilder)}.{nameof(ParseMonth)}] Could not parse month from value: {rawMonth}");
                    throw new ArgumentException();
            }
        }
    }
}