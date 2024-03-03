using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace hashViewer
{
    public partial class MainWindow : Window
    {
        FileHash hash = new FileHash();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                setFile(ofd.FileName);
            }
        }

        public void setFile(string? filePath)
        {
            if (filePath == null) { return; }
            FileInfo fi = new FileInfo(filePath);
            this.tbFilePath.Text = fi.FullName;
            hash.setFile(fi);
            ReadHash();
        }

        /// <summary>
        /// 将哈希显示到窗体中
        /// </summary>
        private void ReadHash()
        {
            this.tbMD5Value.Text = hash.GetMD5();
            this.tbSHA1Value.Text = hash.GetSHA1();
            this.tbSHA256Value.Text = hash.GetSHA256();
            this.tbSHA384Value.Text = hash.GetSHA384();
            this.tbSHA512Value.Text = hash.GetSHA512();
            this.lblVerify.Content = "校验";
            this.tbVerifyHash.Background = Background;
            this.tbVerifyHash.Text = "";
        }

        /// <summary>
        /// 校验哈希
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbVerifyHash_KeyUp(object sender, KeyEventArgs e)
        {
            string? key = this.tbVerifyHash.Text.ToUpper();
            if (key.Length == 0)
            {
                this.lblVerify.Content = "校验";
                this.tbVerifyHash.Background = Background;
                return;
            }
            foreach (var hashValue in hash.hashValues)
            {
                if (hashValue == key)
                {
                    this.lblVerify.Content = "校验成功";
                    this.tbVerifyHash.Background = Brushes.Green;
                    return;
                }
            }
            this.lblVerify.Content = "校验失败";
            this.tbVerifyHash.Background = Brushes.Red;
        }
    }

    /// <summary>
    /// 包含文件信息及其哈希
    /// </summary>
    public class FileHash
    {
        private enum HashType { ALL = -1, MD5 = 0, SHA1 = 1, SHA256 = 2, SHA384 = 3, SHA512 = 4 }
        private HashAlgorithm[] hasher = { MD5.Create(), SHA1.Create(), SHA256.Create(), SHA384.Create(), SHA512.Create() };
        private FileInfo? fileInfo;
        public string[] hashValues = new string[5];

        public void setFile(FileInfo fi)
        {
            this.fileInfo = fi;
        }

        private void _calHash(HashType hy = HashType.ALL)
        {
            if (this.fileInfo == null)
                return;
            FileStream fs = this.fileInfo.OpenRead();

            byte[] tmpHash = hasher[(int)hy].ComputeHash(fs);
            fs.Position = 0;
            this.hashValues[(int)hy] = BitConverter.ToString(tmpHash).Replace("-", "");

            fs.Close();
        }

        public string GetMD5()
        {
            _calHash(HashType.MD5);
            return this.hashValues[0] ?? "获取失败";
        }

        public string GetSHA1()
        {
            _calHash(HashType.SHA1);
            return this.hashValues[1] ?? "获取失败";
        }

        public string GetSHA256()
        {
            _calHash(HashType.SHA256);
            return this.hashValues[2] ?? "获取失败";
        }

        public string GetSHA384()
        {
            _calHash(HashType.SHA384);
            return this.hashValues[3] ?? "获取失败";
        }

        public string GetSHA512()
        {
            _calHash(HashType.SHA512);
            return this.hashValues[4] ?? "获取失败";
        }
    }
}