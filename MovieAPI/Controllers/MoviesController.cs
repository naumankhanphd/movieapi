using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieDataAccess;
using Newtonsoft.Json.Linq;

namespace MovieAPI.Controllers
{
    public class MoviesController : Controller
    {
        //
        // GET: /Movies/
        private static List<MovieInfo> library = new List<MovieInfo>();

        public JsonResult Index(Guid? id)
        {
            if (id == null)
                return Json(library, JsonRequestBehavior.AllowGet);
            else
            {
                MovieInfo movie = library.Where(l => l.ID == id).FirstOrDefault();
                if (movie != null)
                {
                    return Json(movie, JsonRequestBehavior.AllowGet);
                }
                return Json(Utility.RESPONSE_MOVIE_NOT_FOUND) ;
            }
        }

        [HttpPost]
        public string Index(string title, int year)
        {
            string response = string.Empty;
            if (string.IsNullOrEmpty(title))
                return Utility.RESPONSE_TITLE_NOT_SPECIFIED; 

            MovieInfo newMovie = new MovieInfo { ID = Guid.NewGuid(), Title = title, Year = year };
            try
            {
                string jsonText = GlobalFunctions.FetchExternalID(title, year);
                JObject json = JObject.Parse(jsonText);
                if (json != null && json["results"].Count() > 0)
                {

                    JToken movieObject = json["results"][0];
                    if (movieObject != null)
                    {
                        newMovie.ExternalID = movieObject["id"].ToString();
                        var existingMovie = library.Where(l => l.ExternalID == newMovie.ExternalID).FirstOrDefault();
                        if (existingMovie !=  null)
                        {
                            response = Utility.RESPONSE_MOVIE_ALREADY_EXISTS;
                        }
                        else
                        {
                            library.Add(newMovie);
                            response = Utility.RESPONSE_MOVIE_SAVED;
                        }
                    }
                    else
                    {
                        response = Utility.RESPONSE_MOVIE_NOTEXISTS_IN_DATABASE;
                    }
                }
                else
                {
                    response = Utility.RESPONSE_MOVIE_NOTEXISTS_IN_DATABASE;
                }
                
            }
            catch
            {
                response = Utility.RESPONSE_INTERNAL_SERVER_ERROR;
            }
            return response;
        }
    }
}
