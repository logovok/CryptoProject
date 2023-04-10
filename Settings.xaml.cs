using CryptoApp.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CryptoApp
{
	public partial class Settings : Window
	{
		public Settings()
		{
			InitializeComponent();
		}

		private void chrt_tp_Initialized(object sender, EventArgs e)
		{
			chrt_tp.ItemsSource = Enum.GetValues(typeof(ChartRefrashRate)).Cast<ChartRefrashRate>().ToList();
			chrt_tp.SelectedValue = Cashe.chartRefrashRate;
        }

		private void chrt_tp_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Cashe.chartRefrashRate = ((ChartRefrashRate)chrt_tp.SelectedIndex);
		}

		private void chrt_tp_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Cashe.refrashRateSeconds = (int) chrt_tp_Copy.SelectedValue;
		}

		private void chrt_tp_Copy_Initialized(object sender, EventArgs e)
		{
			chrt_tp_Copy.ItemsSource = new List<int> { 
			15, 30, 60
			};
			chrt_tp_Copy.SelectedValue = Cashe.refrashRateSeconds;
		}
	}
}
