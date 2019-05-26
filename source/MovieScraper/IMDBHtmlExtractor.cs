using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MovieScraper
{
    /// <summary>
    /// A class for extracting text from HTML code from IMDB webpages.
    /// </summary>
    public class IMDBHtmlExtractor
    {
        private static string currentDocumentLink;
        private static HtmlDocument currentDocument;

        public static void OpenNewDocument(string url)
        {
            if(currentDocumentLink == url)
            {
                return;
            }

            Logger.Test(!string.IsNullOrWhiteSpace(url),
                $"[{nameof(IMDBHtmlExtractor)}.{nameof(OpenNewDocument)}] Invalid argument: {nameof(url)} is null or white space");

            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }

            Logger.Test(!string.IsNullOrWhiteSpace(htmlCode),
                $"[{nameof(IMDBHtmlExtractor)}.{nameof(OpenNewDocument)}] Failed to download html from url: {url}");

            currentDocument = new HtmlDocument();
            currentDocument.LoadHtml(htmlCode);
            currentDocumentLink = url;
        }
        
        /// <summary>
        /// Extracts the links to each of the top 250 movie's IMDb page.
        /// </summary>
        /// <returns>A list of links to each movie's IMDb page.</returns>
        public static List<string> ExtractMovieLinksFromList()
        {
            List<string> links = new List<string>();

            List<HtmlNode> titleColumnNodes = currentDocument.DocumentNode.Descendants("td").Where((n) => n.HasClass("titleColumn")).ToList();

            for(int i = 0; i < MainClass.NUM_MOVIES; i++)
            {
                links.Add(MainClass.IMDB_URL + titleColumnNodes[i].Descendants("a").First().GetAttributeValue("href", ""));
            }

            return links;
        }

        /// <summary>
        /// Extracts the movie title from its IMDb page and returns it.
        /// </summary>
        /// <param name="link">The link to the movie's IMDb page.</param>
        /// <returns>The title of the movie.</returns>
        public static string ExtractMovieTitle(string link)
        {
            OpenNewDocument(link);
            
            return HtmlEntity.DeEntitize(currentDocument.DocumentNode.Descendants("h1").First().InnerText);
        }

        /// <summary>
        /// Extracts and returns the raw release date of a movie as a string.
        /// </summary>
        /// <param name="link">The link to the movie's IMDb page</param>
        /// <returns>The movie's release date.</returns>
        public static string ExtractReleaseDateString(string link)
        {
            OpenNewDocument(link);
            
            string releaseListLink = MainClass.IMDB_URL + currentDocument.DocumentNode.Descendants("a").Where((n) => n.GetAttributeValue("title", "") == "See more release dates").First().GetAttributeValue("href", "");

            OpenNewDocument(releaseListLink);

            IEnumerable<HtmlNode> releaseDateNodes = currentDocument.DocumentNode.Descendants("td").Where((n) => n.HasClass("release-date-item__date"));

            string ret = releaseDateNodes.First().InnerText.Trim();
            int skipCount = 1;

            while(ret.Split(' ').Length != 3)
            {
                ret = releaseDateNodes.Skip(skipCount++).First().InnerText.Trim();
            }

            return ret;
        }
    }
}