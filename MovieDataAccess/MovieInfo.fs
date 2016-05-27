namespace MovieDataAccess
open System
open System.Net
open System.IO

type GlobalFunctions =
    class
    static member FetchExternalID  (title:string, year:int) =
        let url = "https://api.themoviedb.org/3/search/movie?query=" + title + "&year=" + year.ToString() + "&api_key=73e47d150a7027478573a332d6210251"
        let req = WebRequest.Create(Uri(url)) 
       
        use resp = req.GetResponse() 
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        reader.ReadToEnd()
    end

[<CLIMutable>]
type MovieInfo = { ID:Guid; Title:string; Year:int; ExternalID:string }
