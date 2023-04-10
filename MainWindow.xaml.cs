using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CryptoApp.Cache;
using CryptoApp.Controllers;
using CryptoApp.Models;

namespace CryptoApp
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DispatcherTimer dt = new DispatcherTimer();
			dt.Interval = TimeSpan.FromSeconds(Cache.Cashe.refrashRateSeconds);
			dt.Tick += LbNmb_Initialized;
			dt.Start();

		}

		

		

		

		private void lst1_Initialized(object sender, EventArgs e)
		{

			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			LbNmb_Initialized(sender, e);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			int nmbr = int.Parse((LbNmb.Content.ToString()) ?? "10");
			if (nmbr-10 >= 10)
			{
				LbNmb.Content = (nmbr - 10);
			}
			else
			{
				LbNmb.Content = 10;
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			int nmbr = int.Parse((LbNmb.Content.ToString()) ?? "10");
			if (nmbr + 10 <= 100)
			{
				LbNmb.Content = (nmbr + 10);
			}
			else
			{
				LbNmb.Content = 100;
			}
		}

		private async void LbNmb_Initialized(object sender, EventArgs e)
		{
			int requestedCount = int.Parse((LbNmb.Content.ToString())??"10");
			var popIds = await Cashe.TryFromCache(requestedCount);
			lst1.Items.Clear();
			int c = 0;
			foreach (var currency in popIds.data)
			{
				if (c++ == requestedCount)
				{
					break;
				}
				StackPanel panel = new StackPanel();

				TextBlock nameBlock = new TextBlock();
				nameBlock.Text = currency.name;
				nameBlock.FontSize = 16;
				nameBlock.FontWeight = FontWeights.Bold;
				panel.Children.Add(nameBlock);

				TextBlock priceBlock = new TextBlock();
				priceBlock.Text = "Price: $" + currency.priceUsd.ToString();
				priceBlock.FontSize = 14;
				panel.Children.Add(priceBlock);

				TextBlock changeBlock = new TextBlock();
				changeBlock.Text = "Change: " + currency.changePercent24Hr.ToString() + "%";
				changeBlock.FontSize = 14;

				if (double.Parse(currency.changePercent24Hr.Replace(".", ",")) > 0)
				{
					changeBlock.Foreground = Brushes.Green;
				}
				else if (double.Parse(currency.changePercent24Hr.Replace(".", ",")) < 0)
				{
					changeBlock.Foreground = Brushes.Red;
				}

				panel.Children.Add(changeBlock);
				panel.MouseLeftButtonUp += (e, d) =>
				{
					Window1 w1 = new(currency);
					w1.Show();
					this.Close();
				};

				lst1.Items.Add(panel);
			}
			
		}



		private async void Button_Click_3(object sender, RoutedEventArgs e)
		{
			string searchRequest = search_bar.Text;
			if (searchRequest.Equals("Currency name or code"))
			{
                MessageBox.Show("Enter name or code of crypto currency");
				return;
			}
		 	var currency = await CryptoCurrencyController.Search(searchRequest);
			if (currency != null) {
				try
				{
					Window1 w1 = new(currency.data[0]);
					w1.Show();
					Close();
				}
				catch (Exception)
				{

                    MessageBox.Show("Not found");
				}
				
			}
		}

		private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Settings sts = new Settings();
			sts.Show();
		
		}
	}
}
