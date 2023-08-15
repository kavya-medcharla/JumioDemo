using System;
namespace JumioDemo.Models
{
    public class AccountResponse
    {
        public DateTime Timestamp { get; set; }
        public Account Account { get; set; }
        public Web Web { get; set; }
        public Sdk Sdk { get; set; }
        public WorkflowExecution WorkflowExecution { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
    }

    public class Web
    {
        public string Href { get; set; }
        public string SuccessUrl { get; set; }
        public string ErrorUrl { get; set; }
    }

    public class Sdk
    {
        public string Token { get; set; }
    }

    public class WorkflowExecution
    {
        public string Id { get; set; }
        public List<Credential> Credentials { get; set; }
    }

    public class Credential
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public List<string> AllowedChannels { get; set; }
        public ApiDetails Api { get; set; }
    }

    public class ApiDetails
    {
        public string Token { get; set; }
        public Parts Parts { get; set; }
        public string WorkflowExecution { get; set; }
    }

    public class Parts
    {
        public string Front { get; set; }
        public string Back { get; set; }
        public string Face { get; set; }
    }

}

