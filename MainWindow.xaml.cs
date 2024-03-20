using System.Data;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
{

}

namespace To_Do_App
{
    
    public partial class MainWindow : Window
    {
        DataTable todoList = new DataTable();
        bool isEditing = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataGrid();

        }

        private void InitializeDataGrid()
        {
            // Create columns
            todoList.Columns.Add("Title", typeof(string));
            todoList.Columns.Add("Description", typeof(string));
            todoList.Columns.Add("Completed", typeof(bool));

            //Point our DataGrid to our DataSource
            toDoListView.ItemsSource = todoList.DefaultView;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            titleTextbox.Text = "";
            descriptionTextbox.Text = "";
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true;
            // fill text fields with data from table
            titleTextbox.Text = todoList.Rows[toDoListView.SelectedIndex].ItemArray[0].ToString();
            descriptionTextbox.Text = todoList.Rows[toDoListView.SelectedIndex].ItemArray[1].ToString();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                todoList.Rows[toDoListView.SelectedIndex].Delete();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                todoList.Rows[toDoListView.SelectedIndex]["Title"] = titleTextbox.Text;
                todoList.Rows[toDoListView.SelectedIndex]["Description"] = descriptionTextbox.Text;
            }
            else
            {
                try
                {
                    DataRow newRow = todoList.NewRow();
                    newRow["Title"] = titleTextbox.Text;
                    newRow["Description"] = descriptionTextbox.Text;
                    newRow["Completed"] = false;
                    todoList.Rows.Add(newRow);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
            //Clear fields
            titleTextbox.Text = "";
            descriptionTextbox.Text = "";
            isEditing = false;
        }

        private void saveDataButton_Click(object sender, RoutedEventArgs e)
        {
                // Create a StringBuilder to store the CSV data
                StringBuilder csvData = new StringBuilder();

                // Add the header row to the CSV data
                csvData.AppendLine("Title,Description,Completed");

                // Iterate through the rows in the DataTable
                foreach (DataRow row in todoList.Rows)
                {
                    // Add each row to the CSV data
                    csvData.AppendLine($"{row["Title"]},{row["Description"]},{row["Completed"]}");
                }

                // Save the CSV data to a file
                System.IO.File.WriteAllText("todoList.csv", csvData.ToString());
            
        }
    }
}