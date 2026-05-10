using CitiesWPF.Models;
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

namespace CitiesWPF
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    public partial class EditDialog : Window
    {
        private Subject _edited;
        public EditDialog( Subject subject)
        {
            InitializeComponent();
            _edited = subject;
            NazevTB.Text = _edited.Nazev;
            icoTB.Text = _edited.ico;
            datumTB.Text = _edited.zapisDatum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 1. Uložíme nové hodnoty z formuláře zpět do reference objektu
            _edited.Nazev = NazevTB.Text;
            _edited.ico = icoTB.Text;
            _edited.zapisDatum = datumTB.Text;

            // 2. Nastavíme výsledek dialogu na true (Úspěch).
            // WPF okno automaticky zavře a pošle 'true' tam, odkud jsi volal ShowDialog().
            this.DialogResult = true;
        }
    }
}
