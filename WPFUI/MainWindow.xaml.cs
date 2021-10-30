using HelperLibrary;
using Models;
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

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button clear = new Button();
        public List<PersonModel> People = new List<PersonModel>();
        public MainWindow()
        {
            InitializeComponent();



        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            People.Clear();
            DataAccess dataAcccess = new DataAccess();
            People = dataAcccess.GetPeopleByLastName(searchEntry.Text);

            personInfoPanel.Children.Clear();
            foreach (PersonModel person in People)
            {
                TextBlock tb = new TextBlock();
                tb.Text = person.FullInfo;
                personInfoPanel.Children.Add(tb);
            }

            if (personInfoPanel.Children.Count > 0)
            {
                clear.Content = "clear";
                clear.Click += new RoutedEventHandler(clear_Click);
                searchPanel.Children.Add(clear);
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            personInfoPanel.Children.Clear();
            searchPanel.Children.Remove(clear);
        }

        private void searchEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                searchButton_Click(sender, e);
            }
        }
    }
}
