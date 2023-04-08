using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
	internal class CryptoCurrency
	{
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string supply { get; set; }
        public string marketCapUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string priceUsd { get; set; }
        public string changePercent24Hr { get; set; }
        public string explorer { get; set; }

        [NonSerialized]
        public long updateTime;

        public CryptoCurrency() {
            updateTime = DateTime.Now.Second;
        }
		public override string ToString()
		{
			return id + " " + rank + " " + symbol + " " + name + " " + supply;
		}
	}
}
