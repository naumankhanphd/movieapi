using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;
using System.IO; 

namespace MovieAPI.Tests
{
    [TestClass]
    public class MovieAPITests
    {
        [TestMethod]
        public void AddMovie()
        {
            string title = "Deadpool";
            int year = 2016;
            Assert.IsTrue(Utility.RESPONSE_MOVIE_SAVED == Utility.AddMovie(title, year));

        }

        [TestMethod]
        public void AddDuplicateMovie()
        {
            string title = "Deadpool";
            int year = 2016;
            Assert.IsTrue(Utility.RESPONSE_MOVIE_SAVED == Utility.AddMovie(title, year));
            Assert.IsTrue(Utility.RESPONSE_MOVIE_ALREADY_EXISTS == Utility.AddMovie(title, year));
        }

        [TestMethod]
        public void MovieCount()
        {
            Utility.AddMovie("Deadpool", 2016);
            Utility.AddMovie("Captain America: Civil War", 2016);
            Assert.IsTrue(2 == Utility.GetMovieCount());
        }
    }
}
