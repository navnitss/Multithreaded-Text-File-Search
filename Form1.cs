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
using System.Threading;

namespace nxj190007Asg4
{
    public partial class Form1 : Form
    {
        string path;
        int flc = 1;
        String key;
        int c = 0;

        public Form1()
        {
            InitializeComponent();
            //initialize list view
            listView1.View = View.Details;
            listView1.Columns.Add("Line No", 50);
            listView1.Columns.Add("Line", 450);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

       

        //Function to initiate the open file Dialog
        private void openfile()
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {

                 string ipath = openFileDialog1.FileName;
                try
                {
                   
                   
                    path = ipath;
                    Path.Text = path;


                }
                catch (IOException)
                {
                }


            }
        }
       

        private  void Browse_Click(object sender, EventArgs e)
        {
            //Initiates File Open
            openfile(); 

           
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
         //MultiThreading Implementation with Bacground worker to read file line by line
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            key = Keyword.Text;
            string line;

            if (!File.Exists(path))
            {
                using (FileStream strm = File.Create(path))
                {

                }

            }


            System.IO.StreamReader file =
       new System.IO.StreamReader(@path);

                
           
                while ((line = file.ReadLine()) != null)
                {
                string sendline = line;
                if (backgroundWorker1.CancellationPending)
                {
                    //CANCEL
                    e.Cancel = true;
                }
                

                else if ( line.ToLower().Contains(key.ToLower() + " "))                
                    {
                    backgroundWorker1.ReportProgress(10, sendline);
                    
                    Thread.Sleep(100);
                    }
                flc++;

                //lineCount = lineCount - 1;
            }
            




            //file.Close();

        }
        //BackgroundWorker calls this class to report progress to UI Elements 
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string f = e.UserState as String;
            
            String[] row = { flc.ToString(), f };
            ListViewItem item = new ListViewItem(row);
            listView1.Items.Add(item);
            progressBar1.Maximum = listView1.Items.Count;
            progressBar1.Value = progressBar1.Value+1;


        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        //Calls the backgroundworker to start search of file
        private void Search_Click(object sender, EventArgs e)
        {
            c++;
            Search.Text = "Cancel";
            if (c == 1)
                backgroundWorker1.RunWorkerAsync();
            else
                cancels();
            
        }

        //Cancels the operations
        private void cancels()
        {
            backgroundWorker1.CancelAsync();
            
        }

       
       
     
    }
}
