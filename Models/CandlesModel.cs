using CryptoApp.Controllers;
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
		//public static void test() {
		//	Dictionary<string, string> prams = new Dictionary<string, string> {
		//		{ , }
		//	};
		//ApiHandler.QueryAsync("/candles")
		//}
	
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
