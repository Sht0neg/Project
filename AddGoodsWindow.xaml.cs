using Microsoft.VisualBasic;
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
    /// Логика взаимодействия для AddGoodsWindow.xaml
    /// </summary>
    public partial class AddGoodsWindow : Window
    {
        MainWindow? parent;
        public Goods? CurrentGood;
        List<Producer> producers;

        public AddGoodsWindow(MainWindow? parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = "Добавление товара";
            AddButton.Content = "Добавить";
        }

        public AddGoodsWindow(Goods good, MainWindow? parent)
        {
            InitializeComponent();
            this.CurrentGood = good;
            this.parent = parent;

            this.Title = "Изменение товара";
            AddButton.Content = "Изменить";

            DataContext = good;

        }

        public bool CheckGood(string name, string international, string rf, string price, string total) {
            if (name == "" || double.TryParse(name, out double numericValue)) { MessageBox.Show("Неправильно введено название!"); return false; };
            if (double.TryParse(international, out double numericValue1)) { MessageBox.Show("Неправильно введено международное название!"); return false; };
            if (rf == "" || !double.TryParse(rf, out double numericV)) { MessageBox.Show("Неправильно введен регистрационный номер!"); return false; };
            if (!double.TryParse(price, out double numer)) { MessageBox.Show("Неправильно введена цена товара!"); return false; }
            if (!int.TryParse(total, out int numeric)) { MessageBox.Show("Неправильно введено количество товара!"); return false; }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Producer current = new Producer();
            foreach (Producer p in producers)
            {
                if (p.Name == ProducerBox.SelectedItem)
                {
                    current = p;
                }
            }
            if (AddButton.Content == "Изменить")
            {
                PriceBox.Text = PriceBox.Text.Replace(".", ",");
                if (CheckGood(NameBox.Text, InterNameBox.Text, NumberBox.Text, PriceBox.Text, CountBox.Text))
                {

                    DialogResult = true;
                }
                else DialogResult = false;
            }
            else
            {
                if (CheckGood(NameBox.Text, InterNameBox.Text, NumberBox.Text, PriceBox.Text, CountBox.Text))
                {
                    DateOnly begin = DateOnly.FromDateTime(DateTime.Today);
                    DateOnly end = DateOnly.FromDateTime(DateTime.Today);
                    string inter = NameBox.Text;
                    int count = 0;
                    if (DataBeginBox.SelectedDate != null) begin = DateOnly.FromDateTime((DateTime)DataBeginBox.SelectedDate);
                    if (DataEndBox.SelectedDate != null) end = DateOnly.FromDateTime((DateTime)DataEndBox.SelectedDate);
                    if (InterNameBox.Text != "") inter = InterNameBox.Text;
                    if ((bool)YesBox.IsChecked) count = Convert.ToInt32(CountBox.Text);
                    parent.addGood(
                        NameBox.Text,
                        inter,
                        begin,
                        end,
                        YesBox.IsChecked,
                        NumberBox.Text,
                        current,
                        PackBox.Text,
                        Convert.ToDouble(PriceBox.Text),
                        count
                    );
                    Close();
                    MessageBox.Show("Товар успешно добавлен!");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            producers = parent.ProducerList();
            foreach (Producer producer in producers)
            {
                ProducerBox.Items.Add(producer.Name);
            }
            List<string> batches = new List<string>() { "Ампулы", "Саше", "Флакон", "Блистер", "Туба" };
            foreach (string batch in batches) {
                PackBox.Items.Add(batch);
            }
        }

        public void reProducer() {
            
            foreach (Producer g in producers)
            {
                if (g.Name == CurrentGood.ProducerName)
                {

                    CurrentGood.Producer = g;
                }
            }
            if (CurrentGood.Intenational == "") { CurrentGood.Intenational = CurrentGood.Name; }
            if (!(bool)CurrentGood.Availability)
            {
                CurrentGood.Total = 0;
            }

            parent.context.Update(CurrentGood);
            parent.context.SaveChanges();
        }
    }
}
