using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;

namespace WebApiTests
{
    public abstract class WebApiTestsBase
    {
        protected static HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            WebApplicationFactory<Program> webAppFactory = new WebApplicationFactory<Program>();
            httpClient = webAppFactory.CreateDefaultClient();
        }
    }
}
