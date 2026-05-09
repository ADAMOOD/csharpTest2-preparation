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
using IC_WPF.Models;
using IC_WPF.Services;

namespace IC_WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for CompanyDialog.xaml
    /// </summary>
    
    public partial class CompanyDialog : Window
    {
        private readonly DbService _dbService;
        public CompanyDialog(Company company , DbService dbService)
        {
            InitializeComponent();
            FillInputs(company);
            _dbService = dbService;
        }

        public void FillInputs(Company company)
        {
            if (company.Name.Length != 0)
            {
                NazevTB.Text = company.Name;
            }

            if (company.Dic.Length != 0)
            {
                DicTB.Text= company.Dic;
            }

            if (company.Town.Length != 0)
            {
                ObecTB.Text=company.Town;
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string name = NazevTB.Text;
            string town = ObecTB.Text;
            string dic = DicTB.Text;
            string notes = PoznamkaTB.Text;
            if (name.Length == 0)
            {
                MessageBox.Show($"Jmeno je povinne", "Nevyplneno", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dic.Length == 0)
            {
                MessageBox.Show($"DIC je povinne", "Nevyplneno", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (town.Length==0)
            {
                MessageBox.Show($"Obec je povina", "Nevyplneno", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Company company = new Company()
            {
                Name = name,
                Dic = dic,
                Town = town,
                Notes = notes
            };

            int? insert = await _dbService.InsertCompanyAsync(company);
            if (insert is null)
            {
                MessageBox.Show($"Nastala chyba pri vkladani do databaze", "Chyba pri vkladani do databaze", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Firma {company.Name} uspesne vlozena do db", "Uspech", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;//automaticky zavre

            
        }
    }
}
