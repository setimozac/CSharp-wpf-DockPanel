using System;
using System.Collections.Generic;
using System.IO;
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

namespace DockPanelPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        
    {
        public List<Car> cars = new List<Car>();
        const string DATAPATH = @"..\..\cars.txt";
        
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            LoadFromFile();
            gViewCar.ItemsSource = cars;
            
        }

        public void LoadFromFile()
        {
            
            try
            {
                
                string[] file = File.ReadAllLines(DATAPATH);

                foreach (string line in file)
                {
                    string[] item = line.Split(';');
                    CreateObject(item);
                }

            }
            catch (FileNotFoundException )
            {
                CatchAndClose("The file was not found");
            }
            catch (DirectoryNotFoundException )
            {
                CatchAndClose($"The directory was not found");
            }
            catch (IOException )
            {
                CatchAndClose($"The file could not be opened");
            }
        }//LoadFromFile()

        private void CreateObject(string[] line)
        {
            string model = line[0];
            string fuel = line[2];
            double size;
            if(double.TryParse(line[1], out size))
            {
                Car car = new Car(model, size, fuel);
                cars.Add(car);
                RefreshContent();
            }
        }//CreateObject()

        private void CatchAndClose(string msg)
        {
            MessageBox.Show(msg);
            Application.Current.Shutdown();
        }//CatchAndClose()

        private void gViewCar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RefreshContent();
        }
        private void RefreshContent()
        {
            gViewCar.Items.Refresh();
            tBoxStatusBar.Text = $"You have {cars.Count()} car(s) currently";
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(gViewCar.SelectedIndex != -1 && gViewCar.SelectedItems.Count == 1)
            {
                Car car = (Car)gViewCar.SelectedItem;
                CarDialog carDialog = new CarDialog("Update", car.MakeModel, car.EngineSize, car.Fuel);
                carDialog.AssignResult += (make, size, fuel) => { car.MakeModel = make; car.EngineSize = size; car.Fuel = fuel ; RefreshContent(); };
                carDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select One car to update!");
            }
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            CarDialog carDialog = new CarDialog("Add", "", 1.8, "");
            carDialog.AssignResult += (make, size, fuel) => { if( make != "") { Car newCar = new Car(make, size, fuel); cars.Add(newCar); }  };
            bool? result = carDialog.ShowDialog();
            if(result == true)
            {
                RefreshContent();
            }
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            if(gViewCar.SelectedIndex != -1)
            {
                
                var carsToDelete = gViewCar.SelectedItems;
                MessageBoxResult result = MessageBox.Show($"Are You sure? \n Deleting {carsToDelete.Count} Item(s) !", "Confirm", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    foreach (Car car in carsToDelete)
                    {
                        cars.Remove(car);
                    }
                }
                
                
            }
            else
            {
                MessageBox.Show("Please select car(s) to Delete!");
            }
            RefreshContent();

        }
    }

   
}
/*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
    using (StreamWriter writer = new StreamWriter(DATAFILE))
    {
        foreach (MyTask task in tasks)
        {
            writer.WriteLine(task.ToDataString());
        }

    }
}*/
