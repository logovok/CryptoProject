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
		public static CryptoCurrency tryFromCache(string id) {
			if (cashedInfo.ContainsKey(id))
			{
				if (DateTime.Now.Second - ((CryptoCurrency)(cashedInfo[id])).updateTime < refrashRateSeconds)
				{
					return (CryptoCurrency)cashedInfo[id];
				}
				
			}
			return Controllers.CryptoCurrencyController.GetCurrencyById(id).Result;
		}
	}
}
