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
        public List<string> HeaderInfo = new List<string> { "Id:", "First Name:", "Last Name:", "Email Address:", "Phone Number:" };
        public string NoResultsMessage = "No results for: ";
        public PersonModel Header = new PersonModel();
        public List<StackPanel> defaultHeader = new List<StackPanel>();

        public List<StackPanel> InitDisplayGrid()
        {
            List<StackPanel> Columns = new List<StackPanel>();
            personInfoPanel.Orientation = Orientation.Horizontal;

            // make as many columns as header titles exist
            for (int i = 0; i < HeaderInfo.Count; ++i)
            {
                StackPanel column = new StackPanel();
                column.Orientation = Orientation.Vertical;
                column.Margin = new Thickness(5, 0, 5, 0);
                Columns.Add(column);
            }

            // put a block with each header title into each column
            for (int i = 0; i < HeaderInfo.Count; ++i)
            {
                TextBlock block = new TextBlock();
                block.FontFamily = new FontFamily("Segoe UI");
                block.FontWeight = FontWeights.Bold;
                block.Margin = new Thickness(0,5,0,5);
                block.Text = HeaderInfo[i];
                Columns[i].Children.Add(block);
            }

            return Columns;

        }

        public MainWindow()
        {
            InitializeComponent();
            SetClearButtonProperties();
            defaultHeader = InitDisplayGrid();
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
            ResetDisplayGrid();

            DataAccess dataAcccess = new DataAccess();

            if (string.IsNullOrEmpty(searchEntry.Text) || searchEntry.Text == "*")
            {
                if (MessageBox.Show("Display all Person information from databse?", "Display all", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    GetAllPeople(ref dataAcccess, ref People);
                }
                else
                {
                    return;
                }
            }
            else
            {
                People = dataAcccess.GetPeopleByAnyValue(searchEntry.Text);
            }

  
            if (People.Count == 0)
            {
                // display error message
                TextBlock tb = new TextBlock();
                tb.Text = NoResultsMessage + $"\"{searchEntry.Text}\"";
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
                FillPersonInfoPanel();

                if (personInfoPanel.Children.Count > 0 && !searchPanel.Children.Contains(clear))
                {
                    searchPanel.Children.Add(clear);
                }
            }
        }

        private void FillPersonInfoPanel()
        {
            // fill column info
            FillInfoColumns(defaultHeader[0], ref People, "Id");
            FillInfoColumns(defaultHeader[1], ref People, "FirstName");
            FillInfoColumns(defaultHeader[2], ref People, "LastName");
            FillInfoColumns(defaultHeader[3], ref People, "EmailAddress");
            FillInfoColumns(defaultHeader[4], ref People, "PhoneNumber");

            foreach (StackPanel column in defaultHeader)
            {
                personInfoPanel.Children.Add(column);
            }
        }

        private void FillInfoColumns(StackPanel p_column, ref List<PersonModel> p_people, string p_memberKey)
        {
            if (p_memberKey == "Id")
            {
                foreach (PersonModel person in p_people)
                {
                    TextBlock info = new TextBlock();
                    info.Text = person.Id.ToString();
                    p_column.Children.Add(info);
                }
            }
            else if (p_memberKey == "FirstName")
            {
                foreach (PersonModel person in p_people)
                {
                    TextBlock info = new TextBlock();
                    info.Text = person.FirstName;
                    p_column.Children.Add(info);
                }
            }
            else if (p_memberKey == "LastName")
            {
                foreach (PersonModel person in p_people)
                {
                    TextBlock info = new TextBlock();
                    info.Text = person.LastName;
                    p_column.Children.Add(info);
                }
            }
            else if (p_memberKey == "EmailAddress")
            {
                foreach (PersonModel person in p_people)
                {
                    TextBlock info = new TextBlock();
                    info.Text = person.EmailAddress;
                    p_column.Children.Add(info);
                }
            }
            else if (p_memberKey == "PhoneNumber")
            {
                foreach (PersonModel person in p_people)
                {
                    TextBlock info = new TextBlock();
                    info.Text = person.PhoneNumber;
                    p_column.Children.Add(info);
                }
            }
        }

        private void GetAllPeople(ref DataAccess p_dataAccess, ref List<PersonModel> p_people)
        {
            p_people = p_dataAccess.GetPeople();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            ResetDisplayGrid();
            searchPanel.Children.Remove(clear);
            searchEntry.Text = "";
        }

        private void ResetDisplayGrid()
        {
            personInfoPanel.Children.Clear();
            defaultHeader = InitDisplayGrid();
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

        private void insertButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Write the following to the database: \nFirst Name: {firstNameEntry.Text}\nLast Name: {lastNameEntry.Text}\nEmail Address: {emailEntry.Text}\nPhone Number: {phoneNumberEntry.Text}", "Write to database", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DataAccess da = new DataAccess();
                da.InsertPerson(firstNameEntry.Text, lastNameEntry.Text, emailEntry.Text, phoneNumberEntry.Text);
                firstNameEntry.Text = "";
                lastNameEntry.Text = "";
                emailEntry.Text = "";
                phoneNumberEntry.Text = "";
            }
        }

        private void firstNameEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                insertButton_Click(sender, e);
            }
        }

        private void lastNameEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                insertButton_Click(sender, e);
            }
        }

        private void emailEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                insertButton_Click(sender, e);
            }
        }

        private void phoneNumberEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                insertButton_Click(sender, e);
            }
        }
    }
}