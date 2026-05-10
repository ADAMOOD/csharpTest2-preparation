using CitiesWPF.Models;
using CitiesWPF.Services;
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

namespace CitiesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiservice;
        public MainWindow()
        {
            _apiservice = new ApiService();
            InitializeComponent();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button kliknute = (Button)sender;
            var editovanySubject = kliknute.DataContext as Subject;
            if(editovanySubject is not null)
            {
                EditDialog edit = new EditDialog(editovanySubject);
                var result = edit.ShowDialog();

                // Jakmile se dialog zavře (a ideálně vrátil true), reloadneme DataGrid
                if (result == true)
                {
                    SubjectsDG.Items.Refresh(); // <-- TOTO JE TO KOUZLO
                }

            }
        }
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            btn.IsEnabled = false;
            var selected = CityCB.Text;
            if (selected!= string.Empty)
            {
                var subjects = await _apiservice.sendRequestAsync(selected.ToString());
                if (subjects is not null)
                {
                    SubjectsDG.ItemsSource = subjects;
                    btn.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("chyba pri nacitani api", "api error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    btn.IsEnabled = true;
                    return;
                }
            }
            else
            {
                btn.IsEnabled = true;
                MessageBox.Show("Vyberte mesto", "chyba vyberu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}