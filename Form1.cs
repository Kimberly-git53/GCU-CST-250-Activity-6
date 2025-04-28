using System.Globalization;
using ServiceStack;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace FrmInventory
{
    public partial class FrmInventory : Form
    {
        /// List to hold the items
        private List<Thing> thingsInMyPocket = new List<Thing>();
        BindingSource thingsBindingSource = new BindingSource();
        /// Constructor
        public FrmInventory()
        {
            InitializeComponent();
        }
        /// Load event handler
        private void FrmInventory_Load(object sender, EventArgs e)
        {
            txtValue.Text = string.Format(CultureInfo.CurrentCulture, "{0:C2}", 0);
            txtValue.Leave += txtValue_Leave;
            thingsBindingSource.DataSource = thingsInMyPocket;
            gridInventory.DataSource = thingsBindingSource;
        }
        /// Event handler for the txtValue leave event. Format the value as currency
        private void txtValue_Leave(object? sender, EventArgs e)
        {
            decimal value;
            // Validate the value is a valid decimal number
            if (decimal.TryParse(txtValue.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out value))
            {
                txtValue.Text = string.Format(CultureInfo.CurrentCulture, "{0:C2}", value);
            }
            // If the value is not valid, show a message box and set focus back to the text box
            else
            {
                MessageBox.Show("Invalid value");
                txtValue.Focus();
                txtValue.SelectAll();
            }
        }
        // Add button click event. Create a new Thing obejct and add it to the list
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate input
            try
            {
                decimal value = 0;

                // Validate value is not negative  
                if (decimal.TryParse(txtValue.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out value) && value < 0)
                {
                    MessageBox.Show("Value cannot be negative.", "Validation Error", MessageBoxButtons.OK);
                    txtValue.Focus();
                    txtValue.SelectAll();
                    return;
                }
                // Validate name is not empty
                Thing thing = new Thing
                {
                    Id = thingsInMyPocket.Count + 1,
                    Name = txtName.Text,
                    Value = value
                };
                // Add the new item to the list
                thingsInMyPocket.Add(thing);
                // Update the grid view
                thingsBindingSource.DataSource = thingsInMyPocket;
                thingsBindingSource.ResetBindings(false);
                SetMaxValue();
                UpdateStatsLabel();
            }
            // Catch errors and show message box
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }
        // Save button click event. Save the list to a file
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save File";
                // Setting filter to show different file types
                saveFileDialog.Filter = "Text Files (*.txt)|CSV Files (*.csv)|*.csv|JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                // Set default file name
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    try
                    {
                        // Depending on the file extension, save the appropriate format
                        if (fileName.EndsWith(".json"))
                        {
                            string json = ServiceStack.Text.JsonSerializer.SerializeToString(thingsInMyPocket);
                            File.WriteAllText(fileName, json);
                        }
                        else if (fileName.EndsWith(".csv"))
                        {
                            string csv = ServiceStack.Text.CsvSerializer.SerializeToString(thingsInMyPocket);
                            File.WriteAllText(fileName, csv);
                        }
                        else if (fileName.EndsWith(".txt"))
                        {
                            // Write each item to the text file
                            foreach (Thing thing in thingsInMyPocket)
                            {
                                File.AppendAllText(fileName, thing.ToString() + Environment.NewLine);
                            }
                        }
                        else if (fileName.EndsWith(".xml"))
                        {
                            // Serialize the list to XML
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Thing>));
                            using (StreamWriter writer = new StreamWriter(fileName))
                            {
                                serializer.Serialize(writer, thingsInMyPocket);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid file extension.");
                        }
                    }
                    // Catch errors and show message box
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving file: " + ex.Message);
                    }
                }
            }
        }
        // Load button click event. Load the list from a file
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Setting filter to show different file types
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        // Code to load data from a file
                        string fileName = openFileDialog.FileName;

                        //load from json if extension is .json
                        if (fileName.EndsWith(".json"))
                        {
                            // read json file
                            string json = File.ReadAllText(fileName);
                            // deserialize json to list using service stack
                            thingsInMyPocket = ServiceStack.Text.JsonSerializer.DeserializeFromString<List<Thing>>(json);
                            thingsBindingSource.ResetBindings(false);
                            SetMaxValue();
                            UpdateStatsLabel();
                        }
                        else if (fileName.EndsWith(".csv"))
                        {
                            // read csv file
                            string csv = File.ReadAllText(fileName);
                            // deserialize csv to list using service stack
                            thingsInMyPocket = ServiceStack.Text.CsvSerializer.DeserializeFromString<List<Thing>>(csv);
                            thingsBindingSource.ResetBindings(false);
                            SetMaxValue();
                            UpdateStatsLabel();
                        }
                        // load from xml if extension is .xml
                        else if (fileName.EndsWith(".xml"))
                        {
                            // read xml file
                            XmlSerializer serializer = new XmlSerializer(typeof(List<Thing>));
                            // deserialize xml to list using xml serializer
                            using (StreamReader reader = new StreamReader(fileName))
                            {
                                thingsInMyPocket = (List<Thing>)serializer.Deserialize(reader);
                            }
                            // update the grid view
                            thingsBindingSource.DataSource = thingsInMyPocket;
                            thingsBindingSource.ResetBindings(false);
                            SetMaxValue();
                            UpdateStatsLabel();
                        }
                        else if (fileName.EndsWith(".txt"))
                        {
                            //read each line of the text file
                            string[] lines = File.ReadAllLines(fileName);
                            thingsInMyPocket.Clear();
                            // loop through each line and parse the values
                            foreach (string line in lines)
                            {
                                string[] parts = line.Split(',');
                                // check if the line is empty
                                for (int i = 0; i < parts.Length; i++)
                                {
                                    MessageBox.Show(parts[i]);
                                }
                                // create a new thing object and add it to the list
                                Thing thing = new Thing();
                                // parse the line to get the id, name and value
                                String idString = parts[0].Split('=')[1].Trim();
                                MessageBox.Show("IdString: '" + idString + "'");
                                thing.Id = int.Parse(idString);
                                thing.Name = parts[1].Split("=")[1].Trim();
                                string valueString = parts[2].Split('=')[1].Trim();
                                MessageBox.Show("ValueString: '" + valueString + "'");
                                thing.Value = decimal.Parse(valueString);
                                // add the thing to the list
                                thingsInMyPocket.Add(thing);
                            }
                        }
                        // update the grid view
                        thingsBindingSource.DataSource = thingsInMyPocket;
                        thingsBindingSource.ResetBindings(false);
                        SetMaxValue();
                        UpdateStatsLabel();
                    }
                    // Catch errors and show message box
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading file: " + ex.Message);
                    }
                }
            }
        }
        // Select the least valuable item
        private void rbtnLeastValuable_CheckedChanged(object sender, EventArgs e)
        {
            // Order the items by their Value property in ascending order and take the first 3 items
            var leastValuableItems = (from thing in thingsInMyPocket
                                      orderby thing.Value ascending
                                      select thing).Take(3).ToList();
            // Display the least valuable items in the grid
            thingsBindingSource.DataSource = leastValuableItems;
            thingsBindingSource.ResetBindings(false);
            // Update stats label
            UpdateStatsLabel();
        }
        // Select the most valuable item
        private void rbtnMostValuable_CheckedChanged(object sender, EventArgs e)
        {
            // use method syntax to order the items by their Value property in descending order and take the first 3 items
            var mostValuableItems = thingsInMyPocket.OrderByDescending(thing => thing.Value).Take(3).ToList();
            // Display the most valuable items in the grid
            thingsBindingSource.DataSource = mostValuableItems;
            thingsBindingSource.ResetBindings(false);
            // Update stats label
            UpdateStatsLabel();
        }
        // Show all items
        private void rbtnShowAll_CheckedChanged(object sender, EventArgs e)
        {
            // Use LINQ to order the items by their Id
            var sortedItems = thingsInMyPocket.OrderBy(thing => thing.Id).ToList();
            // Display all items in the grid
            thingsBindingSource.DataSource = sortedItems;
            thingsBindingSource.ResetBindings(false);
            // Update stats label
            UpdateStatsLabel();
        }

        // Filter the grid to show all items between two value ranges 
        private void rangeMinMax_Click(object sender, EventArgs e)
        {
            int min = rangeMinMax.LowerValue;
            int max = rangeMinMax.UpperValue;

            // Use Query syntax to filter items within the value range of the trackbar
            var rangeList = (from thing in thingsInMyPocket
                             where thing.Value >= min && thing.Value <= max
                             select thing).ToList();
            // Display the filtered items in the grid
            thingsBindingSource.DataSource = rangeList;
            thingsBindingSource.ResetBindings(false);
            // Update stats label
            UpdateStatsLabel();
        }
        // Set the maximum value of the trackbar to the maximum value of the items in the list
        private void SetMaxValue()
        {
            {
                decimal maxValuedItem = 0;
                // Loop through the list of items and find the maximum value
                foreach (Thing thing in thingsInMyPocket)
                {
                    // Check if the current item's value is greater than the maximum value
                    if (thing.Value > maxValuedItem)
                    {
                        maxValuedItem = thing.Value;
                    }
                }
                // Set the maximum value of the trackbar to the maximum value of the items in the list
                rangeMinMax.MaxValue = (int)maxValuedItem;
            }
        }
        // Search event handler
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Trim the search text and check if it is empty
            string searchText = txtSearch.Text.Trim();
            decimal searchValue;
            int intValue;
            // Check if the search text can be parsed as a decimal or integer
            bool isDecimalSearch = decimal.TryParse(searchText, NumberStyles.Currency, CultureInfo.CurrentCulture, out searchValue);
            bool isIntegerSearch = int.TryParse(searchText, out intValue);
            // Perform the search based on the type of search
            var searchResults = thingsInMyPocket.Where(thing =>
                thing.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                thing.Value.ToString().Contains(searchText)
                ).ToList();


            // Update grid view
            thingsBindingSource.DataSource = searchResults;
            thingsBindingSource.ResetBindings(false);
            // Update stats label
            UpdateStatsLabel();
        }
        // Stats label update
        private void UpdateStatsLabel()
        {
            // Get the count of items in the list
            int itemCount = thingsInMyPocket.Count;
            // Calculate the total value of all items in the list
            decimal totalValue = thingsInMyPocket.Sum(thing => thing.Value);
            // Update the stats label with the item count and total value
            lblStats.Text = $"Items: {itemCount}, Total Value: {totalValue:C2}";

        }
        // Exit menu item click event
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }
    }
}
