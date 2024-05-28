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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для AddProducerWindow.xaml
    /// </summary>
    public partial class AddProducerWindow : Window
    {
        MainWindow? parent;
        public Producer producer;
        public AddProducerWindow(MainWindow? parent)
        {
            InitializeComponent();
            this.parent = parent;
            AddButton.Content = "Добавить";
            this.Title = "Добавление поставщика";

        }
        public AddProducerWindow(Producer producer)
        {
            InitializeComponent();
            this.producer = producer;
            DataContext = producer;
            AddButton.Content = "Изменить";
            this.Title = "Изменение поставщика";

        }

        public bool CheckProducer(string name, string adress, string phone, string inn) {
            if (name == "" || double.TryParse(name, out double numericValue)) { MessageBox.Show("Неправильно введено название!"); return false; };
            if (adress == "" || double.TryParse(adress, out double numeric)) { MessageBox.Show("Неправильно введен адресс!"); return false; };
            if (!Regex.IsMatch(phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase)) { MessageBox.Show("Неправильно введен номер телефона!"); return false; };
            if (!Regex.IsMatch(inn, @"^[\d+]{10,12}$", RegexOptions.IgnoreCase)) { MessageBox.Show("Неправильно введен ИНН!"); return false; };
            return true;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddButton.Content == "Изменить")
            {
                if (CheckProducer(nameBox.Text, adressBox.Text, phoneBox.Text, INNBox.Text))
                {
                    DialogResult = true;
                }

            }
            else
            {

                if (CheckProducer(nameBox.Text, adressBox.Text, phoneBox.Text, INNBox.Text))
                {
                    parent.addProducer(nameBox.Text, adressBox.Text, phoneBox.Text, INNBox.Text);
                    Close();
                }
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Поставщик успешно добавлен!");
        }
    }
}
