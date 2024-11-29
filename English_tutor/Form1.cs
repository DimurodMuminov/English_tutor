using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace English_tutor
{
    public partial class Form1 : Form
    {
        public static List<Student> students = new List<Student>();

       public static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "app(doNotDeleteOrChange).txt");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //read talabas from txt file
          //  listBox1.Items.Add("Dilmurod");

           // students.Add(new Student("Dilmurod", "https://t.me/dilmurodswords"));


            ReadData();
            FullListBox();
            try
            {
                listBox1.SelectedIndex = 0;
            }
            catch { }
        }

        private void FullListBox()
        {
            listBox1.Items.Clear();
            foreach (Student student in students)
            {
                listBox1.Items.Add(student.name);
            }
        }
        private void ReadData()
        {
            
            try
            {
                using (StreamReader sr = System.IO.File.OpenText(filePath))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] arr = s.Split('~');
                        students.Add(new Student(arr[0], arr[1]));
                    }
                }
            }
            catch
            {
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = students[listBox1.SelectedIndex].name;
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string telegramAccountLink = "https://t.me/Dilmurod_Muminov";
            

            // Start the Telegram app with the channel link as an argument
            Process.Start(new ProcessStartInfo
            {
                FileName =telegramAccountLink,
                UseShellExecute = true
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            this.Visible = false;
            addStudent.ShowDialog();
            FullListBox();
            this.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {
                MessageBoxButtons button = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Question;

                // Show the MessageBox and capture the result
                DialogResult result = MessageBox.Show("Tanlangan talabani o'chirmoqchimisiz?", "Talabani o'chirish", button, icon);

                // Check the user's response
                if (result == DialogResult.Yes)
                {
                    students.RemoveAt(listBox1.SelectedIndex);
                    FullListBox();
                    RemoveStudend();
                    MessageBox.Show("Talaba o'chirildi!");
                    textBox1.Text = "";

                }
            }
        }

        private void RemoveStudend()
        {
            System.IO.File.Delete(filePath);
            foreach (Student student in students)
            {
                using (StreamWriter sw = System.IO.File.CreateText(filePath))
                {
                    sw.WriteLine(student.name+"~"+student.channelUrl);
                }
            }
        }
        /*private string separateChannelName(string channelName)
        {
            //case public https://t.me/dilmurodswords
            //case private https://t.me/+3RhpmsDn50FiOTIy
            string[] blocs=channelName.Split('/');
            return blocs[blocs.Length-1];
        }*/

        private void button1_Click(object sender, EventArgs e)
        {

            string url="";
            if (students.Count > 0)
            {

                url = students[listBox1.SelectedIndex].channelUrl;

                //url=separateChannelName(url);

                try
                {

                    // The Telegram channel link (can also use tg:// link scheme)
                    string telegramChannelLink = "tg://resolve?domain=";//YourChannelName";
                    telegramChannelLink = url;


                    // Start the Telegram app with the channel link as an argument
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = telegramChannelLink,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    // Show an error message if something goes wrong
                    MessageBox.Show($"Telegram kanalni ochib bo'lmadi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
