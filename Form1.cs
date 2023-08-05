using Microsoft.Win32;

namespace _15._07._23
{
    public partial class Form1 : Form
    {
        private TreeView treeView1;
        private ImageList imageList;
        private string desktopPath = @"C:\Users\�����\Desktop"; 
        public Form1()
        {
            InitializeComponent();
            treeView1 = new TreeView();
            treeView1.Location = new Point(10, 10);
            treeView1.Size = new Size(1200, 800);
            Controls.Add(treeView1);

            imageList = new ImageList();
            imageList.ImageSize = new Size(25, 25); 
            treeView1.ImageList = imageList;

            CreateTreeView();
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // ��������� ����������� ��������� ������ � ImageList
        //    imageList.Images.Add("folder_icon", SystemIcons.Folder);
        //    imageList.Images.Add("file_icon", SystemIcons.WinLogo);

        //    // ��������� ���������� ������ ����� �������� ������
        //    CreateTreeView();
        //}

        private void CreateTreeView()
        {
            treeView1.Nodes.Clear();
            DirectoryInfo rootNode = new DirectoryInfo(desktopPath);
            treeView1.Nodes.Add(CreateDirectoryNode(rootNode));
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            directoryNode.ImageKey = "folder_icon"; // ��� ����������� ������ ��� ����������

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                TreeNode fileNode = new TreeNode(file.Name);
                fileNode.ImageKey = GetFileIconKey(file.FullName);

                directoryNode.Nodes.Add(fileNode);
            }

            return directoryNode;
        }

        private string GetFileIconKey(string filePath)
        {
            Icon? fileIcon = Icon.ExtractAssociatedIcon(filePath);
            if (fileIcon != null)
            {
                string key = filePath.ToLower(); // � ������ ������� ��� ������������ key
                if (!imageList.Images.ContainsKey(key))
                {
                    imageList.Images.Add(key, fileIcon.ToBitmap());
                }
                return key;
            }
            return "file_icon"; // ���������� ����������� ������, ���� �� ������� �������� ������ �����
        }

            }
}
        