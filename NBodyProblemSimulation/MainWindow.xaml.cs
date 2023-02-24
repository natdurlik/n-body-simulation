using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace NBodyProblemSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CelestialObject> CelestialObjects { get; private set; } // move to universe?
        private Simulation _simulation;

        public MainWindow()
        {
            InitializeComponent();

            _simulation = new Simulation();
            CelestialObjects = _simulation.Universe.CelestialObjects;

            DataContext = this;
        }

        private void ButtonAddNewObject_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameInputTxt.Text)
                && CelestialObjects.All(i => i.Name != nameInputTxt.Text)
                && !string.IsNullOrWhiteSpace(xInputTxt.Text)
                && !string.IsNullOrWhiteSpace(yInputTxt.Text)
                && !string.IsNullOrWhiteSpace(massInputTxt.Text)
                && float.TryParse(xInputTxt.Text, out float x)
                && float.TryParse(yInputTxt.Text, out float y)
                && float.TryParse(massInputTxt.Text, out float mass))
            {
                if(!string.IsNullOrWhiteSpace(vxInputTxt.Text)
                    && !string.IsNullOrWhiteSpace(vyInputTxt.Text)
                    && float.TryParse(vxInputTxt.Text, out float vx)
                    && float.TryParse(vyInputTxt.Text, out float vy))
                {
                    CelestialObjects.Add(new(nameInputTxt.Text, x, y, vx, vy, mass));
                    vxInputTxt.Clear();
                    vyInputTxt.Clear();
                }
                else
                {
                    CelestialObjects.Add(new(nameInputTxt.Text, x, y, mass));                    
                }

                nameInputTxt.Clear();
                xInputTxt.Clear();
                yInputTxt.Clear();
                massInputTxt.Clear();
            }
        }

        private void ButtonDeleteObjectByName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameInputTxt.Text) && CelestialObjects.Any(i => i.Name == nameInputTxt.Text))
            {
                CelestialObjects.Remove(CelestialObjects.Single(x => x.Name == nameInputTxt.Text));
                nameInputTxt.Clear();
            }
        }

        private void ButtonChangeObjectByName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameInputTxt.Text)
                && CelestialObjects.Any(i => i.Name == nameInputTxt.Text))
            {
                var found = CelestialObjects.FirstOrDefault(i => i.Name == nameInputTxt.Text);
                if (!string.IsNullOrWhiteSpace(xInputTxt.Text) && float.TryParse(xInputTxt.Text, out float x))
                {
                    found.X = x;
                    xInputTxt.Clear();
                }

                if (!string.IsNullOrWhiteSpace(yInputTxt.Text) && float.TryParse(yInputTxt.Text, out float y))
                {
                    found.Y = y;
                    yInputTxt.Clear();
                }

                if (!string.IsNullOrWhiteSpace(vxInputTxt.Text) && float.TryParse(vxInputTxt.Text, out float vx))
                {
                    found.VX = vx;
                    vxInputTxt.Clear();
                }

                if (!string.IsNullOrWhiteSpace(vyInputTxt.Text) && float.TryParse(vyInputTxt.Text, out float vy))
                {
                    found.VY = vy;
                    vyInputTxt.Clear();
                }

                if (!string.IsNullOrWhiteSpace(massInputTxt.Text) && float.TryParse(massInputTxt.Text, out float mass))
                {
                    found.Mass = mass;
                    massInputTxt.Clear();
                }

                nameInputTxt.Clear();
            }
        }

        private void ButtonStartSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (!_simulation.Running)
            {
                _simulation.Running = true;
                _simulation.Simulate();
            }
        }

        private void ButtonStopSimulation_Click(object sender, RoutedEventArgs e)
        {
            _simulation.Running = false;
        }

        private void RadioButtonGravity_Click(object sender, RoutedEventArgs e)
        {
            if (Radio3D.IsChecked == true)
            {
                _simulation.Set3DGravity();
            }
            else
            {
                _simulation.Set2DGravity();
            }
        }
    }
}