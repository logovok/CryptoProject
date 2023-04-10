using CryptoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp.Controllers
{
	internal class CryptoCurrencyController
	{
		public static async Task<ResponseData?> LoadCurrency(int limit) {
			HttpClient client = new HttpClient();
			var queryParams = new Dictionary<string, string>
			{
				{ "limit" , limit.ToString()}
			};
			var response = await ApiHandler.QueryAsync("/assets", queryParams);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			ResponseData currencies = new ResponseData();
			try
			{
				currencies = JsonSerializer.Deserialize<ResponseData>(jsonResponse);
				
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				MessageBox.Show(e.StackTrace);
			}
			return currencies;
		}

		public static async Task<CryptoCurrency> GetCurrencyById(string id) {
			string url = $"/assets/{id}";
			var response = await ApiHandler.QueryAsync(url);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			SingleCurrencyResponseModel responseData = new SingleCurrencyResponseModel();
			try
			{
				responseData = JsonSerializer.Deserialize<SingleCurrencyResponseModel>(jsonResponse);

			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				MessageBox.Show(e.StackTrace);
			}
			return responseData.data;
		}

		public static async Task<ResponseData> Search(string userInput) {
			string url = "/assets";
			Dictionary<string, string> parms = new Dictionary<string, string> {
				{ "search", userInput},
				{ "limit", "1"}
			};
			using var response = await ApiHandler.QueryAsync(url, parms);
			response.EnsureSuccessStatusCode();
			var respString = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<ResponseData>(respString);

		}

	}

	internal class ResponseData
	{
		public List<CryptoCurrency> data { get; set; }
		public long timestamp { get; set; }
	}
}
