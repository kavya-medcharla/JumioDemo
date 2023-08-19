using System;
namespace JumioDemo.Models
{
    using System;
    using System.Collections.Generic;

    public class EventData
    {
        public string Token { get; set; }
        public Config Config { get; set; }
    }

    public class Config
    {
        public Exit Exit { get; set; }
        public Documentation Documentation { get; set; }
        public Basic Basic { get; set; }
        public Manual Manual { get; set; }
    }

    public class Exit
    {
        public string Message { get; set; }
    }

    public class Documentation
    {
        public List<string> AcceptedDocs { get; set; }
        public List<string> AcceptedTypes { get; set; }
    }

    public class Basic
    {
        public bool Documents { get; set; }
        public bool Enabled { get; set; }
    }

    public class Manual
    {
        public bool Enabled { get; set; }
    }

    public class AxleEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public EventData Data { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TokenExchangeResponse
    {
        public bool Success { get; set; }
        public TokenExchangeData Data { get; set; }
    }

    public class TokenExchangeData
    {
        public string AccessToken { get; set; }
        public string Account { get; set; }
        public List<string> Policies { get; set; }
    }
}

