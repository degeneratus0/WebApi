using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebApiTests
{
    public static class TestingUtilities
    {
        public static void IsResponseStatus(HttpStatusCode expected, HttpStatusCode actual)
        {
            Assert.AreEqual(expected, actual, $"Response status code was not {expected}, but '{actual}'");
        }

        public static StringContent CreateDefaultStringContentSerializeObject(object? value)
        {
            return new StringContent(
                JsonSerializer.Serialize(value),
                Encoding.UTF8,
                "application/json"
                );
        }

        public static StringContent CreateDefaultStringContent(string value)
        {
            return new StringContent(
                value,
                Encoding.UTF8,
                "application/json"
                );
        }
    }
}
