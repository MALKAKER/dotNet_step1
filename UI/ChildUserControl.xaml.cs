using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
        int count = 0;
        public ChildUserControl()
        {
            InitializeComponent();
            child = new Child();
            //bind the child entity to the view controls
            this.inputChildDetails.DataContext = child;

            //initialize controls
            this.childHMOComboBox.ItemsSource = Enum.GetValues(typeof(HMO));
            this.dateOfBirthDatePicker.Text = DateTime.Now.ToString();
            //this.pickLang.ItemsSource = Enum.GetValues(typeof(Language));
            this.childMoreDetails.ItemsSource = Enum.GetValues(typeof(ChildInfo));

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
        }

        private void expandedDetails_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(this.expandedDetails.Text).Equals(""))
            {
                Grid newGrid = new Grid();
                newGrid.Name = "childDetails" + count.ToString();
                // Create Columns  
                ColumnDefinition gridCol1 = new ColumnDefinition();
                gridCol1.Width = new GridLength(1, GridUnitType.Star);
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = new GridLength(3, GridUnitType.Star);

                newGrid.ColumnDefinitions.Add(gridCol1);
                newGrid.ColumnDefinitions.Add(gridCol2);
                
                ComboBox comboBox = new ComboBox( ) ;
                comboBox.Name = "childMoreDetails" + count.ToString();
                comboBox.ItemsPanel = new ItemsPanelTemplate();
                //add combobox to the grid in the first column
                Grid.SetColumn(comboBox, 0);
                
                //comboBox.BindingGroup="{}";todo
                TextBox textBox = new TextBox();
                Grid.SetColumn(comboBox, 1);

                this.listOfDetails.Items.Add(newGrid);
            }
        }

        private void isSpecial_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void listOfDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void childDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
