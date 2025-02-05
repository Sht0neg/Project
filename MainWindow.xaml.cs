using Microsoft.EntityFrameworkCore;
using System.Data;
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
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.IO;
using Microsoft.Win32;
using Aspose.Pdf;

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

        public void addGood(string name, string international, DateOnly? begin, DateOnly? end, bool? av, string rf, Producer producer, string batch, double price, int total) {
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

        private void ExpGoodButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=12345;database=pharmacy;";
            string query = "SELECT * FROM Goods";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Лист1");
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                    OpenFolderDialog dialog = new OpenFolderDialog();
                    dialog.Multiselect = false;
                    dialog.Title = "Select a folder";
                    bool? result = dialog.ShowDialog();
                    if (result == true)
                    {
                        string fullPathToFolder = dialog.FolderName;
                        excelPackage.SaveAs(new FileInfo($"{dialog.FolderName}\\ExportedData.xlsx"));
                    }
                    else excelPackage.SaveAs(new FileInfo("ExportedData.xlsx"));
                    Document doc = new Document();
                    doc.PageInfo.Margin.Left = 5;
                    doc.PageInfo.Margin.Right = 5;
                    doc.Pages.Add();
                    Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                    table.ColumnWidths = $"{dataTable.Columns[0].ColumnName.Count() * 7} {dataTable.Columns[1].ColumnName.Count() * 12} {dataTable.Columns[2].ColumnName.Count() * 4.5} {dataTable.Columns[3].ColumnName.Count() * 6} {dataTable.Columns[4].ColumnName.Count() * 8} 50 60 {dataTable.Columns[7].ColumnName.Count() * 5.7} {dataTable.Columns[8].ColumnName.Count() * 5.3} {dataTable.Columns[9].ColumnName.Count() * 7} {dataTable.Columns[10].ColumnName.Count() * 7} {dataTable.Columns[11].ColumnName.Count() * 5}";
                    table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
                    table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
                    table.ImportDataTable(dataTable, true, 0, 0, dataTable.Rows.Count, dataTable.Columns.Count);
                    doc.Pages[1].Paragraphs.Add(table);
                    doc.Save($"{dialog.FolderName}\\ExportedData.pdf");
                }
            }
        }

        private void ExpProdButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}