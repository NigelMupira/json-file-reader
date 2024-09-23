using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JSON_File_Reader
{
    public partial class Form2 : Form
    {

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadFileData(); // Access json file data upon form load
        }
        internal class QuizRepresent
        {
            public string category { get; set; }
            public QuestionsRepresent[] questions { get; set; }
        }

        internal class QuestionsRepresent
        {
            public string question { get; set; }
            public List<string> options { get; set; }
            public string answer { get; set; }
            public string correct_feadback { get; set; }
            public string wrong_feadback { get; set; }
        }

        public string quizFilePath; // Variable to store json file location
        private int questionIndex; // Tracker of the question being displayed
        private string userOption; // Variable to store the options for each question
        private int failCount = 0; // Count to keep tack of user fails to answer correctly

        public Form2()
        {
            InitializeComponent();
            txtQuizDisplay.Enabled = false; // Disable clicking or typing in the textbox
        }

        public void LoadFileData()
        {
            var jsonData = File.ReadAllText(quizFilePath); // Read the json file
            var quizData = JsonConvert.DeserializeObject<QuizRepresent>(jsonData); // Access the data within it to use in the form

            // Randomly select a question index
            Random random = new Random();
            questionIndex = random.Next(0, quizData.questions.Length - 1);

            // Display the selected question and options
            txtQuizDisplay.Clear(); // Clear the textbox before displaying the next question
            txtQuizDisplay.Text = quizData.questions[questionIndex].question; // Display the question
            btnOption1.Text = quizData.questions[questionIndex].options[0]; // Display the first option
            btnOption2.Text = quizData.questions[questionIndex].options[1]; // Display the second option
        }

        private void CheckAnswer()
        {
            var jsonData = File.ReadAllText(quizFilePath); // Read the json file
            var quizData = JsonConvert.DeserializeObject<QuizRepresent>(jsonData); // Access the data within it to use in the form

            if (userOption == quizData.questions[questionIndex].answer) // Check if the user's answer is correct
            {
                // If so then display correct feedback
                MessageBox.Show(quizData.questions[questionIndex].correct_feadback, "Correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                failCount = 0; // Resets the wrong answer count after a correct answer
                LoadFileData(); // Load another question upon correct answer
            }
            else
            {
                // If not then display wrong feedback
                MessageBox.Show(quizData.questions[questionIndex].wrong_feadback, "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                failCount++; // Increments the wrong answer count

                if (failCount >= 3)
                {
                    // If the user provides 3 consecutive wrong answers then display a message
                    MessageBox.Show("You have failed to answer correctly 3 times. The program will now terminate.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // Closes the application afterwards
                }
                else
                {
                    LoadFileData(); // Reload another question upon wrong answer but less than 3 consecutive times.
                }
            }
        }

        private void btnOption1_Click(object sender, EventArgs e)
        {
            userOption = btnOption1.Text; // Assign the user's answer to the selected option
            CheckAnswer(); // Check if the answer is correct
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            userOption = btnOption2.Text; // Assign the user's answer to the selected option
            CheckAnswer(); // Check if the answer is correct
        }
    }
}
