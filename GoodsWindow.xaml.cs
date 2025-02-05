using Microsoft.EntityFrameworkCore;
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

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для GoodsWindow.xaml
    /// </summary>
    public partial class GoodsWindow : Window
    {
        public Context? context;
        MainWindow? parent;
        public GoodsWindow(MainWindow? parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = "Информация о товарах";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new Context();

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Goods.Load();

            GoodsDataGridView.ItemsSource = context.Goods.Local.ToObservableCollection();
        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (GoodsDataGridView.SelectedItems.Count == 0) return;
            Goods? selgood = context.Goods.Find(((Goods)GoodsDataGridView.SelectedItem).Id);
            AddGoodsWindow form = new(selgood, parent);
            bool? result = form.ShowDialog();
            
            if (result == true)
            {
                form.reProducer();
                context.Goods.Update(selgood);
                context.SaveChanges();

            }

            context = new Context();
            context.Goods.Load();
            GoodsDataGridView.ItemsSource = context.Goods.Local.ToObservableCollection();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (GoodsDataGridView.SelectedItems.Count == 0) return;
            Goods selgood = (Goods)GoodsDataGridView.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Удалить товар с названием {selgood.Name}", "Подтверждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK) { context.Goods.Remove(selgood); context.SaveChanges(); }
        }
    }
}
