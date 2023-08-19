using System;
namespace JumioDemo.Models
{
	public class AxleIgnitionRequest
	{
		public string webhookUri = @"https://7157-67-188-12-15.ngrok-free.app/Recieve";
		public string redirectUri = @"https://localhost:7250/RecieveRedirect";
    }
	public class AxleTokenRequest
	{
		public string authCode { get; set; }
	}
}

