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

namespace Lab_i_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

           //DriveTreeIntit; 
        }
        string fullPath;

        public void DriveTreeIntit ()
        {
            string[] drivesArray = Directory.GetLogicalDrives(); // получ. список лог. дисков системы
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();

            foreach (string s in drivesArray)
            {
                TreeNode device = new TreeNode(s, 0, 0);
                this.treeView1.Nodes.Add(device);
                GetDirs(device); 
            }

        }
        public void GetDirs(TreeNode node)
        {
            DirectoryInfo[] diArray;
            node.Nodes.Clear();
            string fullPath = node.FullPath;
            DirectoryInfo di = new DirectoryInfo(fullPath);
            try
            {
                diArray = di.GetDirectories();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show("Выбран узел", selectedNode.FullPath); 
        }
    }
}
