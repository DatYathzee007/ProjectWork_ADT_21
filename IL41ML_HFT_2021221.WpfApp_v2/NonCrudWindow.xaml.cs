using System.Collections.Specialized;
using System.Windows;
using IL41ML_HFT_2021221.WpfApp_v2;


namespace IL41ML_HFT_2021221.WpfApp_v2
{
    /// <summary>
    /// Interaction logic for NonCrudWindow.xaml
    /// </summary>
    public partial class NonCrudWindow : Window
    {
        public NonCrudWindow(INotifyCollectionChanged coll)
        {
            this.DataContext = new NonCrudViewModel(coll);
            this.InitializeComponent();
        }
    }
}
