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
    /// Interaction logic for Child.xaml
    /// </summary>
    public partial class ChildUserControl : UserControl
    {
        Child child;
        private bool dateChanged = false;
        public ChildUserControl()
        {
            InitializeComponent();
            child = new Child();
            //bind the child entity to the view controls
            this.inputChildDetails.DataContext = child;

            //initialize controls -> todo

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void update_click(object sender, RoutedEventArgs e)
        {

        }

        private void remove_click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetButton();
        }

        private void childHMOComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetButton();
        }

        private void pickLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetButton();
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetButton();
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetButton();
        }

        private void iDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetButton();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            SetButton();
        }

        private void SetButton()
        {
            updateB.IsEnabled = (this.lastNameTextBox.Text != "" || this.firstNameTextBox.Text != "" || this.iDTextBox.Text != ""
                ) ? true : false;// TODO
        }
    }
}
