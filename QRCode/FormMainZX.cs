using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode.decoder;

namespace QRCode
{
    public partial class FormMainZX : Form
    {
        public FormMainZX()
        {
            InitializeComponent();
        }

        //选择图片
        private void QRMiddleImg_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片文件|*.bmp;*.jpg;*.png;*.gif|所有文件|*.*";
            openFileDialog1.Title = "选择图片";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                Image image = Image.FromFile(path);
                QRMiddleImg.Image = image;
            }
        }

        private string decodePath = "";

        //打开
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "图片文件|*.bmp;*.jpg;*.png;*.gif|所有文件|*.*";
            openFileDialog1.Title = "选择图片";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                decodePath = openFileDialog1.FileName;
                //Bitmap bitmap = (Bitmap)Image.FromFile(decodePath);
                //pictureBox2.Image = bitmap;

                pictureBox2.ImageLocation = openFileDialog1.FileName;
            }
        }

        //logo二维码
        private void btnEncodePic_Click(object sender, EventArgs e)
        {
            labShow.Text = "";
            string content = txtMsg.Text.Trim();
            if (String.IsNullOrEmpty(content))
            {
                MessageBox.Show("请输入编码内容");
                return;
            }
            try
            {
                //构造二维码编码器
                MultiFormatWriter writer = new MultiFormatWriter();
                Hashtable hints = new Hashtable();
                hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
                hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

                //先生成二维码
                ByteMatrix matrix = writer.encode(content, BarcodeFormat.QR_CODE, 300, 300, hints);
                Bitmap img = matrix.ToBitmap();

                //再处理要插入到二维码中的图片
                Image middlImg = QRMiddleImg.Image;

                //获取二维码实际尺寸（去掉二维码两边空白后的尺寸）
                Size realSize = writer.GetEncodeSize(content, BarcodeFormat.QR_CODE, 300, 300);

                //计算插入的图片的大小和位置
                int middleImgW = Math.Min((int)(realSize.Width / 3.5), middlImg.Width);
                int middleImgH = Math.Min((int)(realSize.Height / 3.5), middlImg.Height);
                int middleImgL = (img.Width - middleImgW) / 2;
                int middleImgT = (img.Height - middleImgH) / 2;

                //将img转换成bmp格式，否则后面无法创建 Graphics对象
                Bitmap bmpimg = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmpimg))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.DrawImage(img, 0, 0);
                }

                //在二维码中插入图片
                Graphics MyGraphic = Graphics.FromImage(bmpimg);

                //白底
                MyGraphic.FillRectangle(Brushes.White, middleImgL, middleImgT, middleImgW, middleImgH);
                MyGraphic.DrawImage(middlImg, middleImgL, middleImgT, middleImgW, middleImgH);
                pictureBox1.Image = bmpimg;

                //自动保存图片到当前目录
                //string filename = Environment.CurrentDirectory + "\\QR" + DateTime.Now.Ticks.ToString() + ".jpg";
                //bmpimg.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                //labShow.Text = "图片已保存到：" + filename;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //二维码
        private void btnEncode_Click(object sender, EventArgs e)
        {
            labShow.Text = "";
            string content = txtMsg.Text.Trim();
            if (String.IsNullOrEmpty(content))
            {
                MessageBox.Show("请输入原文!");
                return;
            }
            try
            {
                MultiFormatWriter writer = new MultiFormatWriter();
                Hashtable hints = new Hashtable();
                hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
                hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

                ByteMatrix matrix = writer.encode(content, BarcodeFormat.QR_CODE, 300, 300);
                Bitmap bitmap = matrix.ToBitmap();
                pictureBox1.Image = bitmap;

                //自动保存图片到当前目录
                //string filename = Environment.CurrentDirectory + "\\QR" + DateTime.Now.Ticks.ToString() + ".jpg";
                //bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                //labShow.Text = "图片已保存到：" + filename;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //解码
        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                //创建解码器
                MultiFormatReader reader = new MultiFormatReader();
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(decodePath);
                if (bitmap == null) return;

                LuminanceSource ls = new RGBLuminanceSource(bitmap, bitmap.Width, bitmap.Height);
                BinaryBitmap bb = new BinaryBitmap(new HybridBinarizer(ls));

                //注意编码对应 ： UTF-8
                Hashtable hints = new Hashtable();
                hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
                Result result = reader.decode(bb, hints);
                txtMsgDecode.Text = result.Text;
                labShow.Text = "解码成功！";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBarCode_Click(object sender, EventArgs e)
        {
            labShow.Text = "";
            string content = txtMsg.Text.Trim();
            Regex regex = new Regex("^[0-9]{13}$");
            if (!regex.IsMatch(content))
            {
                MessageBox.Show("请输入13位的数字！");
                return;
            }

            try
            {
                MultiFormatWriter writer = new MultiFormatWriter();
                ByteMatrix matrix = writer.encode(content, BarcodeFormat.EAN_13, 360, 150);
                Bitmap bitmap = matrix.ToBitmap();
                pictureBox1.Image = bitmap;


                //自动保存图片到当前目录
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "保存文件";
                save.Filter = "图片文件|*.jpg;*.png;*.bmp|所有文件|*.*";
                save.RestoreDirectory = true;
                save.InitialDirectory = Application.StartupPath;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName;
                    bitmap.Save(filename);
                    labShow.Text = "图片已保存到：" + filename;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
