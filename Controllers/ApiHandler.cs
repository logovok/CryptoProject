using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoApp.Controllers
{
	
	internal class ApiHandler
	{

		public static HttpClient httpClient { get; set; } = new HttpClient();
		public static string endpointBase = "http://api.coincap.io/v2";
		

		public static async Task<HttpResponseMessage> 
			QueryAsync(string endpointEnding, Dictionary<string, string> qryParams)
		{
			var uriBuiledr = new UriBuilder(endpointBase + endpointEnding);
			uriBuiledr.Query = await (new FormUrlEncodedContent(qryParams)).ReadAsStringAsync();
			var response = httpClient.GetAsync(uriBuiledr.Uri).Result;
			response.EnsureSuccessStatusCode();
			return response;
		}

		public static async Task<HttpResponseMessage>
			QueryAsync(string endpointEnding)
		{
			
			var response =await httpClient.GetAsync(endpointBase + endpointEnding);
			response.EnsureSuccessStatusCode();
			return response;
		}


	}

	class CrypCurrency
	{
		string id { get; set; }

		public override string ToString()
		{
			return id;
		}
	}
}
