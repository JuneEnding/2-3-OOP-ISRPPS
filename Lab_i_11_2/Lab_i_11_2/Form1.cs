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

namespace Lab_i_11_2
{
    public partial class Form1 : Form
    {
        string fullPath; // Полный путь
        public Form1()
        {
            InitializeComponent();
            TreeNode Node3 = new TreeNode("Node3");
            Node3.Nodes.Add(new TreeNode("node3.1"));
            treeView1.Nodes.Add(Node3);
            treeView1.Nodes[2].Nodes.Add(new TreeNode("node3.2"));
            treeView1.Nodes[2].Nodes[1].Nodes.Add(new TreeNode("node3.2.1"));
            //treeView1.Nodes[2].Clone(treeView1.Nodes[0].Clone());!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // УДАЛЕНИЕ УЗЛОВ
            //treeView1.Nodes[0].Nodes.RemoveAt(0);
            //treeView1.Nodes.Remove(Node3);
            treeView1.ImageList = imageList1;
            treeView1.Nodes[0].ImageIndex = 1;
            DeviceTreeIntit();
        }

        TreeNode device = null;
        public void DeviceTreeIntit()
        {
            string[] Array = Directory.GetLogicalDrives(); // получ. список лог. дисков системы
            this.treeView1.BeginUpdate(); // блок перерисовки окна до снятия блокировки
            this.treeView1.Nodes.Clear(); // удаление всех узлов дерева

            foreach (string s in Array)
            {
                device = new TreeNode(s, 0, 0); // Для всех устройств создается узел дерева
                this.treeView1.Nodes.Add(device);// add узла в коллекцию узлов дерева
                GetDirs(device);  //Доб. в дерево списка содержимого корневого каталога  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            treeView1.EndUpdate();
        }
        // Получение списка всез подкаталогов заданого каталога node
        public void GetDirs(TreeNode node)
        {
            DirectoryInfo[] diArray;
            node.Nodes.Clear();
            string fullPath = node.FullPath;
            DirectoryInfo di = new DirectoryInfo(fullPath);
            try
            {
                // Запись инф. о всех подкаталогах данного каталога массив diArray
                diArray = di.GetDirectories();
            }
            catch { return; }
            //diArray исп. для заполнение узла дерева содержимым каталога
            foreach (DirectoryInfo dirinfo in diArray)
            {
                TreeNode dir = new TreeNode(dirinfo.Name, 0, 0);
                node.Nodes.Add(dir);
            }
        }

        private void button1_Click(object sender, EventArgs e) { treeView1.Nodes[0].Expand(); }
        private void button2_Click(object sender, EventArgs e) { treeView1.Nodes[0].ExpandAll();}
        private void button3_Click(object sender, EventArgs e) { treeView1.Nodes[0].Collapse(); }// Метод класса TreeNode

        private void button4_Click(object sender, EventArgs e) { treeView1.Nodes[0].Toggle(); }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            label1.Text = e.Node.Text; // e.Node - ссылка на выбранный узел
            label6.Text = e.Node.FullPath; // полный путь к узлу
            DirectoryInfo di = new DirectoryInfo(e.Node.FullPath);
            FileInfo[] fiArray;
            DirectoryInfo[] diArray;
            // получение списка всех каталогов и файлов из выбранного каталога
            try
            {
                fiArray = di.GetFiles();
                diArray = di.GetDirectories();

            }  catch { return; }
            //listView1.Items.Clear();
            //Наполнение списка именами директорий
            foreach (DirectoryInfo directoryInfo in diArray)
            {
                ListViewItem lvi = new ListViewItem(directoryInfo.Name);
                lvi.SubItems.Add("0");
                // время последнего изменения
                lvi.SubItems.Add(directoryInfo.LastAccessTime.ToString());
                listView1.Items.Add(lvi);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();
            foreach(TreeNode node in e.Node.Nodes) { GetDirs(node); }
            treeView1.EndUpdate();
        }
    }
}
