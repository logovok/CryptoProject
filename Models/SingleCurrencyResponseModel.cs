using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
	internal class SingleCurrencyResponseModel
	{
		public CryptoCurrency data { get; set; }
		public long timestamp { get; set; }
	}
}
