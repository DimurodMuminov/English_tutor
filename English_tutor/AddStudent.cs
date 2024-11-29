using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace English_tutor
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // File path (change if needed)
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "app(doNotDeleteOrChange).txt");


            // Collect input
            Form1.students.Add(new Student(textBox1.Text, textBox2.Text));
            string content = $"{textBox1.Text}~{textBox2.Text}";

            try
            {
                // Attempt to create or append to the file
                if (!System.IO.File.Exists(filePath))
                {
                    // Create and write to the file
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine(content);
                    }
                }
                else
                {
                    // Append to the existing file
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied! Run the program as an administrator or use a different directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Final confirmation
            MessageBox.Show("Qo'shildi");
            this.Close();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {

        }


        /* private void button1_Click(object sender, EventArgs e)
         {
             // File path
             string filePath = @"C:\app(doNotDeleteOrChange).txt"; // Make sure the file path is correct and accessible

             Form1.students.Add(new Student(textBox1.Text,textBox2.Text));

             // Content to write
             string content = textBox1.Text;
             content += "~";
             content += textBox2.Text;

             try
             {
                 // Check if the file exists
                 MessageBox.Show("chek");
                 if (!System.IO.File.Exists(filePath))
                 {
                     // Create and write to the file if it doesn't exist
                     using (StreamWriter sw = File.CreateText(filePath))
                     {

                         sw.WriteLine(content);

                     }
                 }
                 else
                 {
                     MessageBox.Show("yozildi");
                     using (StreamWriter writer = File.AppendText(filePath))
                     {
                         writer.WriteLine(content);
                     }
                 }
             }
             catch (Exception ex)
             {
                 // Handle any errors
                 Console.WriteLine($"An error occurred: {ex.Message}");
             }


             MessageBox.Show("Qo'shildi");
             this.Close();
         }*/
    }
}
