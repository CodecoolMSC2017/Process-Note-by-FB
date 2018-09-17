using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_Note
{

    using System.Diagnostics;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyProcess selectedProcess;
        List<MyProcess> myProcesses;

        private void Form1_Load(object sender, EventArgs e)
        {
            List<MyProcess> Processes = new List<MyProcess>();
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                Processes.Add(new MyProcess(theprocess));
            }
            var sorted = Processes.OrderBy(process => process.Name);
            myProcesses = sorted.ToList();
            CreateListView1(myProcesses);
            CreateListView2();
        }

        private void CreateListView1(List<MyProcess> sorted)
        {
            listView1.View = View.Details;

            listView1.Columns.Add("ID");
            listView1.Columns.Add("Name");

            foreach (MyProcess theprocess in sorted)
            {
                listView1.Items.Add(new ListViewItem(new string[] { theprocess.Id.ToString(), theprocess.Name }));
            }
            listView1.GridLines = true;
            listView1.Columns[0].Width = 400;
            listView1.Columns[1].Width = 400;

        }

        private void ListView1_Click(object sender, EventArgs e)
        {
            var firstSelectedItem = listView1.SelectedItems[0];
            if (selectedProcess != null)
            {
                if (selectedProcess.Id.Equals(firstSelectedItem.Text)) return;

                if (textBox1.Text != "" )
                {
                    DialogResult result = MessageBox.Show("Continue without saving the comment?",
                    "Comment isn't saved",
                    MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
            
            }
            
       

            
            selectedProcess = GetProcessFromId(firstSelectedItem.Text);
            AddNewItemListView2();
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            var firstSelectedItem = listView1.SelectedItems[0];
            selectedProcess = GetProcessFromId(firstSelectedItem.Text);
            AddNewItemListView2();
        }

        private void CreateListView2()
        {

            
            listView2.View = View.Details;

            listView2.Columns.Add("ID");
            listView2.Columns.Add("Name");
            listView2.Columns.Add("CPU Usage");
            listView2.Columns.Add("Memory Usage");
            listView2.Columns.Add("Running Time");
            listView2.Columns.Add("Start Time");
            listView2.Columns.Add("Number of Threads");
            listView2.Columns.Add("Comment");

            listView2.GridLines = true;
            for(int i = 0; i< 8; i++)
            {
                listView2.Columns[i].Width = 110;
            }

        }




        private void AddNewItemListView2()
        {
            if (listView2.Items.Count >= 1)
            {
                listView2.Items.Remove(listView2.Items[0]);
            }
            if (selectedProcess != null)
            {
                listView2.Items.Add(new ListViewItem(new string[] { selectedProcess.Id, selectedProcess.Name, selectedProcess.CPU_usage, selectedProcess.Memory_usage, selectedProcess.Running_time, selectedProcess.Start_time, selectedProcess.Number_of_threads.ToString(), selectedProcess.Comment }));
            }
            textBox1.Text = "";
        }

        private void CheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (TopMost == false)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private MyProcess GetProcessFromId(string id)
        {
            foreach (MyProcess theprocess in myProcesses)
            {
                if(theprocess.Id.Equals(id))
                {
                    return theprocess;
                }
            }
            return null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            selectedProcess.Comment = textBox1.Text;
            textBox1.Text = "";
            AddNewItemListView2();
        }
    }
}
