using CryptoApp.Controllers;
using CryptoApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Cache
{
	internal class Cashe
	{
		public static int refrashRateSeconds = 15;
		public static Hashtable cashedInfo = new Hashtable();
		public static async Task<CryptoCurrency> TryFromCache(string id) {
			if (cashedInfo.ContainsKey(id))
			{
				if (DateTime.Now.Second - ((CryptoCurrency)cashedInfo[id]).updateTime < refrashRateSeconds)
				{
					return (CryptoCurrency)cashedInfo[id];
				}
				
			}
			return await Controllers.CryptoCurrencyController.GetCurrencyById(id);
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

		public static async Task ParseCryptos(SingleCurrencyResponseModel scrm) {
			await Task.Run(() => {
				if (cashedInfo.ContainsKey(scrm.data.id))
				{
					cashedInfo[scrm.data.id] = scrm.data;
				}
				else
				{
					cashedInfo.Add(scrm.data.id, scrm.data);
				}
			});
			
		}
	}
}
