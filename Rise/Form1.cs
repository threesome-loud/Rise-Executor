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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rise
{
    public partial class Form1 : RoundedForm
    {
        Timer time = new Timer();
        public Point mouseLocation;

        public Form1()
        {
            InitializeComponent();
            time.Tick += timertick;
            time.Start();
            ForlornApi.Api.InitializeForlorn();

        }

        private void timertick(object sender, EventArgs e)
        {
            if (ForlornApi.Api.IsRobloxOpen())
            {
                robloxopen.Text = "Roblox Open: ✅";
                robloxopen.ForeColor = Color.LightGreen;  // Change text color to green
            }
            else
            {
                robloxopen.Text = "Roblox Open: ❌";
                robloxopen.ForeColor = Color.Blue;  // Change text color to red
            }

            if (ForlornApi.Api.IsInjected())
            {
                status.Text = "Status: Injected!";
                status.ForeColor = Color.LightGreen;  // Change text color to green
            }
            else
            {
                status.Text = "Status: Not Injected!";
                status.ForeColor = Color.Blue;  // Change text color to red
            }
        }


        private void Inject_Click(object sender, EventArgs e)
        {
            ForlornApi.Api.Inject();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            ForlornApi.Api.ExecuteScript(Editor.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ForlornApi.Api.SetAutoInject(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ForlornApi.Api.KillRoblox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Editor.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor.Text = File.ReadAllText($"./Scripts/{listBox1.SelectedItem}");
        }

        class functions
        {
            public static void PopulateListBox(System.Windows.Forms.ListBox lsb, string Folder, string FileType)
            {
                DirectoryInfo dinfo = new DirectoryInfo(Folder);
                FileInfo[] Files = dinfo.GetFiles(FileType);
                foreach (FileInfo file in Files)
                {
                    lsb.Items.Add(file.Name);
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            functions.PopulateListBox(listBox1, "./Scripts", "*.lua");
            functions.PopulateListBox(listBox1, "./Scripts", "*.txt");
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt",
                Title = "Open Lua or Text File"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Editor.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void savefile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt",
                DefaultExt = "lua",
                Title = "Save Lua or Text File"
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string textToSave = Editor.Text;
                using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(textToSave);
                }
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;
        }

        private void robloxopen_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
