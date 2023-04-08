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
		public static async Task<ResponseData> LoadCurrency(int limit) {
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
			HttpClient client = new HttpClient();
			string url = $"/assets/{id}";
			var response = await ApiHandler.QueryAsync(url);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			//MessageBox.Show(jsonResponse);
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


	}

	internal class ResponseData
	{
		public List<CryptoCurrency> data { get; set; }
		public long timestamp { get; set; }
	}
}
