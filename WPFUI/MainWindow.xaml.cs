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
        public Button clear = new Button();
        public List<PersonModel> People = new List<PersonModel>();
        public MainWindow()
        {
            InitializeComponent();
            SetClearButtonProperties();
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
            personInfoPanel.Children.Clear();

            DataAccess dataAcccess = new DataAccess();

            if (string.IsNullOrEmpty(searchEntry.Text) || searchEntry.Text == "*")
            {
                GetAllPeople(ref dataAcccess, ref People);
            }
            else
            {
                People = dataAcccess.GetPeopleByLastName(searchEntry.Text);
            }

  
            if (People.Count == 0)
            {
                // display error message
                TextBlock tb = new TextBlock();
                tb.Text = dataAcccess.NoResultsMessage;
                personInfoPanel.Children.Add(tb);

                // remove clear button
                if (searchPanel.Children.Contains(clear))
                {
                    searchPanel.Children.Remove(clear);
                }

                // dont populate clear button
            }
            else
            {
                foreach (PersonModel person in People)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = person.FullInfo;
                    personInfoPanel.Children.Add(tb);
                }

                if (personInfoPanel.Children.Count > 0 && !searchPanel.Children.Contains(clear))
                {
                    searchPanel.Children.Add(clear);
                }
            }
        }

        private void GetAllPeople(ref DataAccess p_dataAccess, ref List<PersonModel> p_people)
        {
            p_people = p_dataAccess.GetPeople();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            personInfoPanel.Children.Clear();
            searchPanel.Children.Remove(clear);
        }

        private void SetClearButtonProperties()
        {
            clear.Content = "clear";
            clear.Click += new RoutedEventHandler(clear_Click);
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