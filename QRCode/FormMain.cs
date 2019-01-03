using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace QRCode
{
    public partial class FormMain : Form
    {
        private int m_width;

        private int m_height;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            m_width = picDecode.Width;
            m_height = picDecode.Height;
            cmbVersion.SelectedIndex = 4;
            cmbCorrectLevel.SelectedIndex = 1;
            cmbEncodingMode.SelectedIndex = 0;
        }

        private void btnOpen_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|All files (*.*)|*.*";
            openFileDialog1.Title = "Open";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                picDecode.Image = new Bitmap(fileName);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string codeString = txtEncode.Text.Trim();

            if (String.IsNullOrEmpty(codeString))
            {
                MessageBox.Show("请输入原文！");
                return;
            }

            picEncode.Image = CreateImageCode(codeString);
        }

        //生成二维码图片
        private Bitmap CreateImageCode(string codeString)
        {
            try
            {
                //创建二维码生成类
                QRCodeEncoder encoder = new QRCodeEncoder();

                //设置编码模式
                string encoding = cmbEncodingMode.Text;
                switch (encoding)
                {
                    case "Byte":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                        break;
                    case "AlphaNumeric":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                        break;
                    case "Numeric":
                        encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                        break;
                }

                //设置编码测量度-二维码容量大小
                try
                {
                    encoder.QRCodeScale = Convert.ToInt32(this.txtSize.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("请输入正确的测量度!");
                    return null;
                }

                //设备编码版本：字符串较长的情况下，用ThoughtWorks.QRCode生成二维码时出现“索引超出了数组界限”的错误。解决方法：将 QRCodeVersion 改为0。
                try
                {
                    encoder.QRCodeVersion = Convert.ToInt32(this.cmbVersion.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("请输入正确的编码版本!");
                    return null;
                }

                //设置编码错误纠正等级
                string correct = this.cmbCorrectLevel.Text;
                switch (correct)
                {
                    case "L":
                        encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                        break;
                    case "M":
                        encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                        break;
                    case "Q":
                        encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                        break;
                    case "H":
                        encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                        break;
                }

                //生成二维码图片
                Bitmap bitmap = encoder.Encode(codeString, System.Text.Encoding.UTF8);
                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            txtDecode.Text = DeCode(picDecode.Image);
        }

        //解码
        private string DeCode(Image image)
        {
            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                string codeString = decoder.decode(new QRCodeBitmapImage(new Bitmap(image)), System.Text.Encoding.UTF8);
                return codeString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return String.Empty;
            }
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath);
            saveFileDialog1.RestoreDirectory = true;//保存对话框是否记忆上次打开目录

            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
                string fileName = saveFileDialog1.FileName;
                if (String.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("文件名不能为空！");
                    return;
                }

                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.picEncode.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picEncode.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.picEncode.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        this.picEncode.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        //打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(picEncode.Image, 0, 0);
        }


        #region 二维码批量生成

        private DataSet myDataSet;

        private int imgSize;

        /// <summary>
        /// 批量生成二维码图片
        /// </summary>
        private void Create_CodeImages()
        {
            try
            {
                if (myDataSet != null)
                {
                    if (myDataSet.Tables[0].Rows.Count > 0)
                    {
                        //清空目录
                        DeleteDir(currentPath);
                        foreach (DataRow dr in myDataSet.Tables[0].Rows)
                        {
                            if (dr[2] != null)
                            {
                                //生成图片
                                Bitmap image = Create_ImgCode(dr[2].ToString(), imgSize);
                                //保存图片
                                SaveImg(currentPath, image);
                            }
                        }
                        //打开文件夹
                        Open_File(currentPath);
                        myDataSet = null;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //程序路径
        readonly string currentPath = Application.StartupPath + @"\BarCode_Images";

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strPath">保存路径</param>
        /// <param name="img">图片</param>
        public void SaveImg(string strPath, Bitmap img)
        {
            //保存图片到目录
            if (Directory.Exists(strPath))
            {
                //文件名称
                string guid = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                //当前目录不存在，则创建
                Directory.CreateDirectory(strPath);
            }
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="codeNumber">要生成二维码的字符串</param>     
        /// <param name="size">大小尺寸</param>
        /// <returns>二维码图片</returns>
        public Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber,Encoding.UTF8);
            return image;
        }

        /// <summary>
        /// /打开指定目录
        /// </summary>
        /// <param name="path"></param>
        public void Open_File(string path)
        {
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        /// <summary>
        /// 删除目录下所有文件
        /// </summary>
        /// <param name="aimPath">路径</param>
        public void DeleteDir(string aimPath)
        {
            try
            {
                //目录是否存在
                if (Directory.Exists(aimPath))
                {
                    // 检查目标目录是否以目录分割字符结束如果不是则添加之
                    if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                        aimPath += Path.DirectorySeparatorChar;
                    // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                    // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
                    string[] fileList = Directory.GetFiles(aimPath);
                    //string[] fileList = Directory.GetFileSystemEntries(aimPath);
                    // 遍历所有的文件和目录
                    foreach (string file in fileList)
                    {
                        // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
                        if (Directory.Exists(file))
                        {
                            DeleteDir(aimPath + Path.GetFileName(file));
                        }
                        // 否则直接Delete文件
                        else
                        {
                            File.Delete(aimPath + Path.GetFileName(file));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }
}
