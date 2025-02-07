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
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.IO;
using Microsoft.Win32;
using Aspose.Pdf;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        string connectionString = "server=localhost;user=root;password=12345;database=pharmacy;";
        public ExportWindow()
        {
            InitializeComponent();
        }

        public ExportWindow(string name) {
            InitializeComponent();
            this.Title = name;
        }


        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "Good")
            {
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
                            excelPackage.SaveAs(new FileInfo($"{dialog.FolderName}\\ExportedGoods{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.xlsx"));
                        }
                        else excelPackage.SaveAs(new FileInfo($"ExportedGoods{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.xlsx"));
                    }
                }
            }
            else {
                string query = "SELECT * FROM Producers";
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
                            excelPackage.SaveAs(new FileInfo($"{dialog.FolderName}\\ExportedProducers{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.xlsx"));
                        }
                        else excelPackage.SaveAs(new FileInfo($"ExportedProducers{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.xlsx"));
                    }
                }
            }
            Close();
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title == "Good")
            {
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
                        OpenFolderDialog dialog = new OpenFolderDialog();
                        dialog.Multiselect = false;
                        dialog.Title = "Select a folder";
                        bool? result = dialog.ShowDialog();
                        doc.Save($"{dialog.FolderName}\\ExportedGoods{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.pdf");
                    }
                }
            }
            else {
                string query = "SELECT * FROM Producers";
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
                        Document doc = new Document();
                        doc.PageInfo.Margin.Left = 5;
                        doc.PageInfo.Margin.Right = 5;
                        doc.Pages.Add();
                        Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                        table.ColumnWidths = $"100 100 150 100 100";
                        table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray));
                        table.ImportDataTable(dataTable, true, 0, 0, dataTable.Rows.Count, dataTable.Columns.Count);
                        doc.Pages[1].Paragraphs.Add(table);
                        OpenFolderDialog dialog = new OpenFolderDialog();
                        dialog.Multiselect = false;
                        dialog.Title = "Select a folder";
                        bool? result = dialog.ShowDialog();
                        doc.Save($"{dialog.FolderName}\\ExportedProducers{DateOnly.FromDateTime(DateTime.Today).ToString().Replace(".", "")}.pdf");
                    }
                }
            }
            Close();
        }
    }
}
