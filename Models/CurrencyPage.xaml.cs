using LiveCharts.Defaults;
using LiveCharts;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using LiveCharts.Wpf;
using System.Windows.Threading;

namespace CryptoApp.Models
{
    public partial class Window1 : Window
    {


        CryptoCurrency? cc;
        public Window1(CryptoCurrency cryptoCurrency)
        {
            InitializeComponent();
            cc = cryptoCurrency;

			DispatcherTimer dt = new DispatcherTimer();
			dt.Interval = TimeSpan.FromSeconds(Cache.Cashe.refrashRateSeconds);
			dt.Tick += (s,e) => {
                fillWithData();
            };
			dt.Start();

			fillWithData();
            fillChartWithData();
        }

        public async void fillWithData()
        {
            cc = (await Cache.Cashe.TryFromCache(cc.id));
            label.Content = cc.name;
            label_Copy.Content = $"#{cc.rank} by popularity";
            price.Content = $"Price: ${cc.priceUsd.Substring(0, 12)}";
            change.Foreground = double.Parse(cc.changePercent24Hr.Replace(".", ",")) >= 0 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            change.Content = $"Change: {cc.changePercent24Hr.Substring(0, 12)}%";
            supply.Content = $"Supply {cc.supply.Substring(0, 20)}";
            vol_24h.Content = $"Volume 24H: {cc.volumeUsd24Hr.Substring(0, 20)}";
            mrk_capacity.Content = $"Maket capacity: {cc.marketCapUsd.Substring(0, 20)}";
			
		}

        public async void fillChartWithData() {
			var chartValues = new ChartValues<ObservablePoint>();


			CryptoCurrencyHistoryModel chm = await CryptoCurrencyHistoryModel.Load(cc.id);
			int refreshPoints = 0;
			chm.Data.Reverse();

			foreach (var item in chm.Data)
			{
				chartValues.Add(new ObservablePoint(refreshPoints--, double.Parse(item.PriceUsd.Replace(".", ","))));


			}
			chart.Series = new SeriesCollection
			{
				new LineSeries
				{
					Title = "Price",
					Values = chartValues,
					PointGeometrySize =1,
					StrokeThickness = 1
				}
			};
		}



		private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string url = cc.explorer;
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
            e.Handled = true;
        }
    }
}
