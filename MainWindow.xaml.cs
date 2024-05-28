using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Context? context;
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Главная страница";
        }

        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
            AddGoodsWindow add = new AddGoodsWindow(this);
            bool? result = add.ShowDialog();
        }

        private void AddProdButton_Click(object sender, RoutedEventArgs e)
        {
            AddProducerWindow add = new AddProducerWindow(this);
            bool? result = add.ShowDialog();
        }

        private void InfGoodButton_Click(object sender, RoutedEventArgs e)
        {
            GoodsWindow inf = new GoodsWindow(this);
            bool? result = inf.ShowDialog();
        }

        private void InfProdButton_Click(object sender, RoutedEventArgs e)
        {
            InfProducerWinsow inf = new InfProducerWinsow(this);
            bool? result = inf.ShowDialog();
        }

        public void addProducer(string name, string adress, string phone, string inn)
        {
            context = new Context();
            context.Producers.Load();
            context.Producers.Add(new Producer
            {
                Name = name,
                Phone = phone,
                Address = adress,
                INN = inn
            });
            try
            {
                context.SaveChanges();

            } catch (Exception ex)
            {
                MessageBox.Show("Производитель с таким названием уже существует");
            }

        }

        public List<Producer> ProducerList() {
            List<Producer> list = new List<Producer>();
            context = new Context();
            foreach (Producer prod in context.Producers ) {
                list.Add( prod );
            }
            return list;
        }

        public void addGood(string name, string international, DateTime? begin, DateTime? end, bool? av, string rf, Producer producer, string batch, double price, int total) {
            context = new Context();
            context.Goods.Load();
            context.Goods.Add(new Goods
            {
                Name = name,
                Intenational = international,
                DataBegin = begin,
                DataEnd = end,
                Availability = av,
                RF = rf,
                ProducerName = producer.Name,
                ProducerId = producer.ProducerId,
                Batch = batch,
                Price = price,
                Total = total
            });
            context.SaveChanges(true);
        }
    }
}