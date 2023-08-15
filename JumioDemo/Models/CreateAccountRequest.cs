using System;
namespace JumioDemo.Models
{
    public class AccountRequest
    {
        public string customerInternalReference { get; set; }
        public string userReference { get; set; }
        public string tokenLifetime { get; set; }
        public string callbackUrl { get; set; }
        public WorkflowDefinition workflowDefinition { get; set; }
        public WebDetails web { get; set; }
    }

    public class WorkflowDefinition
    {
        public int key { get; set; }
    }

    public class WebDetails
    {
        public string successUrl { get; set; }
        public string errorUrl { get; set; }
    }

}

