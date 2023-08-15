using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JumioDemo.Models;
using System;
using IdentityModel;

namespace JumioDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class JumioScanController : ControllerBase
{
    private readonly ILogger<JumioScanController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private string accountID = string.Empty;
    private string wrkflowExecutionID = string.Empty;

    public JumioScanController(ILogger<JumioScanController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost(Name ="InitiateRequest")]
    public async Task<IActionResult> Index()
    {
        Console.WriteLine($"{DateTime.Now} started the request..");
        var accountRequest = new AccountRequest
        {
            customerInternalReference = "Testing",
            userReference = "Postman API Tests",
            tokenLifetime = "5m",
            callbackUrl = "https://7157-67-188-12-15.ngrok-free.app/WebHookReceiver",
            workflowDefinition = new WorkflowDefinition { key = 10011 },
            web = new WebDetails
            {
                successUrl = "https://www.jumio.com",
                errorUrl = "https://jumio.com/error"
            }
        };

        var client = _httpClientFactory.CreateClient("jumio");
        var requestData = new StringContent(JsonConvert.SerializeObject(accountRequest), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("accounts", requestData);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var accountResponse = JsonConvert.DeserializeObject<AccountResponse>(responseContent);
            accountID = accountResponse.Account.Id;
            wrkflowExecutionID = accountResponse.WorkflowExecution.Id;
            return Ok(accountResponse);
        }
        else
        {
            // Handle the error case
            return null;
        }
    }
   
}

