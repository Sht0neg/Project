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
    /// Логика взаимодействия для InfProducerWinsow.xaml
    /// </summary>
    public partial class InfProducerWinsow : Window
    {
        public Context? context;
        MainWindow? parent;
        public InfProducerWinsow(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = "Информация о поставщиках";
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProducersDataGrid.SelectedItems.Count == 0) return;
            Producer? selectedProducer = ProducersDataGrid.SelectedItem as Producer;
            MessageBoxResult result = MessageBox.Show($"Удалить поставщика с названием {selectedProducer.Name}", "Подтверждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK) { context.Producers.Remove(selectedProducer); context.SaveChanges(); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context = new Context();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Producers.Load();

            ProducersDataGrid.ItemsSource = context.Producers.Local.ToObservableCollection();
        }

        private void ReButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProducersDataGrid.SelectedItems.Count == 0) return;
            Producer selProducer = (Producer)ProducersDataGrid.SelectedItem;
            AddProducerWindow form = new(selProducer);
            bool? result = form.ShowDialog();
            if (result == true) {
                context.Producers.Update(selProducer);
                context.SaveChanges();
            }

            context = new Context();
            context.Producers.Load();

            ProducersDataGrid.ItemsSource = context.Producers.Local.ToObservableCollection();
        }
    }
}
