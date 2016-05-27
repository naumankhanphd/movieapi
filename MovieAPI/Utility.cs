using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MovieAPI
{
    public class Utility
    {

        public const string RESPONSE_MOVIE_NOT_FOUND = "The movie is not found";
        public const string RESPONSE_INTERNAL_SERVER_ERROR = "The server had some internal error processing the request";
        public const string RESPONSE_TITLE_NOT_SPECIFIED = "Title is not supplied";
        public const string RESPONSE_MOVIE_ALREADY_EXISTS = "The movie has already been added to the list";
        public const string RESPONSE_MOVIE_NOT_RETRIEVED = "The movie information could not be retrieved";
        public const string RESPONSE_MOVIE_NOTEXISTS_IN_DATABASE = "No Movies matched the criteria";
        public const string RESPONSE_MOVIE_SAVED = "Movie saved successfully";

        public static int GetMovieCount()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:26162/Movies/Index");

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JArray movieList = JArray.Parse(responseString);
            return movieList.Count;
        }

        public static string AddMovie(string title, int year)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:26162/Movies/Index");

            var postData = string.Format("title={0}&year={1}", title, year);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

    }
}