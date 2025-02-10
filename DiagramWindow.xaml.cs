using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для DiagramWindow.xaml
    /// </summary>
    public partial class DiagramWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        
        public DiagramWindow()
        {
            InitializeComponent();
            TitelDiag.Content = "Соотношение кол-ва товаров между собой";
            var context = new Context();
            context.Goods.Load();
            var goods = context.Goods.Local.AsEnumerable();
            SeriesCollection = new SeriesCollection();
            foreach (var good in goods) {
                SeriesCollection.Add(
                    new PieSeries { Title = good.Name, Values = new ChartValues<decimal> {good.Total}, DataLabels = true }
                    );
            }
            DataContext = this;    
        }
        public DiagramWindow(string t) {
            InitializeComponent();
            TitelDiag.Content = "Соотношение кол-ва товаров поступаемых от поставщиков";
            var context = new Context();
            context.Goods.Load();
            var goods = context.Goods.Local.AsEnumerable();
            SeriesCollection = new SeriesCollection();
            context.Producers.Load();
            var producers = context.Producers.Local.AsEnumerable();
            foreach (var prod in producers)
            {
                int tempCounter = 0;
                foreach (var good in goods) {
                    if (prod.ProducerId == good.ProducerId) {
                        tempCounter++;
                    }
                }
                SeriesCollection.Add(
                    new PieSeries { Title = prod.Name, Values = new ChartValues<decimal> {tempCounter}, DataLabels = true }
                    );
            }
            DataContext = this;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
