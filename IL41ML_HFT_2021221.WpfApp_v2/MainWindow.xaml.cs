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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IL41ML_HFT_2021221.WpfApp_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Text.RegularExpressions.Regex regex = new("[^0-9]+");
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBlock_InputValidation(object sender, TextCompositionEventArgs e) => e.Handled = this.regex.IsMatch(e.Text);
    }
}
