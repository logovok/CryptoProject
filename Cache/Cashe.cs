using CryptoApp.Controllers;
using CryptoApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoApp.Cache
{
	internal class Cashe
	{
		public static int refrashRateSeconds = 30;
		public static int candleRefashRateSeconds = 60;
		public static Hashtable cashedInfo = new Hashtable();
		private static DateTime? lastFullRefresh;
		public static async Task<CryptoCurrency> TryFromCache(string id) {
			if (cashedInfo.ContainsKey(id))
			{
				if ((DateTime.Now - ((CryptoCurrency)cashedInfo[id]).updateTime).TotalSeconds < refrashRateSeconds)
				{
					return (CryptoCurrency)cashedInfo[id];
				}
				
			}
			var res = await CryptoCurrencyController.GetCurrencyById(id);
			ParseCryptos(res);
			return res;
		}

		public static async Task<ResponseData> TryFromCache(int count)
		{
			SortedSet<CryptoCurrency> srs = new SortedSet<CryptoCurrency>();
			var cashedCount = cashedInfo.Count;
			if (!lastFullRefresh.HasValue || (DateTime.Now - lastFullRefresh).Value.TotalSeconds > refrashRateSeconds)
			{
				cashedCount = 0;
				lastFullRefresh = DateTime.Now;
				MessageBox.Show((DateTime.Now - lastFullRefresh).Value.TotalSeconds.ToString());
				MessageBox.Show("Full reload");
			}
			else
			{
				MessageBox.Show((DateTime.Now - lastFullRefresh).Value.TotalSeconds.ToString());
				await Task.Run(() => {
					int c = 0;
					foreach (var item in cashedInfo.Values)
					{
						srs.Add((CryptoCurrency)item);
					}
				});
			}
			
			if (count > cashedCount)
			{
				Dictionary<string, string> parms = new Dictionary<string, string> {
					{"limit", $"{count-cashedCount}"},
					{ "offset", $"{cashedCount}"}
				};
				var apiResponse = await ApiHandler.QueryAsync("/assets", parms);
				apiResponse.EnsureSuccessStatusCode();
				var responseText = await apiResponse.Content.ReadAsStringAsync();
				try
				{
					ResponseData rdForRest = JsonSerializer.Deserialize<ResponseData>(responseText);
					foreach (var item in rdForRest.data)
					{
						srs.Add(item);
					}
					await ParseCryptos(rdForRest);
				}
				catch (Exception)
				{
					//ignore
				}
				
			}
			
			
			ResponseData rd = new ResponseData();
			rd.data = srs.ToList();
			return rd;
		}



		public static async Task ParseCryptos(ResponseData rd) {
			await Task.Run(() => {
				foreach (var item in rd.data)
				{
					if (cashedInfo.ContainsKey(item.id))
					{
						cashedInfo[item.id] = item;
					}
					else
					{
						cashedInfo.Add(item.id, item);
					}
				}
			});
		}

		public static async Task ParseCryptos(CryptoCurrency scrm) {
			await Task.Run(() => {
				if (cashedInfo.ContainsKey(scrm.id))
				{
					cashedInfo[scrm.id] = scrm;
				}
				else
				{
					cashedInfo.Add(scrm.id, scrm);
				}
			});
			
		}
	}
}
