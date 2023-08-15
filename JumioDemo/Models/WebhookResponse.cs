using System;
namespace JumioDemo.Models
{
    public class WebHookWorkflowExecution
    {
        public string id { get; set; }
        public string href { get; set; }
        public string definitionKey { get; set; }
        public string status { get; set; }
    }

    public class WebhookAccount
    {
        public string id { get; set; }
        public string href { get; set; }
    }

    public class Root
    {
        public DateTime callbackSentAt { get; set; }
        public string userReference { get; set; }
        public WebHookWorkflowExecution workflowExecution { get; set; }
        public WebhookAccount account { get; set; }
    }

}

