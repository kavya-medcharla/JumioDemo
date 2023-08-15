using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JumioDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JumioDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookReceiverController : ControllerBase
    {
        private readonly ILogger<WebHookReceiverController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WebHookReceiverController(ILogger<WebHookReceiverController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        // GET: /<controller>/
        [HttpPost(Name = "RecieveWebhook")]
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

                var webhookResponse = JsonConvert.DeserializeObject<Root>(requestBody);
                string parsedData = await GetWorkflowDetails(webhookResponse.account.id, webhookResponse.workflowExecution.id);
                Console.WriteLine(parsedData);
                Console.WriteLine($"{DateTime.Now} entire cycle complete..");
                return Ok("Jumio webhook received and processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private void ProcessJumioAccountWebhook(string payload)
        {
            // Parse and handle the incoming webhook payload from Jumio's accounts endpoint
            // Implement your logic here to handle different webhook events
            // You might deserialize the payload and perform actions based on the data
            Console.WriteLine($"Received Jumio webhook payload: {payload}");
        }

        private async Task<string> GetWorkflowDetails(string accountId, string workflowExecutionId)
        {
            Console.WriteLine($"{DateTime.Now} started the workflow details..");
            using (var client = _httpClientFactory.CreateClient("jumioRetrieval"))
            {
                string relativePath = $"accounts/{accountId}/workflow-executions/{workflowExecutionId}";
                HttpResponseMessage response = await client.GetAsync(relativePath);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JumioDemo.Models.Retrieval.Root>(responseBody);
                    Console.Write(data);
                    return responseBody;
                }
                return "Error";
            }

        }
    }
}

