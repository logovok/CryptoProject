using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CryptoApp.Cache;
using CryptoApp.Models;

namespace CryptoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
			
		}

		

		

		

		private void lst1_Initialized(object sender, EventArgs e)
		{

			
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			LbNmb_Initialized(sender, e);
		}

		private async void Button_Click_1(object sender, RoutedEventArgs e)
		{
			int nmbr = int.Parse(LbNmb.Content.ToString());
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
			int nmbr = int.Parse(LbNmb.Content.ToString());
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
			int requestedCount = int.Parse(LbNmb.Content.ToString());
			var popIds = await Cashe.TryFromCache(requestedCount);

			//var popIds = Controllers.CryptoCurrencyController
			//	.LoadCurrency(int.Parse(LbNmb.Content.ToString())).Result;
			lst1.Items.Clear();
			int c = 0;
			foreach (var currency in popIds.data)
			{
				if (c++ == requestedCount )
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
	}
}
