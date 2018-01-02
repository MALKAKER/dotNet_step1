using BE;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            //List<Child> g = GlobalVariablles.mybl.getAllChildren();
            //mybl = BL.FactoryBL.Instance;
        }

        private void NannyOption(object sender, RoutedEventArgs e)
        {
            Window nannyOption = new Nanny();
            nannyOption.ShowDialog();
        }

        private void ParentOption(object sender, RoutedEventArgs e)
        {
            Window nannyOption = new parent();
            nannyOption.ShowDialog();
        }

        private void AboutOption(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void ContractOption(object sender, RoutedEventArgs e)
        {
            Window nannyOption = new contract();
            nannyOption.ShowDialog();
        }
    }
    /// <summary>
    /// class GlobalVariablles, the class initiate once the bl instance though the bl created once in each run
    /// </summary>
    public class GlobalVariables
       {
        public static BL.IBL mybl = BL.FactoryBL.Instance;
        public GlobalVariables()
        {
           
        }
    }
}
