using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Models
{
	internal class CandlesModel
	{
		public List<CandleModel> data { get; set; }
		public long timestamp { get; set; }
	}

	internal class CandleModel
	{
		//"open": "0.0708000000000000",
		//     "high": "0.0710450000000000",
		//     "low": "0.0706434000000000",
		//     "close": "0.0708350100000000",
		//     "volume": "1818.1433015300000000",
		//     "period": 1530720000000
		//[NonSerialized]
		private double Open;
		public double open
		{
			get { return Open; }
			set { Open = value; }
		}

		//[NonSerialized]
		private double High;
		public double high
		{
			get { return High; }
			set { High = value; }
		}

		//[NonSerialized]
		private double Low;
		public double low
		{
			get { return Low; }
			set { Low = value; }
		}

		//[NonSerialized]
		private double Close;
		public double close
		{
			get { return Close; }
			set { Close = value; }
		}

		//[NonSerialized]
		private double Volume;
		public double volume
		{
			get { return Volume; }
			set { Volume = value; }
		}


		public long period { get; set; }
	}
}
