using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JumioDemo.Models;
using System;
using IdentityModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JumioDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AxleController : ControllerBase
    {
        private readonly ILogger<AxleController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private TokenExchangeResponse AxleAccountResponse = new TokenExchangeResponse() { Data = new TokenExchangeData() };
        public AxleController(ILogger<AxleController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("/Create")]
        public async Task<string> Index()
        {
            var axleClient = _httpClientFactory.CreateClient("axle");
            var axleIgnition = new AxleIgnitionRequest();
            var requestData = new StringContent(JsonConvert.SerializeObject(axleIgnition),Encoding.UTF8, "application/json");
            HttpResponseMessage response = await axleClient.PostAsync("ignition",requestData);
            if (response.IsSuccessStatusCode)
            {
                var respContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(respContent);
                return respContent;
            }
            return "InvalidStatusCode";
        }

        [HttpPost("/Recieve")]
        public async Task<IActionResult> RecieveWebhook()
        {
            Console.WriteLine($"{DateTime.Now} recieved the webhook..");
            try
            {
                string requestBody;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                var webhookResponse = JsonConvert.DeserializeObject<AxleEvent>(requestBody);
                Console.WriteLine(requestBody);
                Console.WriteLine("Recieved ignition status Data");
                return Ok("Axle status webhook received and processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("/RecieveRedirect")]
        public async Task<IActionResult> RecieveRedirectWebhook([FromQuery] string status, [FromQuery] string authCode, [FromQuery] string result)
        {
            Console.WriteLine($"{DateTime.Now} recieved the webhook..");
            try
            {
                //GetAccountInfo(authCode);

                //Retrieve AccountId and AccessToken.
                using (var axleClient = _httpClientFactory.CreateClient("axle"))
                {
                    var tokenReq = new AxleTokenRequest() { authCode = authCode };
                    //var authContent = new { AuthCode = authCode };
                    var content = new StringContent(JsonConvert.SerializeObject(tokenReq), Encoding.UTF8, "application/json");
                    //axleClient.DefaultRequestHeaders.Add("x-client-id", "cli_Ke3MCrIN24to552VVUoyT");
                    //axleClient.DefaultRequestHeaders.Add("x-client-secret", "Wa0Znr2i2YC0x6BBsnpch");
                    HttpResponseMessage response = await axleClient.PostAsync("/token/exchange", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var respContent = await response.Content.ReadAsStringAsync();
                        AxleAccountResponse = JsonConvert.DeserializeObject<TokenExchangeResponse>(respContent);
                    }
                    if (!string.IsNullOrEmpty(AxleAccountResponse.Data.Account) && !string.IsNullOrEmpty(AxleAccountResponse.Data.AccessToken))
                    {
                        axleClient.DefaultRequestHeaders.Add("x-access-token", AxleAccountResponse.Data.AccessToken);
                        HttpResponseMessage resp = await axleClient.GetAsync($"accounts/{AxleAccountResponse.Data.Account}?expand=true");
                        if (resp.IsSuccessStatusCode)
                        {
                            string responseBody = await resp.Content.ReadAsStringAsync();
                            Console.WriteLine("Insurance Data");
                            Console.WriteLine(responseBody);
                            return Ok(responseBody);
                        }
                    }
                    return Ok("Invalid Status");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok(ex.Message);
            }
        }

        private async Task GetAccountInfo(string token)
        {
            using (var axleClient = _httpClientFactory.CreateClient("axle"))
            {
                var tokenReq = new AxleTokenRequest() { authCode = token };
                //var authContent = new { AuthCode = authCode };
                var content = new StringContent(JsonConvert.SerializeObject(tokenReq), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await axleClient.PostAsync("/token/exchange", content);
                if (response.IsSuccessStatusCode)
                {
                    var respContent = await response.Content.ReadAsStringAsync();
                    AxleAccountResponse = JsonConvert.DeserializeObject<TokenExchangeResponse>(respContent);
                }
            }
        }
    }
}

