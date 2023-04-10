using CryptoApp.Controllers;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoApp.Models

{
	

    class CryptoCurrencyHistoryModel
	{
		public static async Task<CryptoCurrencyHistoryModel> Load(string id) {
			var res = new CryptoCurrencyHistoryModel();
			Dictionary<string, string> parms = new Dictionary<string, string> {
				{ "id", id},
				{"interval", Cache.Cashe.chartRefrashRate.ToString() }
			};
			var apiResponse = await ApiHandler.QueryAsync($"/assets/{id}/history", parms);
			var respString = await apiResponse.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<CryptoCurrencyHistoryModel>(respString)?? new CryptoCurrencyHistoryModel();
		}


		[JsonPropertyName("data")]
		public List<PriceEntry> Data { get; set; }

		[JsonPropertyName("timestamp")]
		public long Timestamp { get; set; }


	}
	public class PriceEntry
	{
		[JsonPropertyName("priceUsd")]
		public string PriceUsd { get; set; }

		[JsonPropertyName("time")]
		public long Time { get; set; }
	}
}
