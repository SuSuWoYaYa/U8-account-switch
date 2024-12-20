using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        //测试按钮
        private void button3_Click(object sender, EventArgs e)
        {
            //textBox1.Text += isolatedStorageDir + " isolatedStorageDir\r\n";

            //string appdir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //textBox1.Text += appdir + "\r\n";
            try
            {
                // Only get files that begin with the letter "c".
                //string[] dirs = Directory.GetFiles(@"C:\Users\Administrator\AppData\Local\IsolatedStorage", "ufsoftLoginInfoDP");

                string[] dirs = Directory.GetDirectories(isolatedStorageDir, "AssemFiles", SearchOption.AllDirectories);
                Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
                textBox1.Text += textBox1.Text + "The number of files starting with c is " + dirs.Length;
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
            //搜索获取目录
            //string[] dirs = Directory.GetDirectories(isolatedStorageDir, "AssemFiles", SearchOption.AllDirectories);

            //搜索获取文件路径
            string[] dirs = Directory.GetFiles(isolatedStorageDir, "ufsoftLoginInfoDP", SearchOption.AllDirectories);

            //foreach(string dir in dirs){
            //    MessageBox.Show(dir);
            //}
            //MessageBox.Show(""+ dirs.Length);

            if (dirs.Length == 1)
            {
                //AssemFilesDir = dirs[0];
                AssemFilesDir = Path.GetDirectoryName(dirs[0]);
                //MessageBox.Show("" + AssemFilesDir);
                //textBox1.Text = AssemFilesDir;
            }
            else if (dirs.Length == 0)
            {
                textBox1.Text = "获取配置目录失败";
                MessageBox.Show("获取配置目录失败");
                System.Environment.Exit(0);
                // Application.Exit();
            }
            //检测到目录下多个文件夹同名
            else
            {
                foreach (string dir in dirs)
                {
                    AssemFilesDir = "";

                    string FileLoginInfoDP = Path.Combine(dir, "ufsoftLoginInfoDP");
                    if (File.Exists(FileLoginInfoDP))
                    {
                        //MessageBox.Show(dir,"获取到多个配置目录，失败");
                        AssemFilesDir = dir;
                        break;

                    }

                }

                if (String.IsNullOrEmpty(AssemFilesDir))
                {
                    MessageBox.Show("获取到多个配置目录，失败");
                }
                // textBox1.Text = textBox1.Text + "\r\n" + dir;
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
            StartU8();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Copy(File188, FileLoginInfoDP, true);
            StartU8();
            Application.Exit();
        }


        //打开文件夹
        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", AssemFilesDir);




        }

        private void StartU8()
        {
            String[] U8Path = new String[] {
                "C:\\U8SOFT\\EnterprisePortal.exe",
                "D:\\U8SOFT\\EnterprisePortal.exe",
                "E:\\U8SOFT\\EnterprisePortal.exe",
                "F:\\U8SOFT\\EnterprisePortal.exe" };


            for (int i = 0; i < U8Path.Length; i++)
            {
                try
                {
                    System.Diagnostics.Process.Start(U8Path[i]);
                    //System.Diagnostics.Process.Start("D:\\U8SOFT\\EnterprisePortal.exe", "D:\\U8SOFT");
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                }
            }

        }



    }
}
