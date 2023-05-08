using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace U8_account_switch
{
    public partial class Form1 : Form
    {
        static string localApplicationDataDir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string isolatedStorageDir = Path.Combine(localApplicationDataDir, "IsolatedStorage");
        string AssemFilesDir = "";

        string File186 = "";
        string File188 = "";
        string FileLoginInfoDP = "";

        Boolean HasFile = true;

        public Form1()
        {
            InitializeComponent();

            getAssemFilesDir();

            FileCheck();

          
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox1.Text += isolatedStorageDir + " isolatedStorageDir\r\n";

            //string appdir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //textBox1.Text += appdir + "\r\n";
            try
            {
                // Only get files that begin with the letter "c".
                //string[] dirs = Directory.GetFiles(@"C:\Users\Administrator\AppData\Local\IsolatedStorage", "ufsoftLoginInfoDP");

                string[] dirs = Directory.GetDirectories(isolatedStorageDir, "AssemFiles",SearchOption.AllDirectories);
                Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
                textBox1.Text += textBox1.Text + "The number of files starting with c is "  + dirs.Length;
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                    textBox1.Text = textBox1.Text + "\r\n" + dir;
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine("The process failed: {0}", exception.ToString());
                textBox1.Text = "The process failed: " + exception.ToString();
            }
        }

        private void getAssemFilesDir()
        {
            string[] dirs = Directory.GetDirectories(isolatedStorageDir, "AssemFiles", SearchOption.AllDirectories);
            if (dirs.Length == 1)
            {
                AssemFilesDir = dirs[0];
                //textBox1.Text = AssemFilesDir;
            }
            else
            {
                textBox1.Text = "获取配置目录失败";
                MessageBox.Show("获取配置目录失败");
                System.Environment.Exit(0);
               // Application.Exit();
            }
        }

        private void FileCheck()
        {

            File186 = Path.Combine(AssemFilesDir, "186");
            File188 = Path.Combine(AssemFilesDir, "188");
            FileLoginInfoDP = Path.Combine(AssemFilesDir, "ufsoftLoginInfoDP");

            if (!File.Exists(File186))
            {
                HasFile = false;
                textBox1.Text += "获取186文件失败";
            }
            

            if (!File.Exists(File188))
            {
                HasFile = false;
                textBox1.Text += "获取188文件失败";
            }

            if (HasFile == false)
            {
                MessageBox.Show("获取切换文件失败");
                System.Diagnostics.Process.Start("explorer.exe", AssemFilesDir);
                System.Environment.Exit(0);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            File.Copy(File186, FileLoginInfoDP, true);
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Copy(File188, FileLoginInfoDP, true);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    
    }
}
