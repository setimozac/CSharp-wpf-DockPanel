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

namespace DockPanelPractice
{
    /// <summary>
    /// Interaction logic for CarDialog.xaml
    /// </summary>
    public partial class CarDialog : Window
    {
        private string make;
        private double size;
        private string fuel;
        public string[] fuelTypes = new string[2];
        public CarDialog(string buttonName, string make, double size, string fuel)
        {
            InitializeComponent();
            this.make = make;
            this.size = size;
            this.fuel = fuel;
            Load(buttonName);
        }

        private void Load(string buttonName)
        {
            fuelTypes[0] = "Gasoline";
            fuelTypes[1] = "Hybrid";
            comboFuel.ItemsSource = fuelTypes;
            btnAction.Content = buttonName;
            tBoxMake.Text = make;
            sliderEngine.Value = size;
            if(fuelTypes.Contains(fuel))
            {
                comboFuel.SelectedValue = fuel;
            }
            else
            {
                comboFuel.SelectedValue = fuelTypes[0];
                fuel = fuelTypes[0];
            }
            
        }
    }
}
