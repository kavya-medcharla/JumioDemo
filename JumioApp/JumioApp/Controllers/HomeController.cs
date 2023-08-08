using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JumioApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace JumioApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var accountRequest = new AccountRequest
        {
            CustomerInternalReference = "Testing",
            UserReference = "Postman API Tests",
            TokenLifetime = "5m",
            WorkflowDefinition = new WorkflowDefinition { Key = 10011 },
            WebDetails = new WebDetails
            {
                SuccessUrl = "https://www.jumio.com",
                ErrorUrl = "https://jumio.com/error"
            }
        };

        var client = _httpClientFactory.CreateClient("jumio");
        var requestData = new StringContent(JsonConvert.SerializeObject(accountRequest), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("accounts",requestData);
        var result = await response.Content.ReadAsStringAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

