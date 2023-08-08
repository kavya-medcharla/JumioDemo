using System;
namespace JumioApp.Models
{
    public class AccountRequest
    {
        public string CustomerInternalReference { get; set; }
        public string UserReference { get; set; }
        public string TokenLifetime { get; set; }
        public WorkflowDefinition WorkflowDefinition { get; set; }
        public WebDetails WebDetails { get; set; }
    }

    public class WorkflowDefinition
    {
        public int Key { get; set; }
    }

    public class WebDetails
    {
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
    }

}

