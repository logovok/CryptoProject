using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp.Controllers
{
	
	internal class ApiHandler
	{

		public static async void execute()
		{
			var queryParams = new Dictionary<string, string>
			{
				{ "limit" , "10"}
			};
			var res = await ApiHandler.QueryAsync("/assets" ,queryParams);
			var names = await res.Content.ReadAsStringAsync();//, "\"name\":\"(\\w+)\"").Groups[2].Captures;

			foreach (var item in ApiHandler.ExtractPatternGroup(names, "\"id\":\"(\\w+)\"").Result)
			{
				await Console.Out.WriteLineAsync(item);
			}
		}

		public static async Task<IEnumerable<string>> getPopularIds(int limit) {

			var queryParams = new Dictionary<string, string>
			{
				{ "limit" , limit.ToString()}
			};
			var queryResString = await QueryAsync("/assets", queryParams)
				.Result
				.Content
				.ReadAsStringAsync();

			return await ExtractPatternGroup(queryResString, "\"id\":\"(\\w+)\"");

		}

		public static async Task<IEnumerable<string>> ExtractPatternGroup(string text, string pattern)
		{

			List<string> res = new List<string>();
			Regex r = new Regex(pattern);
			var m = r.Match(text);
			await Task.Run(() =>
			{
				while (m.Success)
				{
					res.Add(m.Groups[1].ToString());
					m = m.NextMatch();
				}
			});

			return res;

		}

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
			
			var response = httpClient.GetAsync(endpointBase + endpointEnding).Result;
			MessageBox.Show(endpointBase+endpointEnding);
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
