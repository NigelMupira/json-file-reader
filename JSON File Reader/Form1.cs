using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSON_File_Reader
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // The quiz json file is located in "C:\Users\nigel\Documents\HIT\1.2\ISS1201 Vis Programming Concepts\13. prac ass 2"
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Open a file dialog to select our json file
            openFileDialog.ShowDialog();
            txtFileDirectory.Text = openFileDialog.FileName; // Display the file (address and) name to the textbox
            Form2 ourForm2 = new Form2(); // Create a new instance of form 2
            ourForm2.quizFilePath = txtFileDirectory.Text; // Assign the file directory to the variable quizPath from form 2
            ourForm2.Show(); // Show form 2
            this.Hide(); // Hide the current form
        }
    }
}
