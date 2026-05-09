using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using IC_WPF.Models;
using IC_WPF.Services;

namespace IC_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;
        private readonly DbService _dbservice;
        public MainWindow()
        {
            InitializeComponent();
            Dapper.SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            _apiService = new ApiService(new HttpClient());
            _dbservice = new DbService();
            
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button.IsEnabled = false;
            string ic = IcTB.Text;
            if (ic.Length == 0)
            {
                MessageBox.Show("Musite zadat IC");
                Button.IsEnabled = true;
                return;
            }
            var response = await _apiService.makeRequest(ic);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("Zadane IC neexistuje");
                Button.IsEnabled = true;
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("nastala chyba - zkontrolujte sve ic","chyba",MessageBoxButton.OK,MessageBoxImage.Error);
                Button.IsEnabled = true;
                return;
            }

            Stream json = await response.Content.ReadAsStreamAsync();

            JsonNode node = await JsonNode.ParseAsync(json);
            string obchodniJmeno = node["obchodniJmeno"].ToString();
            string dic = node["dic"].ToString();
            var nazevObce = node["sidlo"]["nazevObce"].ToString();
            Company newCompany = new Company()
            {
                Name = obchodniJmeno,
                Dic = dic,
                Town = nazevObce,
            };
            Dialogs.CompanyDialog CompanyDialog = new Dialogs.CompanyDialog(newCompany,_dbservice);
            bool? vysledek = CompanyDialog.ShowDialog();

            if (vysledek == true)
            {
                await ReloadTable();
                IcTB.Text = "";

            }
            Button.IsEnabled = true;
        }

        private void IcTB_OnPreviewDragEnter(object sender, TextCompositionEventArgs e)
        {
            Regex numbers = new Regex("[^0-9]+");
            e.Handled = numbers.IsMatch(e.Text);
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await _dbservice.InitAsync();
            await ReloadTable();
        }

        private async Task ReloadTable()
        {
            List<Company> companies = await _dbservice.GetAllCompaniesAsync();
            if (companies.Any())
            {
                DataGrid.ItemsSource= companies;
            }
        }
    }
}