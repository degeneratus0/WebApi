using NUnit.Framework;
using System.Net;

namespace WebApiTests
{
    public static class TestingUtilities
    {
        public static void IsResponseStatus(HttpStatusCode expected, HttpStatusCode actual)
        {
            Assert.AreEqual(expected, actual, $"Response status code was not {expected}, but '{actual}'");
        }
    }
}
